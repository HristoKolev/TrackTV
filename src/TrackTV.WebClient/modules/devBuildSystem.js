'use strict';

function devBuildSystem(appBuilder, output, appStream, includes) {

    var that = Object.create(null);

    // modules
    var gulp = require('gulp'),
        del = require('del'),
        glob = require('glob'),
        path = require('path');

    // custom modules
    var fillContent = require('./fill-content').external,
        listScripts = require('./list-resources'),
        fsCopy = require('./preserveFileStructureCopy');

    // paths
    var libPath = output('/lib'),
        libScriptsPath = libPath('/scripts'),
        libTemplatesPath = libPath('/templates'),
        libCssPath = libPath('/styles'),
        libFontsPath = libPath('/fonts');

    var mergedPath = output('/merged');

    ////////////////////

    var constants = {
        thirdPartyScripts: 'third-party-scripts',
        thirdPartyStyles: 'third-party-styles',
        initFile: 'init-file',
        moduleHeaders: 'module-headers',
        moduleConstants: 'module-constants',
        moduleLibraries: 'module-libraries',
        routeConfig: 'route-config'
    };

    // paths

    var outputIndex = output('index.html');

    var scriptFormatter = function (resourcePath) {

        return '<script src="' + resourcePath + '"></script>';
    };

    var styleFormatter = function (resourcePath) {

        return '<link rel="stylesheet" href="' + resourcePath + '">';
    };

    function removeBaseDir(files, baseDir) {

        files = files.slice();

        for (var i = 0; i < files.length; i += 1) {

            files[i] = files[i].slice(baseDir.length);
        }

        return files;
    }

    function renameModuleFile(fileName) {

        var ext = path.extname(fileName);
        var base = path.basename(fileName, ext);

        var moduleName = path.basename(path.dirname(fileName));

        return path.join(moduleName + '-' + base + ext);
    };

    function injectFiles(placeholder, files, formatter) {

        fillContent(outputIndex.value(), placeholder, listScripts(files, formatter));
    }

    function injectApplicationFiles(placeholder, files, formatter) {

        fillContent(outputIndex.value(), placeholder, listScripts(removeBaseDir(files, output.value()), formatter));
    }

    function includeDirectory(name, list, baseDir, formatter) {

        var newList = fsCopy(list, output(name).value(), baseDir);

        injectApplicationFiles(name, newList, formatter);

    };

    function includeFile(placeholder, file, baseDir, formatter) {

        var newList = fsCopy([file], output.value(), baseDir);

        injectApplicationFiles(placeholder, newList, formatter);

    };

    function includeModuleFiles(name, list, formatter) {

        var newList = fsCopy.simple(list, output(name).value(), renameModuleFile);

        injectApplicationFiles(name, newList, formatter);
    }

    ////////////////////

    that.registerTasks = function () {

        gulp.task('dev-clean', function (callback) {

            del.sync([
                output.value() + '/*',
                output.value() + '/*/'
            ], { force: true });

            callback();
        });

        gulp.task('dev-index', function () {

            return appStream.indexFileStream()
                .pipe(output.destStream());
        });

        gulp.task('dev-include-' + constants.thirdPartyScripts, function () {

            return includeDirectory(constants.thirdPartyScripts, includes.scripts, includes.baseDir, scriptFormatter);
        });

        gulp.task('dev-include-' + constants.thirdPartyStyles, function () {

            return includeDirectory(constants.thirdPartyStyles, includes.styles, includes.baseDir, styleFormatter);
        });

        gulp.task('dev-include-' + constants.initFile, function () {

            return includeFile(constants.initFile, appBuilder.initFile, appBuilder.appPath(), scriptFormatter);
        });

        gulp.task('dev-include-' + constants.moduleHeaders, function () {

            return includeModuleFiles(constants.moduleHeaders, glob.sync(appBuilder.moduleHeaders), scriptFormatter);
        });

        gulp.task('dev-include-' + constants.moduleConstants, function () {

            return includeModuleFiles(constants.moduleConstants, glob.sync(appBuilder.moduleConstants), scriptFormatter);
        });

        gulp.task('dev-include-' + constants.moduleLibraries, function () {

            return includeModuleFiles(constants.moduleLibraries, glob.sync(appBuilder.moduleLibraries), scriptFormatter);
        });

        gulp.task('dev-include-' + constants.routeConfig, function () {

            return includeFile(constants.routeConfig, appBuilder.routeConfig, appBuilder.appPath(), scriptFormatter);
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

        //gulp.task('dev-browserify', function () {

        //    return appStream.browserifyStream()
        //        .pipe(libScriptsPath.destStream());
        //});

    };

    return that;
}

module.exports = {
    instance: devBuildSystem
};