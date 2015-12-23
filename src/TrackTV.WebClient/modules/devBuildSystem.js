'use strict';

const Mocha = require('mocha');

const gulp = require('gulp'),
    del = require('del'),
    glob = require('glob-all').sync,
    browserify = require('browserify'),
    source = require('vinyl-source-stream'),
    buffer = require('vinyl-buffer'),
    path = require('path');

const output = require('../config/outputConfig').devPath;

const copyFiles = require('./copyFiles'),
    appBuilder = require('./appBuilder'),
    includer = require('./includer'),
    runner = require('./runner'),
    includes = require('./instances/bowerComponents');

// modules
const constants = {
    thirdPartyScripts: 'third-party-scripts',
    thirdPartyStyles: 'third-party-styles',
    initFile: 'init-file',
    moduleHeaders: 'module-headers',
    moduleConstants: 'module-constants',
    moduleLibraries: 'module-libraries',
    routeConfig: 'route-config',
    browserified: 'browserified',
    globalScripts: 'global-scripts',
    globalModuleScripts: 'global-module-scripts',
    scripts: 'main-scripts',
    globalLess: 'global-less',
    globalModuleLess: 'global-module-less',
    lessFiles: 'main-less-styles',
    templates: 'local-templates'
};

const locations = {
    less: 'less',
    scripts: 'scripts',
    thirdParty: 'third-party',
    headers: 'headers',
    constants: 'constants',
    libraries: 'libraries'
};

const formatters = includer.formatters;

const names = [];

function register(name, func) {

    gulp.task(name, func);
    names.push(name);
}

function registerTasks() {

    register('dev-validate', function (callback) {

        let mocha = new Mocha();

        let validationsFilePath = path.resolve('./validations.js');

        mocha.addFile(validationsFilePath);

        mocha.run(function (failedTestsCount) {

            if (failedTestsCount === 0) {

                callback();
            }
            else {

                throw new Error('Validation failed.');
            }
        });
    });

    register('dev-clean', function (callback) {

        del.sync([
            output + '/*',
            output + '/*/'
        ], {force: true});

        callback();
    });

    register('dev-copy-app', function () {

        copyFiles.copyStructure(glob(path.join(appBuilder.appPath(), '**/*.*')), output, appBuilder.appPath());
    });

    register('dev-init', function () {

        includer.createIncludeLog();
        //includer.copyIndex();
    });

    register('dev-include-' + constants.thirdPartyScripts, function () {

        includer.copyAndIncludeDirectory(
            constants.thirdPartyScripts,
            includes.scripts,
            includes.basePath,
            formatters.scriptFormatter,
            [],
            locations.thirdParty
        );
    });

    register('dev-include-' + constants.thirdPartyStyles, function () {

        includer.copyAndIncludeDirectory(
            constants.thirdPartyStyles,
            includes.styles,
            includes.basePath,
            formatters.styleFormatter,
            [],
            locations.thirdParty
        );
    });

    register('dev-include-' + constants.initFile, function () {

        includer.copyAndIncludeFile(
            constants.initFile,
            appBuilder.initFile,
            appBuilder.appPath(),
            formatters.scriptFormatter
        );
    });

    register('dev-include-' + constants.moduleHeaders, function () {

        var files = glob(appBuilder.moduleHeaders);

        if (files.length) {

            includer.copyAndIncludeFiles(
                constants.moduleHeaders,
                files,
                appBuilder.appPath(),
                formatters.scriptFormatter,
                []
            );
        }
    });

    register('dev-include-' + constants.moduleConstants, function () {

        var files = glob(appBuilder.moduleConstants);

        if (files.length) {

            includer.copyAndIncludeFiles(
                constants.moduleConstants,
                files,
                appBuilder.appPath(),
                formatters.scriptFormatter,
                []
            );
        }
    });

    register('dev-include-' + constants.moduleLibraries, function () {

        var files = glob(appBuilder.moduleLibraries);

        if (files.length) {

            includer.copyAndIncludeFiles(
                constants.moduleLibraries,
                files,
                appBuilder.appPath(),
                formatters.scriptFormatter,
                []
            );
        }
    });

    register('dev-include-' + constants.routeConfig, function () {

        includer.copyAndIncludeFile(
            constants.routeConfig,
            appBuilder.routeConfig,
            appBuilder.appPath(),
            formatters.scriptFormatter
        );
    });

    register('dev-browserify', function () {

        var files = glob(appBuilder.npmModuleFiles);

        if (files.length) {

            var fileName = constants.browserified + '.js';

            includer.logInclude(constants.browserified, [fileName], formatters.scriptFormatter);

            var browserifyOptions = {
                debug: true
            };

            return browserify(files, browserifyOptions)
                .bundle()
                .pipe(source(fileName))
                .pipe(buffer())
                .pipe(gulp.dest(output));
        }
    });

    register('dev-include-' + constants.globalScripts, function () {

        var files = glob(appBuilder.globalScripts);

        if (files.length) {

            includer.copyAndIncludeFiles(
                constants.globalScripts,
                files,
                appBuilder.appPath(),
                formatters.scriptFormatter,
                []
            );
        }
    });

    register('dev-include-' + constants.globalLess, function () {

        var files = glob(appBuilder.globalLess);

        if (files.length) {

            includer.copyAndIncludeFiles(
                constants.globalLess,
                files,
                appBuilder.appPath(),
                formatters.styleFormatter,
                ['less']
            );
        }
    });

    register('dev-include-' + constants.globalModuleScripts, function () {

        var files = glob(appBuilder.globalModuleScripts);

        includer.copyAndIncludeFiles(
            constants.globalModuleScripts,
            files,
            appBuilder.appPath(),
            formatters.scriptFormatter,
            []
        );
    });

    register('dev-include-' + constants.globalModuleLess, function () {

        var files = glob(appBuilder.globalModuleLess);

        if (files.length) {

            includer.copyAndIncludeFiles(
                constants.globalModuleLess,
                files,
                appBuilder.appPath(),
                formatters.styleFormatter,
                ['less']
            );
        }
    });

    register('dev-include-' + constants.scripts, function () {

        var files = glob(appBuilder.scripts);

        if (files.length) {

            includer.copyAndIncludeFiles(
                constants.scripts,
                files,
                appBuilder.appPath(),
                formatters.scriptFormatter,
                []
            );
        }
    });

    register('dev-include-' + constants.lessFiles, function () {

        var files = glob(appBuilder.lessFiles);

        if (files.length) {

            includer.copyAndIncludeFiles(
                constants.lessFiles,
                files,
                appBuilder.appPath(),
                formatters.styleFormatter,
                ['less', 'css-rebase']
            );
        }
    });

    register('dev-process-templates', function () {

        var files = glob(appBuilder.templates);

        if (files.length) {

            includer.copyAndIncludeFiles(
                constants.templates,
                files,
                appBuilder.appPath(),
                formatters.none,
                ['html-rebase']
            );
        }
    });

    register('dev-process-includes', function (callback) {

        runner.run(includer.readIncludes(), output)
            .then(function (newIncludes) {

                includer.writeIncludes(newIncludes);

            }).then(callback);
    });

    register('dev-update-includes', function () {

        includer.updateIncludes();
    });

    //register('dev-copy-content', function () {
    //
    //    let list = copyContent(appBuilder, output);
    //
    //    for (let directory of list) {
    //
    //        let paths = glob(path.join(directory.targetPath, '**/*'));
    //
    //        if (paths.length > 0) {
    //
    //            copyFiles.copyStructure(paths, directory.destinationPath, directory.targetPath);
    //        }
    //    }
    //});

    return names;
}

module.exports = {
    registerTasks
};