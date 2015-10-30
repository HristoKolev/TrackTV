'use strict';

function devBuildSystem(appBuilder, buildSystem, pathResolver) {

    var that = Object.create(null);

    // modules
    var gulp = require('gulp'),
        del = require('del'),
        insert = require('gulp-insert'),
        jslint = require('gulp-jslint-simple'),
        jshint = require('gulp-jshint'),
        stylish = require('jshint-stylish');

    // paths
    var libPath = pathResolver.publicPath('/lib'),
        libScriptsPath = libPath + '/scripts',
        libTemplatesPath = libPath + '/templates',
        libCssPath = libPath + '/styles',
        libFontsPath = libPath + '/fonts';

    var mergedPath = pathResolver.publicPath('/merged');

    that.registerTasks = function () {

        gulp.task('dev-clean', function (callback) {

            del.sync([libPath, mergedPath]);

            callback();
        });

        gulp.task('dev-scripts', function () {

            return buildSystem.libScriptsStream()
                .pipe(gulp.dest(libScriptsPath));

        });

        gulp.task('dev-templates', function () {

            return buildSystem.libTemplatesStream()
                .pipe(gulp.dest(libTemplatesPath));
        });

        gulp.task('dev-styles', function () {

            return buildSystem.libStylesStream()
                .pipe(gulp.dest(libCssPath));
        });

        gulp.task('dev-fonts', function () {

            return buildSystem.libFontsStream()
                .pipe(gulp.dest(libFontsPath));
        });

        gulp.task('dev-less', function () {

            return buildSystem.appStylesStream()
                .pipe(gulp.dest(mergedPath))
                .on('error', console.error);
        });

        gulp.task('dev-merge', function () {

            return buildSystem.appScriptsStream()
                .pipe(gulp.dest(mergedPath));
        });

        gulp.task('dev-lint', function () {

            var jsLintFlagComment = '/*global $, angular, window */\n';

            buildSystem.allSourcesStream()
                .pipe(insert.transform(function (contents, file) {

                    return jsLintFlagComment + contents;
                }))
                .pipe(jslint.run())
                .pipe(jslint.report(stylish))
                .on('error', console.error);

            buildSystem.allSourcesStream()
                .pipe(jshint('.jshintrc'))
                .pipe(jshint.reporter(stylish))
                .on('error', console.error);
        });

        gulp.task('dev-module-headers', function () {

            return buildSystem.moduleHeadersStream()
                .pipe(gulp.dest(mergedPath));
        });

        gulp.task('dev-browserify', function () {

            return buildSystem.browserifyStream()
                .pipe(gulp.dest(libScriptsPath));

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