'use strict';

const fileClass = Object.freeze({
    global: Symbol('global'),
    moduleGlobal: Symbol('moduleGlobal'),
    local: Symbol('local')
});

function getParts(filePath) {

    const parts = filePath.split(/[/\\]/g);

    parts.pop();

    return parts;
}

function getModuleName(filePath) {

    var parts = getParts(filePath);

    parts.reverse();

    let moduleName = parts.pop();

    return moduleName;
}

function getSubmoduleName(filePath) {

    var parts = getParts(filePath);

    let submoduleName = parts.pop();

    return submoduleName;
}

function parse(filePath) {

    if (!filePath) {

        throw new Error('The file path is invalid.');
    }

    let parts = getParts(filePath);

    if (parts.length === 0) {

        return {
            fileClass: fileClass.global
        };
    }
    else if (parts.length === 1) {

        return {
            fileClass: fileClass.moduleGlobal,
            moduleName: getModuleName(filePath)
        };
    }

    return {
        fileClass: fileClass.local,
        moduleName: getModuleName(filePath),
        submoduleName: getSubmoduleName(filePath)
    };
}

module.exports = parse;
module.exports.fileClass = fileClass;