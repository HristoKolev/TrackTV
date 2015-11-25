'use strict';

const gulp = require('gulp'),
    runSequence = require('run-sequence');

const fixGulp = require('./modules/fix-gulp');

fixGulp(gulp);

const pathConfig = require('./config/path.json'),
    includes = require('./config/bowerIncludes.json'),
    appConfig = require('./config/appConfig.json'),
    outputConfig = require('./config/outputConfig.json');

const output = require('./modules/pathChain');

const devOutput = output.instance(outputConfig.devPath);

const bowerComponents = require('./modules/bowerComponents').instance(includes, pathConfig.bowerRootPath),
    appBuilder = require('./modules/appBuilder').instance(appConfig.appPath),
    tasks = require('./modules/tasks'),
    runner = require('./modules/runner');

tasks.load(runner);

const includer = require('./modules/includer').instance(appBuilder.indexFile, devOutput);

const instance = require('./modules/devBuildSystem')
    .instance(appBuilder, devOutput, includer, bowerComponents, runner)
    .registerTasks();

//var productionBuildSystem = require('./modules/productionBuildSystem')
//    .instance(prodOutput, appBuilder, appStream, pathResolver)
//    .registerTasks();

//var devSupport = require('./modules/devSupport')
//    .instance(appBuilder, appStream)
//    .registerTasks();

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
        'dev-update-includes'
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