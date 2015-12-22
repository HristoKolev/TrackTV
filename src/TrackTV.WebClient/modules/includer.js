'use strict';

let path = require('path'),
    fs = require('fs');

function includer(indexFile, output) {

    if (!indexFile) {

        throw new Error('The index file is invalid.');
    }

    if (!output) {

        throw new Error('The output is invalid.');
    }

    let that = Object.create(null);

    let outputIndex = path.join(output, 'index.html'),
        includeLog = path.join(output, 'includes.json');

    // custom modules
    let fillContent = require('./fillContent'),
        listScripts = require('./list-resources'),
        copyFiles = require('./copyFiles');

    // logic

    that.formatters = {
        scriptFormatter: 'script',
        styleFormatter: 'style',
        none: 'none'
    };

    function removeBaseDir(files, baseDir) {

        files = files.slice();

        for (let i = 0; i < files.length; i += 1) {

            if (files[i].indexOf(baseDir) !== 0) {

                throw new Error('This file does not start with the provided base directory. baseDir: ' + baseDir + '; fileName: ' + files[i]);
            }

            files[i] = path.relative(baseDir, files[i]);
        }

        return files;
    }

    function getFormatter(name) {

        let formattersByName = {
            [that.formatters.scriptFormatter]: resourcePath => `<script src="${ resourcePath }"></script>`,
            [that.formatters.styleFormatter]: resourcePath  => `<link rel="stylesheet" href="${ resourcePath }">`,
            [that.formatters.none]: resourcePath => resourcePath
        };

        let formatter = formattersByName[name];

        if (!formatter) {

            throw new Error('Unknown formatter name: ' + name);
        }

        return formatter;
    }

    function addFormatter(includes) {

        for (let i = 0; i < includes.length; i += 1) {

            includes[i].formatter = getFormatter(includes[i].formatter);
        }

        return includes;
    }

    that.readIncludes = function () {

        return JSON.parse(fs.readFileSync(includeLog));
    };

    that.writeIncludes = function (includes) {

        if (!includes) {

            throw new Error('The includes are invalid.');
        }

        if (!Array.isArray(includes)) {

            throw new Error('The includes are not an array.');
        }

        let jsonString = JSON.stringify(includes, null, '\t');

        fs.writeFileSync(includeLog, jsonString);
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

        let includes = that.readIncludes();

        includes.push({
            name: name,
            files: files,
            formatter: formatter,
            tasks: tasks
        });

        that.writeIncludes(includes);
    };

    function injectApplicationFiles(placeholder, files, formatter, tasks) {

        files = removeBaseDir(files, output);

        that.logInclude(placeholder, files, formatter, tasks);
    }

    that.copyIndex = function () {

        copyFiles.copy(indexFile, output);
    };

    that.updateIncludes = function () {

        that.copyIndex();

        let includes = addFormatter(that.readIncludes());

        for (let include of includes) {

            fillContent(outputIndex, include.name, listScripts(include.files, include.formatter));
        }
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

        let newList = copyFiles.copyStructure([file], output, basePath);

        injectApplicationFiles(placeholder, newList, formatter, tasks);
    };

    that.includeDirectory = function (name, files, basePath, formatter, tasks, directoryName) {

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

        if (!directoryName) {

            directoryName = name;
        }

        let newList = copyFiles.copyStructure(files, path.join(output, directoryName), basePath);

        injectApplicationFiles(name, newList, formatter, tasks);
    };

    that.includeFiles = function (name, files, basePath, formatter, tasks) {

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

        let newList = copyFiles.copyStructure(files, output, basePath);

        injectApplicationFiles(name, newList, formatter, tasks);
    };

    return that;
}

module.exports = {
    instance: includer
};