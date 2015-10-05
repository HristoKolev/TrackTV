/// <binding BeforeBuild='default' Clean='default' ProjectOpened='default' />
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

function publicPath (path) {
    return 'wwwroot/' + path;
}

// constants
var libsPath = publicPath('lib');
var fontsPath = publicPath('fonts');
var mergedPath = publicPath('merged');
var bowerRootPath = 'bower_components';

// helper functions
function bowerComponent (path) {
    return bowerRootPath + '/' + path;
}

// Contents of the third-party.js
var scripts = [
    bowerComponent('jquery/dist/jquery.js'),
    bowerComponent('bootstrap/dist/js/bootstrap.js'),
    bowerComponent('angular/angular.js'),
    bowerComponent('angular-route/angular-route.js'),
];

// Contents of the third-party.css
var styles = [
    bowerComponent('bootstrap/dist/css/bootstrap.css'),
    bowerComponent('bootstrap/dist/css/bootstrap-theme.css'),
    bowerComponent('bootswatch/cosmo/bootstrap.css'),
];

var lessFiles = publicPath('content/*.less');
var cssFiles = publicPath('content/*.css');
var controllers = publicPath('app/controllers/*Controller.js');
var services = publicPath('app/services/*.js');
var directives = publicPath('app/directives/*.js');

(function RegisterTasks () {

    gulp.task('clean', function () {
        del([libsPath, fontsPath, cssFiles, mergedPath]);
    });

    gulp.task('scripts', function () {
        gulp.src(scripts)
            .pipe(concat('third-party.js'))
            .pipe(gulp.dest(libsPath));
    });

    gulp.task('styles', function () {
        gulp.src(styles)
            .pipe(concat('third-party.css'))
            .pipe(gulp.dest(libsPath));
    });

    gulp.task('fonts', function () {
        gulp.src([bowerComponent('bootstrap/dist/fonts/*')])
            .pipe(gulp.dest(fontsPath));
    });

    gulp.task('less', function () {

        gulp.src([lessFiles])
            .pipe(less())
            .pipe(gulp.dest(publicPath('content')))
            .on('error', console.error);
    });

    gulp.task('controllers', function () {
        gulp.src([controllers])
            .pipe(concat('controllers.js'))
            .pipe(gulp.dest(mergedPath));
    });

    gulp.task('services', function () {
        gulp.src([services])
            .pipe(concat('services.js'))
            .pipe(gulp.dest(mergedPath));
    });

    gulp.task('directives', function () {
        gulp.src([directives])
            .pipe(concat('directives.js'))
            .pipe(gulp.dest(mergedPath));
    });

    gulp.task('watch', function () {
        //less files
        gulp.watch(lessFiles, ['less']);

        //angular controllers
        gulp.watch(controllers, ['controllers']);

        //angular services
        gulp.watch(services, ['services']);

        //angular directives
        gulp.watch(directives, ['directives']);
    });

    gulp.task('default', ['clean', 'scripts', 'styles', 'fonts', 'less', 'controllers', 'services', 'directives']);

})();