'use strict';

const gulp = require('gulp'),
    runSequence = require('run-sequence');

let taskNames = require('./modules/load');

gulp.task('default', function () {

    runSequence(...taskNames);
});

//gulp.task('build', function () {

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

    const copyTemplates = require('./modules/copyTemplates');

    let list = copyTemplates(['app/module/submodule/index.html'], 'app', 'wwwroot');

    console.log(list);

});