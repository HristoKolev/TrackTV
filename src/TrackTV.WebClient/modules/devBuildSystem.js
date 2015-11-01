'use strict';

function devBuildSystem(output, appStream) {

    var that = Object.create(null);

    // modules
    var gulp = require('gulp'),
        del = require('del');

    // paths
    var libPath = output('/lib'),
        libScriptsPath = libPath('/scripts'),
        libTemplatesPath = libPath('/templates'),
        libCssPath = libPath('/styles'),
        libFontsPath = libPath('/fonts');

    var mergedPath = output('/merged');

    that.registerTasks = function () {

        gulp.task('dev-clean', function (callback) {

            del.sync([libPath.value(), mergedPath.value()]);

            callback();
        });

        gulp.task('dev-scripts', function () {

            return appStream.libScriptsStream()
                .pipe(libScriptsPath.destStream());

        });

        gulp.task('dev-templates', function () {

            return appStream.libTemplatesStream()
                .pipe(libTemplatesPath.destStream());
        });

        gulp.task('dev-styles', function () {

            return appStream.libStylesStream()
                .pipe(libCssPath.destStream());
        });

        gulp.task('dev-fonts', function () {

            return appStream.libFontsStream()
                .pipe(libFontsPath.destStream());
        });

        gulp.task('dev-less', function () {

            return appStream.appStylesStream()
                .pipe(mergedPath.destStream())
                .on('error', console.error);
        });

        gulp.task('dev-merge', function () {

            return appStream.appScriptsStream()
                .pipe(mergedPath.destStream());
        });

        gulp.task('dev-module-headers', function () {

            return appStream.moduleHeadersStream()
                .pipe(mergedPath.destStream());
        });

        gulp.task('dev-module-libraries', function () {

            return appStream.moduleLibrariesStream()
                .pipe(mergedPath.destStream());
        });

        gulp.task('dev-module-constants', function () {

            return appStream.moduleConstantsStream()
                .pipe(mergedPath.destStream());
        });

        gulp.task('dev-copy-routeConfig', function () {

            return appStream.routeConfigStream()
                .pipe(mergedPath.destStream());
        });

        gulp.task('dev-copy-initFile', function () {

            return appStream.initFileStream()
                .pipe(mergedPath.destStream());
        });

        gulp.task('dev-browserify', function () {

            return appStream.browserifyStream()
                .pipe(libScriptsPath.destStream());
        });

    };

    return that;
}

module.exports = {
    instance: devBuildSystem
};