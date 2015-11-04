'use strict';

function devBuildSystem(appBuilder, output, appStream, includes) {

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

    // custom modules
    var fillContent = require('./fill-content').external,
        listScripts = require('./list-resources'),
        copyFiles = require('./copyFiles');

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
    };

    var browserifyOptions = {
        debug: true,
        entries: glob.sync(appBuilder.npmModuleFiles)
    };

    // paths

    var outputIndex = output('index.html'),
        includeLog = output('includes.json');

    // logic

    var scriptFormatter = function(resourcePath) {

        return '<script src="' + resourcePath + '"></script>';
    };

    var styleFormatter = function(resourcePath) {

        return '<link rel="stylesheet" href="' + resourcePath + '">';
    };

    var formatterNames = {
        script: 'script',
        style: 'style'
    };

    function removeBaseDir(files, baseDir) {

        files = files.slice();

        for (var i = 0; i < files.length; i += 1) {

            if (files[i].indexOf(baseDir) !== 0) {

                throw Error('This file does not start with the provided base directory. baseDir: ' + baseDir + '; fileName: ' + files[i]);
            }

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

    function separateModuleFile(fileName) {

        var ext = path.extname(fileName);
        var base = path.basename(fileName, ext);

        var moduleName = path.basename(path.dirname(fileName));

        return path.join(moduleName, base + ext);
    }

    function getFormatterName(formatter) {

        var formatterName;

        if (formatter === scriptFormatter) {

            formatterName = formatterNames.script;
        }
        else if (formatter === styleFormatter) {

            formatterName = formatterNames.style;
        } else {

            throw Error('Unknown formatter: ' + formatter);
        }

        return formatterName;
    }

    function getFormatter(name) {

        if (name === formatterNames.script) {

            return scriptFormatter;
        }
        else if (name === formatterNames.style) {

            return styleFormatter;
        } else {

            throw Error('Unknown formatter name: ' + name);
        }
    }

    function logIncludes(name, files, formatter) {

        var obj = JSON.parse(fs.readFileSync(includeLog.value()));

        obj[name] = {
            files: files,
            formatter: getFormatterName(formatter)
        };

        fs.writeFileSync(includeLog.value(), JSON.stringify(obj, null, '\t'));
    }

    function copyIndex() {

        copyFiles.copy(appBuilder.indexFile, output.value());
    }

    function readIncludes() {

        var obj = JSON.parse(fs.readFileSync(includeLog.value()));

        Object.keys(obj).forEach(function(index) {

            obj[index].formatter = getFormatter(obj[index].formatter);
        });

        return obj;
    }

    function updateIncludes() {

        copyIndex();

        var includes = readIncludes();

        Object.keys(includes).forEach(function(index) {

            var include = includes[index];

            fillContent(outputIndex.value(), index, listScripts(include.files, include.formatter));
        });
    }

    function injectApplicationFiles(placeholder, files, formatter) {

        files = removeBaseDir(files, output.value());

        logIncludes(placeholder, files, formatter);
    }

    function includeDirectory(name, list, baseDir, formatter) {

        var newList = copyFiles.copyStructure(list, output(name).value(), baseDir);

        injectApplicationFiles(name, newList, formatter);
    };

    function includeFile(placeholder, file, baseDir, formatter) {

        var newList = copyFiles.copyStructure([file], output.value(), baseDir);

        injectApplicationFiles(placeholder, newList, formatter);
    };

    function includeModuleFiles(name, list, formatter) {

        var newList = copyFiles.copy(list, output(name).value(), renameModuleFile);

        injectApplicationFiles(name, newList, formatter);
    }

    function includeSeparatedModuleFiles(name, list, baseDir, formatter) {

        var newList = copyFiles.copyStructure(list, output(name).value(), baseDir, separateModuleFile);

        injectApplicationFiles(name, newList, formatter);
    }

    // tasks

    that.registerTasks = function() {

        gulp.task('dev-clean', function(callback) {

            del.sync([
                output.value() + '/*',
                output.value() + '/*/'
            ], { force: true });

            callback();
        });

        gulp.task('dev-init', function() {

            fs.writeFileSync(includeLog.value(), '{}');

            copyIndex();
        });

        gulp.task('dev-include-' + constants.thirdPartyScripts, function() {

            return includeDirectory(
                constants.thirdPartyScripts,
                includes.scripts,
                includes.baseDir,
                scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.thirdPartyStyles, function() {

            return includeDirectory(
                constants.thirdPartyStyles,
                includes.styles,
                includes.baseDir,
                styleFormatter
            );
        });

        gulp.task('dev-include-' + constants.initFile, function() {

            return includeFile(
                constants.initFile,
                appBuilder.initFile,
                appBuilder.appPath(),
                scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.moduleHeaders, function() {

            return includeModuleFiles(
                constants.moduleHeaders,
                glob.sync(appBuilder.moduleHeaders),
                scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.moduleConstants, function() {

            return includeModuleFiles(
                constants.moduleConstants,
                glob.sync(appBuilder.moduleConstants),
                scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.moduleLibraries, function() {

            return includeModuleFiles(
                constants.moduleLibraries,
                glob.sync(appBuilder.moduleLibraries),
                scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.routeConfig, function() {

            return includeFile(
                constants.routeConfig,
                appBuilder.routeConfig,
                appBuilder.appPath(),
                scriptFormatter
            );
        });

        gulp.task('dev-browserify', function() {

            var fileName = constants.browserified + '.js';

            logIncludes(constants.browserified, [fileName], scriptFormatter);

            return browserify(browserifyOptions)
                .bundle()
                .pipe(source(fileName))
                .pipe(buffer())
                .pipe(output.destStream());
        });

        gulp.task('dev-include-' + constants.globalScripts, function() {

            return includeDirectory(
                constants.globalScripts,
                glob.sync(appBuilder.globalScripts),
                appBuilder.appPath(),
                scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.globalModuleScripts, function() {

            return includeSeparatedModuleFiles(
                constants.globalModuleScripts,
                glob.sync(appBuilder.globalModuleScripts),
                appBuilder.modulesDir,
                scriptFormatter
            );
        });

        gulp.task('dev-include-' + constants.scripts, function() {

            return includeDirectory(
               constants.scripts,
               glob.sync(appBuilder.scripts),
               appBuilder.modulesDir,
               scriptFormatter
           );
        });

        gulp.task('dev-update-includes', function() {

            updateIncludes();
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