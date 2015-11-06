'use strict';

function devSupport(appBuilder, appStream) {

    var gulp = require('gulp'),
        insert = require('gulp-insert'),
        jslint = require('gulp-jslint-simple'),
        jshint = require('gulp-jshint'),
        stylish = require('jshint-stylish');

    var that = Object.create(null);

    that.registerTasks = function () {

        gulp.task('lint', function () {

            var jsLintFlagComment = '/*global $, angular, window */\n';

            appStream.allSourcesStream()
                .pipe(insert.transform(function (contents) {

                    return jsLintFlagComment + contents;
                }))
                .pipe(jslint.run())
                .pipe(jslint.report(stylish))
                .on('error', console.error);

            appStream.allSourcesStream()
                .pipe(jshint('.jshintrc'))
                .pipe(jshint.reporter(stylish))
                .on('error', console.error);
        });

        gulp.task('watch', function () {

            //less files
            gulp.watch(appBuilder.lessFiles, ['dev-less']);
            console.log('Watching: ' + appBuilder.lessFiles);

            //angular app
            gulp.watch(appBuilder.sourceFiles, [
                'dev-merge',
                'dev-module-headers',
                'dev-module-libraries',
                'dev-module-constants',
                'dev-copy-initFile',
                'dev-copy-routeConfig',
                'lint'
            ]);
            console.log('Watching: ' + appBuilder.sourceFiles);

            //browserify
            gulp.watch(appBuilder.npmModuleFiles, ['dev-browserify']);
            console.log('Watching: ' + appBuilder.npmModuleFiles);

            //configuration files
            var buildSystemConfigs = './config/*.json';

            gulp.watch(buildSystemConfigs, ['default']);
            console.log('Watching: ' + buildSystemConfigs);
        });

    };

    return that;
}

module.exports = {
    instance: devSupport
};