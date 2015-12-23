'use strict';

const gulp = require('gulp'),
    runSequence = require('run-sequence');

let taskNames = require('./modules/load');

gulp.task('default', function () {

    runSequence(...taskNames);
});

//gulp.task('build', function () {
//
//    runSequence(
//        'build-clean',
//        'build-index',
//        'build-scripts',
//        'build-browserify',
//        'build-source',
//        'build-copy-initFile',
//        'build-module-headers',
//        'build-module-constants',
//        'build-module-libraries',
//        'build-copy-routeConfig',
//        'build-styles',
//        //'build-fonts',
//        'build-less',
//        'build-copy-content',
//        'build-templates',
//        'build-settings',
//        'build-merge',
//        'build-clear'
//    );
//});

gulp.task('test-task', function () {

    const glob = require('glob-all').sync,
        _ = require('underscore'),
        path = require('path'),
        copyFiles = require('./modules/copyFiles');

    let appPath = 'app';
    let outputPath = 'wwwroot';

    copyFiles.copyStructure(glob(path.join(appPath, '**/*.*')), outputPath, appPath);
});