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

    let outputIndex = output('index.html'),
        includeLog = output('includes.json');

    // custom modules
    let fillContent = require('./fillContent'),
        listScripts = require('./list-resources'),
        copyFiles = require('./copyFiles');

    // logic

    that.formatters = {
        scriptFormatter: 'script',
        styleFormatter: 'style'
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

    function getModuleInfo(fileName) {

        let extension = path.extname(fileName);
        let baseName = path.basename(fileName, extension);
        let moduleName = path.basename(path.dirname(fileName));

        return {
            extension: extension,
            baseName: baseName,
            moduleName: moduleName
        };
    }

    function renameModuleFile(fileName) {

        let moduleInfo = getModuleInfo(fileName);

        return path.join(moduleInfo.moduleName + '-' + moduleInfo.baseName + moduleInfo.extension);
    }

    function separateModuleFile(fileName) {

        let moduleInfo = getModuleInfo(fileName);

        return path.join(moduleInfo.moduleName, moduleInfo.baseName + moduleInfo.extension);
    }

    function getFormatter(name) {

        function scriptFormatter(resourcePath) {

            return `<script src="${ resourcePath }"></script>`;
        }

        function styleFormatter(resourcePath) {

            return `<link rel="stylesheet" href="${ resourcePath }">`;
        }

        let formattersByName = {
            [that.formatters.scriptFormatter]: scriptFormatter,
            [that.formatters.styleFormatter]: styleFormatter
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

        return JSON.parse(fs.readFileSync(includeLog.value()));
    };

    that.writeIncludes = function (includes) {

        if (!includes) {

            throw new Error('The includes are invalid.');
        }

        if (!Array.isArray(includes)) {

            throw new Error('The includes are not an array.');
        }

        let jsonString = JSON.stringify(includes, null, '\t');

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

        files = removeBaseDir(files, output.value());

        that.logInclude(placeholder, files, formatter, tasks);
    }

    that.copyIndex = function () {

        copyFiles.copy(indexFile, output.value());
    };

    that.updateIncludes = function () {

        that.copyIndex();

        let includes = addFormatter(that.readIncludes());

        for (let include of includes) {

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

        let newList = copyFiles.copyStructure(files, output(name).value(), basePath);

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

        let newList = copyFiles.copyStructure([file], output.value(), basePath);

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

        let newList = copyFiles.copy(files, output(name).value(), renameModuleFile);

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

        let newList = copyFiles.copyStructure(files, output(name).value(), basePath, separateModuleFile);

        injectApplicationFiles(name, newList, formatter, tasks);
    };

    return that;
}

module.exports = {
    instance: includer
};