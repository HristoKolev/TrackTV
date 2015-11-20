'use strict';

var path = require('path'),
    validator = require('validator');

var linuxStylePath = require('./linuxStylePath');

var validatorOptions = {
    allow_protocol_relative_urls: true
};

function isAbsolute(p) {

    return path.normalize(p) === path.resolve(p);
}

function getModuleInfo(outputPath, filePath) {

    var relativeFilePath = path.relative(path.resolve(outputPath), filePath);

    var parts = relativeFilePath.split(path.sep);

    if (parts.length < 4) {

        throw new Error('Invalid relative file path: ' + relativeFilePath + '; Number of path parts: ' + parts.length +
            '; required minimum of 4 for the format "$include_name$/$module_name$/**/$submodule_name$/$file_name$"');

    }

    parts.pop();

    var submoduleName = parts.pop();

    parts.reverse();

    parts.pop();

    var moduleName = parts.pop();

    return {
        moduleName,
        submoduleName
    };
}

function getRelativePath(outputPath, filePath, relativeResourcePath) {

    var fileDirPath = path.dirname(path.relative(path.resolve(outputPath), filePath));

    return path.relative(fileDirPath, relativeResourcePath);
}

module.exports = function (outputPath, filePath, resourcePath) {

    if (!outputPath) {

        throw new Error('The output path is invalid');
    }

    if (!filePath) {

        throw new Error('The file path is invalid');
    }

    if (!isAbsolute(filePath)) {

        throw new Error('The file path is not absolute');
    }

    var absoluteOutputPath = path.resolve(outputPath);

    if (!filePath.startsWith(absoluteOutputPath)) {

        throw new Error('The file is not in the output directory. file path: ' + filePath + '; output path: ' + absoluteOutputPath);
    }

    if (!resourcePath) {

        throw new Error('The resource path is invalid');
    }

    if (validator.isURL(resourcePath, validatorOptions) || isAbsolute(resourcePath)) {

        return resourcePath;
    }

    var result;

    var contentPath = 'content/';

    if (resourcePath.startsWith(contentPath)) {

        var info = getModuleInfo(outputPath, filePath);

        var relativeResource = path.relative(contentPath, resourcePath);

        result = path.join(contentPath, info.moduleName, info.submoduleName, relativeResource);
    }
    else {

        var resource = resourcePath;

        var globalContentPath = 'global_content/';

        if (!resource.startsWith(globalContentPath)) {

            resource = path.join(globalContentPath, resource);
        }

        result = path.join(resource);
    }

    return linuxStylePath(getRelativePath(outputPath, filePath, result));
};