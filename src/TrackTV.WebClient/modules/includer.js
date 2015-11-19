'use strict';

var path = require('path'),
    fs = require('fs');

function includer(indexFile, output) {

    if (!indexFile) {

        throw new Error('The index file is invalid.');
    }

    if (!output) {

        throw new Error('The output is invalid.');
    }

    var that = Object.create(null);

    var outputIndex = output('index.html'),
        includeLog = output('includes.json');

    // custom modules
    var fillContent = require('./fillContent'),
        listScripts = require('./list-resources'),
        copyFiles = require('./copyFiles');

    // logic

    var scriptFormatter = function (resourcePath) {

        return '<script src="' + resourcePath + '"></script>';
    };

    var styleFormatter = function (resourcePath) {

        return '<link rel="stylesheet" href="' + resourcePath + '">';
    };

    that.formatters = {
        scriptFormatter: 'script',
        styleFormatter: 'style'
    };

    function removeBaseDir(files, baseDir) {

        files = files.slice();

        for (var i = 0; i < files.length; i += 1) {

            if (files[i].indexOf(baseDir) !== 0) {

                throw new Error('This file does not start with the provided base directory. baseDir: ' + baseDir + '; fileName: ' + files[i]);
            }

            files[i] = path.relative(baseDir, files[i]);
        }

        return files;
    }

    function renameModuleFile(fileName) {

        var ext = path.extname(fileName);
        var base = path.basename(fileName, ext);

        var moduleName = path.basename(path.dirname(fileName));

        return path.join(moduleName + '-' + base + ext);
    }

    function separateModuleFile(fileName) {

        var ext = path.extname(fileName);
        var base = path.basename(fileName, ext);

        var moduleName = path.basename(path.dirname(fileName));

        return path.join(moduleName, base + ext);
    }

    function getFormatter(name) {

        var formattersByName = {};

        formattersByName[that.formatters.scriptFormatter] = scriptFormatter;
        formattersByName[that.formatters.styleFormatter] = styleFormatter;

        var formatter = formattersByName[name];

        if (!formatter) {

            throw new Error('Unknown formatter name: ' + name);
        }

        return formatter;

    }

    function addFormatter(includes) {

        for (var i = 0; i < includes.length; i += 1) {

            includes[i].formatter = getFormatter(includes[i].formatter);
        }

        return includes;
    }

    that.readIncludes = function () {

        return JSON.parse(fs.readFileSync(includeLog.value()));
    };

    that.writeIncludes = function (includes) {

        if (!includes) {

            throw new Error('The includes are invalid.');
        }

        if (!Array.isArray(includes)) {

            throw new Error('The includes are not an array.');
        }

        var jsonString = JSON.stringify(includes, null, '\t');

        fs.writeFileSync(includeLog.value(), jsonString);
    };

    that.createIncludeLog = function () {

        that.writeIncludes([]);
    };

    that.logInclude = function (name, files, formatter, tasks) {

        if (!name) {

            throw new Error('The name is invalid.');
        }

        if (!files) {

            throw new Error('The files argument is invalid.');
        }

        if (!Array.isArray(files)) {

            throw new Error('The files argument is not an array.');
        }

        if (!formatter) {

            throw new Error('The formatter is invalid.');
        }

        if (!tasks) {

            tasks = [];
        }

        if (!Array.isArray(tasks)) {

            throw new Error('The tasks argument is not an array.');
        }

        var includes = that.readIncludes();

        includes.push({
            name: name,
            files: files,
            formatter: formatter,
            tasks: tasks
        });

        that.writeIncludes(includes);
    };

    function injectApplicationFiles(placeholder, files, formatter, tasks) {

        files = removeBaseDir(files, output.value());

        that.logInclude(placeholder, files, formatter, tasks);
    }

    that.copyIndex = function () {

        copyFiles.copy(indexFile, output.value());
    };

    that.updateIncludes = function () {

        that.copyIndex();

        var includes = addFormatter(that.readIncludes());

        for (var i = 0; i < includes.length; i += 1) {

            var include = includes[i];

            fillContent(outputIndex.value(), include.name, listScripts(include.files, include.formatter));
        }
    };

    that.includeDirectory = function (name, files, basePath, formatter, tasks) {

        if (!name) {

            throw new Error('The name is invalid.');
        }

        if (!files) {

            throw new Error('The files argument is invalid.');
        }

        if (!Array.isArray(files)) {

            throw new Error('The files argument is not an array');
        }

        if (!basePath) {

            throw new Error('The base path is invalid.');
        }

        if (!formatter) {

            throw new Error('The formatter is invalid.');
        }

        if (!tasks) {

            tasks = [];
        }

        if (!Array.isArray(tasks)) {

            throw new Error('The tasks argument is not an array.');
        }

        var newList = copyFiles.copyStructure(files, output(name).value(), basePath);

        injectApplicationFiles(name, newList, formatter, tasks);
    };

    that.includeFile = function (placeholder, file, basePath, formatter, tasks) {

        if (!placeholder) {

            throw new Error('The placeholder is invalid.');
        }

        if (!file) {

            throw new Error('The file is invalid.');
        }

        if (!basePath) {

            throw new Error('The base path is invalid.');
        }

        if (!formatter) {

            throw new Error('The formatter is invalid.');
        }

        if (!tasks) {

            tasks = [];
        }

        if (!Array.isArray(tasks)) {

            throw new Error('The tasks argument is not an array.');
        }

        var newList = copyFiles.copyStructure([file], output.value(), basePath);

        injectApplicationFiles(placeholder, newList, formatter, tasks);
    };

    that.includeModuleFiles = function (name, files, formatter, tasks) {

        if (!name) {

            throw new Error('The name is invalid.');
        }

        if (!files) {

            throw new Error('The files argument is invalid.');
        }

        if (!Array.isArray(files)) {

            throw new Error('The files argument is not an array');
        }

        if (!formatter) {

            throw new Error('The formatter is invalid.');
        }

        if (!tasks) {

            tasks = [];
        }

        if (!Array.isArray(tasks)) {

            throw new Error('The tasks argument is not an array.');
        }

        var newList = copyFiles.copy(files, output(name).value(), renameModuleFile);

        injectApplicationFiles(name, newList, formatter, tasks);
    };

    that.includeSeparatedModuleFiles = function (name, files, basePath, formatter, tasks) {

        if (!name) {

            throw new Error('The name is invalid.');
        }

        if (!files) {

            throw new Error('The files argument is invalid.');
        }

        if (!Array.isArray(files)) {

            throw new Error('The files argument is not an array');
        }

        if (!basePath) {

            throw new Error('The base path is invalid.');
        }

        if (!formatter) {

            throw new Error('The formatter is invalid.');
        }

        if (!tasks) {

            tasks = [];
        }

        if (!Array.isArray(tasks)) {

            throw new Error('The tasks argument is not an array.');
        }

        var newList = copyFiles.copyStructure(files, output(name).value(), basePath, separateModuleFile);

        injectApplicationFiles(name, newList, formatter, tasks);
    };

    return that;
}

module.exports = {
    instance: includer
};