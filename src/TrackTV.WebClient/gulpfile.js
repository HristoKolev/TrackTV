/// <binding Clean='default' ProjectOpened='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/



var gulp = require('gulp'),
    del = require('del'),
    concat = require('gulp-concat'),
    less = require('gulp-less'),
    fs = require('fs'),
    path = require('path'),
    replace = require('gulp-replace'),
    tap = require('gulp-tap'),
    dom = require('gulp-dom'),
    insert = require('gulp-insert');

var settings = require('./wwwroot/app/settings.json'),
    sourceManager = require('./sourceManager');

sourceManager.setSettings(settings.source);

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

function getFolders (dir) {
    return fs.readdirSync(dir)
        .filter(function (file) {
            return fs.statSync(path.join(dir, file)).isDirectory();
        });
}

function getAppScripts () {

    var builder = sourceManager.createSourceListBuilder();

    builder.addPublicFile('/app/init.js')
        .addModule(getFolders(settings.source.moduleRootPath))
        .addModuleFile('main', '/routeConfig.js');

    return builder.src();
}

var pathResolve = sourceManager.pathResolve;
var modulePath = sourceManager.modulePath;

// source files

var lessFiles = modulePath.modulePath('main', '/styles/*.less');

var bowerScripts = pathResolve.bowerComponent([
    '/jquery/dist/jquery.js',
    '/bootstrap/dist/js/bootstrap.js',
    '/toastr/toastr.js',
    '/angular/angular.js',
    '/angular-route/angular-route.js',
    '/angular-cookies/angular-cookies.js',
    '/angular-gravatar/build/angular-gravatar.js',
    '/angular-utils-pagination/dirPagination.js',
    '/underscore/underscore.js',
    '/moment/moment.js'
]);

var npmScripts = pathResolve.npmComponent([
    '/underscore.string/dist/underscore.string.js'
]);

var styles = pathResolve.bowerComponent([
    '/bootstrap/dist/css/bootstrap.css',
    '/bootswatch/cosmo/bootstrap.css',
    '/toastr/toastr.css'
]);

var fonts = pathResolve.bowerComponent([
    '/bootstrap/dist/fonts/*'
]);

var templates = pathResolve.bowerComponent([
    '/angular-utils-pagination/dirPagination.tpl.html'
]);

var appScripts = getAppScripts();

// tasks

gulp.task('clean', function () {

    del.sync(pathResolve.publicPath([
        '/lib',
        '/styles.css',
        '/merged-app.js'
    ]));
});

gulp.task('scripts', function () {

    var libPath = pathResolve.publicPath('/lib');

    gulp.src(bowerScripts.concat(npmScripts))
        .pipe(concat('third-party.js'))
        .pipe(gulp.dest(libPath));

    gulp.src(templates)
        .pipe(gulp.dest(pathResolve.publicPath('/lib/templates')));
});

gulp.task('styles', function () {

    gulp.src(styles)
        .pipe(concat('third-party.css'))
        .pipe(gulp.dest(pathResolve.publicPath('/lib/css')));
});

gulp.task('fonts', function () {

    var fontsPath = pathResolve.publicPath('/lib/fonts');

    gulp.src([fonts])
        .pipe(gulp.dest(fontsPath));
});

gulp.task('less', function () {

    gulp.src([lessFiles])
        .pipe(less())
        .pipe(gulp.dest(pathResolve.publicPath()))
        .on('error', console.error);
});

gulp.task('merge', function () {

    gulp.src(appScripts)
        .pipe(concat('merged-app.js'))
        .pipe(gulp.dest(pathResolve.publicPath()));;

});

gulp.task('watch', function () {
    //less files
    gulp.watch(lessFiles, ['less']);
    console.log('Watching: ' + lessFiles);

    var sourceFiles = modulePath.getSourceFilesPattern();

    //angular app
    gulp.watch(sourceFiles, ['merge']);
    console.log('Watching: ' + sourceFiles);
});

var buildPath = pathResolve.publicPath('/build');

gulp.task('compile', function () {

    // clean
    del.sync(buildPath);

});

gulp.task('default', ['clean', 'scripts', 'styles', 'fonts', 'less', 'merge']);

