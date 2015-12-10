'use strict';

require('./modules/load');

const gulp = require('gulp'),
    runSequence = require('run-sequence');

gulp.task('default', function () {

    runSequence(
        'dev-validate',
        'dev-clean',
        'dev-init',
        'dev-include-third-party-scripts',
        'dev-include-third-party-styles',
        'dev-include-init-file',
        'dev-include-module-headers',
        'dev-include-module-constants',
        'dev-include-module-libraries',
        'dev-include-route-config',
        'dev-browserify',
        'dev-include-global-scripts',
        'dev-include-global-less',
        'dev-include-global-module-scripts',
        'dev-include-global-module-less',
        'dev-include-main-scripts',
        'dev-include-main-less-styles',
        'dev-process-includes',
        'dev-update-includes',
        'dev-copy-content'
    );
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

});