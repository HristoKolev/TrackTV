'use strict';

function devBuildSystem(appBuilder, output, includer, includes, runner) {

    var that = Object.create(null);

    // modules
    var gulp = require('gulp'),
        del = require('del'),
        glob = require('glob-all'),
        browserify = require('browserify'),
        source = require('vinyl-source-stream'),
        buffer = require('vinyl-buffer');

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
        scripts: 'main-scripts',
        globalLess: 'global-less',
        globalModuleLess: 'global-module-less',
        lessFiles: 'main-less-styles'
    };

    var browserifyOptions = {
        debug: true
    };

    var formatters = includer.formatters;

    that.registerTasks = function () {

        gulp.task('dev-clean', function (callback) {

            del.sync([
                output.value() + '/*',
                output.value() + '/*/'
            ], {force: true});

            callback();
        });

        gulp.task('dev-init', function () {

            includer.createIncludeLog();

            includer.copyIndex();
        });

        gulp.task('dev-include-' + constants.thirdPartyScripts, function () {

            includer.includeDirectory(
                constants.thirdPartyScripts,
                includes.scripts,
                includes.basePath,
                formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.thirdPartyStyles, function () {

            includer.includeDirectory(
                constants.thirdPartyStyles,
                includes.styles,
                includes.basePath,
                formatters.styleFormatter
            );
        });

        gulp.task('dev-include-' + constants.initFile, function () {

            includer.includeFile(
                constants.initFile,
                appBuilder.initFile,
                appBuilder.appPath(),
                formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.moduleHeaders, function () {

            var files = glob.sync(appBuilder.moduleHeaders);

            if (files.length) {

                includer.includeModuleFiles(
                    constants.moduleHeaders,
                    files,
                    formatters.scriptFormatter
                );
            }
        });

        gulp.task('dev-include-' + constants.moduleConstants, function () {

            var files = glob.sync(appBuilder.moduleConstants);

            if (files.length) {

                includer.includeModuleFiles(
                    constants.moduleConstants,
                    files,
                    formatters.scriptFormatter
                );
            }
        });

        gulp.task('dev-include-' + constants.moduleLibraries, function () {

            var files = glob.sync(appBuilder.moduleLibraries);

            if (files.length) {

                includer.includeModuleFiles(
                    constants.moduleLibraries,
                    files,
                    formatters.scriptFormatter
                );
            }
        });

        gulp.task('dev-include-' + constants.routeConfig, function () {

            includer.includeFile(
                constants.routeConfig,
                appBuilder.routeConfig,
                appBuilder.appPath(),
                formatters.scriptFormatter
            );
        });

        gulp.task('dev-browserify', function () {

            var files = glob.sync(appBuilder.npmModuleFiles);

            if (files.length) {

                var fileName = constants.browserified + '.js';

                includer.logInclude(constants.browserified, [fileName], formatters.scriptFormatter);

                return browserify(files, browserifyOptions)
                    .bundle()
                    .pipe(source(fileName))
                    .pipe(buffer())
                    .pipe(gulp.dest(output.value()));
            }
        });

        gulp.task('dev-include-' + constants.globalScripts, function () {

            var files = glob.sync(appBuilder.globalScripts);

            if (files.length) {

                includer.includeDirectory(
                    constants.globalScripts,
                    files,
                    appBuilder.appPath(),
                    formatters.scriptFormatter
                );
            }
        });

        gulp.task('dev-include-' + constants.globalLess, function () {

            var files = glob.sync(appBuilder.globalLess);

            if (files.length) {

                includer.includeDirectory(
                    constants.globalLess,
                    files,
                    appBuilder.appPath(),
                    formatters.styleFormatter,
                    ['less']
                );
            }
        });

        gulp.task('dev-include-' + constants.globalModuleScripts, function () {

            var files = glob.sync(appBuilder.globalModuleScripts);

            includer.includeSeparatedModuleFiles(
                constants.globalModuleScripts,
                files,
                appBuilder.modulesDir,
                formatters.scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.globalModuleLess, function () {

            var files = glob.sync(appBuilder.globalModuleLess);

            if (files.length) {

                includer.includeSeparatedModuleFiles(
                    constants.globalModuleLess,
                    files,
                    appBuilder.modulesDir,
                    formatters.styleFormatter,
                    ['less']
                );
            }
        });

        gulp.task('dev-include-' + constants.scripts, function () {

            var files = glob.sync(appBuilder.scripts);

            if (files.length) {

                includer.includeDirectory(
                    constants.scripts,
                    files,
                    appBuilder.modulesDir,
                    formatters.scriptFormatter
                );
            }
        });

        gulp.task('dev-include-' + constants.lessFiles, function () {

            var files = glob.sync(appBuilder.lessFiles);

            if (files.length) {

                includer.includeDirectory(
                    constants.lessFiles,
                    files,
                    appBuilder.modulesDir,
                    formatters.styleFormatter,
                    ['less', 'url-resolve']
                );
            }
        });

        gulp.task('dev-process-includes', function (callback) {

            runner.run(includer.readIncludes(), output.value())
                .then(function (newIncludes) {

                    includer.writeIncludes(newIncludes);

                }).then(callback);
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
    };

    return that;
}

module.exports = {
    instance: devBuildSystem
};