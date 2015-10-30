'use strict';

function devBuildSystem(appBuilder, includes, pathResolver) {

    var that = require('./buildSystem').instance(appBuilder, includes);

    // modules
    var gulp = require('gulp'),
        del = require('del'),
        insert = require('gulp-insert'),
        jslint = require('gulp-jslint-simple'),
        jshint = require('gulp-jshint'),
        stylish = require('jshint-stylish');

    // paths
    var libPath = pathResolver.publicPath('/lib'),
        libTemplatesPath = libPath + '/templates',
        libCssPath = libPath + '/css',
        libFontsPath = libPath + '/fonts';

    var mergedPath = pathResolver.publicPath('/merged');

    that.registerTasks = function () {

        gulp.task('dev-clean', function (callback) {

            del([libPath, mergedPath]);

            callback();
        });

        gulp.task('dev-scripts', function () {

            return that.libScriptsStream()
                .pipe(gulp.dest(libPath));

        });

        gulp.task('dev-templates', function () {

            return that.libTemplatesStream()
                .pipe(gulp.dest(libTemplatesPath));
        });

        gulp.task('dev-styles', function () {

            return that.libStylesStream()
                .pipe(gulp.dest(libCssPath));
        });

        gulp.task('dev-fonts', function () {

            return that.libFontsStream()
                .pipe(gulp.dest(libFontsPath));
        });

        gulp.task('dev-less', function () {

            return that.appStylesStream()
                .pipe(gulp.dest(mergedPath))
                .on('error', console.error);
        });

        gulp.task('dev-merge', function () {

            return that.appScriptsStream()
                .pipe(gulp.dest(mergedPath));
        });

        gulp.task('dev-lint', function () {

            var jsLintFlagComment = '/*global $, angular, window */\n';

            that.allSourcesStream()
                .pipe(insert.transform(function (contents, file) {

                    return jsLintFlagComment + contents;
                }))
                .pipe(jslint.run())
                .pipe(jslint.report(stylish))
                .on('error', console.error);

            that.allSourcesStream()
                .pipe(jshint('.jshintrc'))
                .pipe(jshint.reporter(stylish))
                .on('error', console.error);
        });

        gulp.task('dev-module-headers', function () {

            return that.moduleHeadersStream()
                .pipe(gulp.dest(mergedPath));
        });

        gulp.task('dev-browserify', function () {

            return that.browserifyStream()
                .pipe(gulp.dest(libPath));

        });

        gulp.task('watch', function () {

            //less files
            gulp.watch(appBuilder.lessFiles(), ['dev-less']);
            console.log('Watching: ' + appBuilder.lessFiles());

            //angular app
            gulp.watch(appBuilder.sourceFiles(), ['dev-merge', 'dev-module-headers', 'dev-lint']);
            console.log('Watching: ' + appBuilder.sourceFiles());

            //browserify
            gulp.watch(appBuilder.npmModuleFiles(), ['dev-browserify']);
            console.log('Watching: ' + appBuilder.npmModuleFiles());

            //configuration files
            var buildSystemConfigs = './config/*.json';

            gulp.watch(buildSystemConfigs, ['default']);
            console.log('Watching: ' + buildSystemConfigs);
        });

    };

    return that;
}

module.exports = {
    instance: devBuildSystem
};