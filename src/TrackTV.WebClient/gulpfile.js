'use strict';

var gulp = require('gulp'),
    runSequence = require('run-sequence');

var fixGulp = require('./modules/fix-gulp');

fixGulp(gulp);

var pathConfig = require('./config/path.json'),
    includes = require('./config/bowerIncludes.json'),
    appConfig = require('./config/appConfig.json'),
    outputConfig = require('./config/outputConfig.json');

var pathResolver = require('./modules/pathResolver').instance(pathConfig),
    bowerComponents = require('./modules/bowerComponents').instance(pathResolver, includes),
    appBuilder = require('./modules/appBuilder').instance(pathResolver, appConfig.appRoot),
    appStream = require('./modules/appStream').instance(appBuilder, bowerComponents, pathResolver),
    output = require('./modules/buildOutput');

var devOutput = output.instance(outputConfig.devPath),
    prodOutput = output.instance(outputConfig.prodPath);

var devBuildSystem = require('./modules/devBuildSystem')
    .instance(devOutput, appStream)
    .registerTasks();

var productionBuildSystem = require('./modules/productionBuildSystem')
    .instance(prodOutput, appBuilder, appStream, pathResolver)
    .registerTasks();

var devSupport = require('./modules/devSupport')
    .instance(appBuilder, appStream)
    .registerTasks();

gulp.task('default', function () {

    runSequence(
        'dev-clean',
        'dev-styles',
        //'dev-fonts',
        'dev-scripts',
        'dev-browserify',
        'dev-templates',
        'dev-less',
        'dev-copy-initFile',
        'dev-module-headers',
        'dev-module-constants',
        'dev-module-libraries',
        'dev-copy-routeConfig',
        'dev-merge'
    );
});

gulp.task('build', function () {

    runSequence(
        'build-clean',
        'build-index',
        'build-scripts',
        'build-browserify',
        'build-source',
        'build-copy-initFile',
        'build-module-headers',
        'build-module-constants',
        'build-module-libraries',
        'build-copy-routeConfig',
        'build-styles',
        //'build-fonts',
        'build-less',
        'build-copy-content',
        'build-templates',
        'build-settings',
        'build-merge',
        'build-clear'
    );
});