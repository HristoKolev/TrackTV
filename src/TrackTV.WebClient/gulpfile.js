'use strict';

var gulp = require('gulp'),
    runSequence = require('run-sequence');

var fixGulp = require('./modules/fix-gulp');

fixGulp(gulp);

var pathConfig = require('./config/path.json'),
    includes = require('./config/bowerIncludes.json'),
    appConfig = require('./config/appConfig.json'),
    outputConfig = require('./config/outputConfig.json');

var bowerComponents = require('./modules/bowerComponents').instance(includes, pathConfig.bowerRootPath),
    appBuilder = require('./modules/appBuilder').instance(appConfig.appRoot),
    output = require('./modules/buildOutput');

var devOutput = output.instance(outputConfig.devPath),
    prodOutput = output.instance(outputConfig.prodPath);

var includer = require('./modules/includer').instance(appBuilder.indexFile, devOutput);

var devBuildSystem = require('./modules/devBuildSystem')
    .instance(appBuilder, devOutput, includer, bowerComponents)
    .registerTasks();

//var productionBuildSystem = require('./modules/productionBuildSystem')
//    .instance(prodOutput, appBuilder, appStream, pathResolver)
//    .registerTasks();

//var devSupport = require('./modules/devSupport')
//    .instance(appBuilder, appStream)
//    .registerTasks();

gulp.task('default', function () {

    runSequence(
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
        'dev-include-global-module-scripts',
        'dev-include-main-scripts',
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

    var streamFiles = require('./modules/plugins/streamFiles');
    var task = require('gulp-task');
    var q = require('q');
    var end = require('stream-end');

    task('do-work', function () {

        var deferred = q.defer();

        var stream = gulp.src('modules/*.js');

        stream.on('error', function (error) {

            deferred.reject(error);
        });

        stream.pipe(streamFiles('base'));

        stream.pipe(end(function () {

            console.log('kitten');
            deferred.resolve();
        }));

        return deferred.promise;

    });

    task.run('do-work')
        .then(function () {

            console.log(streamFiles.record());
        }, function (error) {

            console.log(error);
        });

});