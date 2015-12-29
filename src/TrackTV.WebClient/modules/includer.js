'use strict';

let path = require('path'),
    fs = require('fs');

let outputConfig = require('../config/outputConfig');

const fillContent = require('./fillContent'),
    listScripts = require('./list-resources'),
    copyFiles = require('./copyFiles');

let outputIndex = path.join(outputConfig.devPath, 'index.html'),
    includeLog = path.join(outputConfig.devPath, 'includes.json');

let formatters = {
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
        [formatters.scriptFormatter]: resourcePath => `<script src="${ resourcePath }"></script>`,
        [formatters.styleFormatter]: resourcePath  => `<link rel="stylesheet" href="${ resourcePath }">`,
        [formatters.none]: resourcePath => resourcePath
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

function readIncludes() {

    return JSON.parse(fs.readFileSync(includeLog));
}

function writeIncludes(includes) {

    if (!includes) {

        throw new Error('The includes are invalid.');
    }

    if (!Array.isArray(includes)) {

        throw new Error('The includes are not an array.');
    }

    let jsonString = JSON.stringify(includes, null, '\t');

    fs.writeFileSync(includeLog, jsonString);
}

function createIncludeLog() {

    writeIncludes([]);
}

function logInclude(name, files, formatter, tasks) {

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

    let includes = readIncludes();

    includes.push({
        name: name,
        files: files,
        formatter: formatter,
        tasks: tasks
    });

    writeIncludes(includes);
}

function injectApplicationFiles(placeholder, files, formatter, tasks) {

    files = removeBaseDir(files, outputConfig.devPath);

    logInclude(placeholder, files, formatter, tasks);
}

function updateIncludes() {

    let includes = addFormatter(readIncludes());

    for (let include of includes) {

        fillContent(outputIndex, include.name, listScripts(include.files, include.formatter));
    }
}

function copyAndIncludeFile(placeholder, file, basePath, formatter, tasks) {

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

    let newList = copyFiles.copyStructure([file], outputConfig.devPath, basePath);

    injectApplicationFiles(placeholder, newList, formatter, tasks);
}

function copyAndIncludeDirectory(name, files, basePath, formatter, tasks, directoryName) {

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

    let newList = copyFiles.copyStructure(files, path.join(outputConfig.devPath, directoryName), basePath);

    injectApplicationFiles(name, newList, formatter, tasks);
}

function copyAndIncludeFiles(name, files, basePath, formatter, tasks) {

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

    let newList = copyFiles.copyStructure(files, outputConfig.devPath, basePath);

    injectApplicationFiles(name, newList, formatter, tasks);
}

module.exports = {
    formatters,

    readIncludes,
    writeIncludes,
    createIncludeLog,
    logInclude,
    updateIncludes,

    copyAndIncludeFile,
    copyAndIncludeDirectory,
    copyAndIncludeFiles
};