/// <binding Clean='default' ProjectOpened='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    del = require('del'),
    concat = require('gulp-concat'),
    less = require('gulp-less');

(function () {
    // Workaround for https://github.com/gulpjs/gulp/issues/71
    var origSrc = gulp.src;
    gulp.src = function () {
        return fixPipe(origSrc.apply(this, arguments));
    };

    function fixPipe (stream) {
        var origPipe = stream.pipe;
        stream.pipe = function (dest) {
            arguments[0] = dest.on('error', function (error) {
                var nextStreams = dest._nextStreams;
                if (nextStreams) {
                    nextStreams.forEach(function (nextStream) {
                        nextStream.emit('error', error);
                    });
                } else if (dest.listeners('error').length === 1) {
                    throw error;
                }
            });
            var nextStream = fixPipe(origPipe.apply(this, arguments));
            (this._nextStreams || (this._nextStreams = [])).push(nextStream);
            return nextStream;
        };
        return stream;
    }
})();

function bowerComponent (path) {

    var bowerRootPath = 'bower_components';

    return bowerRootPath + '/' + path;
}

function publicPath (path) {

    path = path || '';

    return 'wwwroot/' + path;
}

function SourceListBuilder () {

    this.files = [];

    SourceListBuilder.prototype.addModule = function (name) {

        this.files.push(publicPath('app/' + name + '/module.js'));
        this.files.push(publicPath('app/' + name + '/scripts/**/**/*.js'));

        return this;
    };

    SourceListBuilder.prototype.addFile = function (path) {

        this.files.push(publicPath(path));

        return this;
    };

    SourceListBuilder.prototype.src = function () {

        return this.files;
    };
}

var lessFiles = publicPath('app/main/styles/*.less');

gulp.task('clean', function () {

    del.sync([publicPath('lib'), publicPath('styles.css'), publicPath('merged-app.js')]);
});

gulp.task('scripts', function () {

    var scripts = [
        bowerComponent('jquery/dist/jquery.js'),
        bowerComponent('bootstrap/dist/js/bootstrap.js'),
        bowerComponent('toastr/toastr.js'),
        bowerComponent('angular/angular.js'),
        bowerComponent('angular-route/angular-route.js'),
        bowerComponent('angular-cookies/angular-cookies.js'),
        bowerComponent('angular-gravatar/build/angular-gravatar.js')
    ];

    gulp.src(scripts)
        .pipe(concat('third-party.js'))
        .pipe(gulp.dest(publicPath('lib')));
});

gulp.task('styles', function () {

    var styles = [
        bowerComponent('bootstrap/dist/css/bootstrap.css'),
        bowerComponent('bootswatch/cosmo/bootstrap.css'),
        bowerComponent('toastr/toastr.css')
    ];

    gulp.src(styles)
        .pipe(concat('third-party.css'))
        .pipe(gulp.dest(publicPath('lib/css')));
});

gulp.task('fonts', function () {

    var fontsPath = publicPath('lib/fonts');

    gulp.src([bowerComponent('bootstrap/dist/fonts/*')])
        .pipe(gulp.dest(fontsPath));
});

gulp.task('less', function () {

    gulp.src([lessFiles])
        .pipe(less())
        .pipe(gulp.dest(publicPath()))
        .on('error', console.error);
});

gulp.task('merge', function () {

    var builder = new SourceListBuilder();

    builder.addFile('app/init.js')
        .addModule('services')
        .addModule('directives')
        .addModule('main')
        .addFile('app/main/routeConfig.js');

    gulp.src(builder.src())
        .pipe(concat('merged-app.js'))
        .pipe(gulp.dest(publicPath()));;
});

gulp.task('watch', function () {
    //less files
    gulp.watch(lessFiles, ['less']);

    var sourceFiles = publicPath('app/**/**/**/**/*.js');

    //angular app
    gulp.watch(sourceFiles, ['merge']);
});

gulp.task('default', ['clean', 'scripts', 'styles', 'fonts', 'less', 'merge']);