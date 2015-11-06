'use strict';

function devBuildSystem(appBuilder, output, includer, includes) {

    var that = Object.create(null);

    // modules
    var gulp = require('gulp'),
        del = require('del'),
        glob = require('glob-all'),
        path = require('path'),
        browserify = require('browserify'),
        source = require('vinyl-source-stream'),
        buffer = require('vinyl-buffer'),
        fs = require('fs');

    var constants = {
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
        scripts: 'main-scripts'
    };

    var browserifyOptions = {
        debug: true,
        entries: glob.sync(appBuilder.npmModuleFiles)
    };

    that.registerTasks = function () {

        gulp.task('dev-clean', function (callback) {

            del.sync([
                output.value() + '/*',
                output.value() + '/*/'
            ], { force: true });

            callback();
        });

        gulp.task('dev-init', function () {

            includer.createIncludeLog();

            includer.copyIndex();
        });

        gulp.task('dev-include-' + constants.thirdPartyScripts, function () {

            return includer.includeDirectory(
                constants.thirdPartyScripts,
                includes.scripts,
                includes.baseDir,
                includer.formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.thirdPartyStyles, function () {

            return includer.includeDirectory(
                constants.thirdPartyStyles,
                includes.styles,
                includes.baseDir,
                includer.formatters.styleFormatter
            );
        });

        gulp.task('dev-include-' + constants.initFile, function () {

            return includer.includeFile(
                constants.initFile,
                appBuilder.initFile,
                appBuilder.appPath(),
                includer.formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.moduleHeaders, function () {

            return includer.includeModuleFiles(
                constants.moduleHeaders,
                glob.sync(appBuilder.moduleHeaders),
                includer.formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.moduleConstants, function () {

            return includer.includeModuleFiles(
                constants.moduleConstants,
                glob.sync(appBuilder.moduleConstants),
                includer.formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.moduleLibraries, function () {

            return includer.includeModuleFiles(
                constants.moduleLibraries,
                glob.sync(appBuilder.moduleLibraries),
                includer.formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.routeConfig, function () {

            return includer.includeFile(
                constants.routeConfig,
                appBuilder.routeConfig,
                appBuilder.appPath(),
                includer.formatters.scriptFormatter
            );
        });

        gulp.task('dev-browserify', function () {

            var fileName = constants.browserified + '.js';

            includer.logIncludes(constants.browserified, [fileName], includer.formatters.scriptFormatter);

            return browserify(browserifyOptions)
                .bundle()
                .pipe(source(fileName))
                .pipe(buffer())
                .pipe(output.destStream());
        });

        gulp.task('dev-include-' + constants.globalScripts, function () {

            return includer.includeDirectory(
                constants.globalScripts,
                glob.sync(appBuilder.globalScripts),
                appBuilder.appPath(),
                includer.formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.globalModuleScripts, function () {

            return includer.includeSeparatedModuleFiles(
                constants.globalModuleScripts,
                glob.sync(appBuilder.globalModuleScripts),
                appBuilder.modulesDir,
                includer.formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.scripts, function () {

            return includer.includeDirectory(
                constants.scripts,
                glob.sync(appBuilder.scripts),
                appBuilder.modulesDir,
                includer.formatters.scriptFormatter
            );
        });

        gulp.task('dev-update-includes', function () {

            includer.updateIncludes();
        });

        ////////////////////////////////////////////////////////////////////////////////////

        //gulp.task('dev-templates', function () {

        //    return appStream.libTemplatesStream()
        //        .pipe(libTemplatesPath.destStream());
        //});

        //gulp.task('dev-fonts', function () {

        //    return appStream.libFontsStream()
        //        .pipe(libFontsPath.destStream());
        //});

        //gulp.task('dev-less', function () {

        //    return appStream.appStylesStream()
        //        .pipe(mergedPath.destStream())
        //        .on('error', console.error);
        //});

        //gulp.task('dev-merge', function () {

        //    return appStream.appScriptsStream()
        //        .pipe(mergedPath.destStream());
        //});

    };

    return that;
}

module.exports = {
    instance: devBuildSystem
};