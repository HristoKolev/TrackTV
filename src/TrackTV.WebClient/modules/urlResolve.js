'use strict';

let path = require('path'),
    validator = require('validator');

let linuxStylePath = require('./linuxStylePath');

function isUrl(url) {

    let options = {
        allow_protocol_relative_urls: true // jshint ignore:line
    };

    return validator.isURL(url, options);
}

function isAbsolute(p) {

    return path.normalize(p) === path.resolve(p);
}

function getModuleInfo(outputPath, filePath) {

    let relativeFilePath = path.relative(path.resolve(outputPath), filePath);

    let parts = relativeFilePath.split(path.sep);

    if (parts.length < 4) {

        throw new Error('Invalid relative file path: ' + relativeFilePath + '; Number of path parts: ' + parts.length +
            '; required minimum of 4 for the pattern "$include_name$/$module_name$/**/$submodule_name$/$file_name$"');
    }

    parts.pop();

    let submoduleName = parts.pop();

    parts.reverse();

    parts.pop();

    let moduleName = parts.pop();

    return {
        moduleName,
        submoduleName
    };
}

function getRelativePath(outputPath, filePath, relativeResourcePath) {

    let fileDirPath = path.dirname(path.relative(path.resolve(outputPath), filePath));

    return path.relative(fileDirPath, relativeResourcePath);
}

function rewritePath(outputPath, filePath, resourcePath) {

    function getLocalPath(basePath) {

        let info = getModuleInfo(outputPath, filePath);

        let relativeResourcePath = path.relative(basePath, resourcePath);

        return path.join(basePath, info.moduleName, info.submoduleName, relativeResourcePath);
    }

    function getGlobalPath(basePath) {

        let resource = resourcePath;

        if (!resource.startsWith(basePath)) {

            resource = path.join(basePath, resource);
        }

        return path.join(resource);
    }

    const contentPath = 'content/';
    const globalContentPath = 'global_content/';

    const includesPath = 'include/';
    const globalIncludes = 'global_include/';

    if (resourcePath.startsWith(contentPath)) {

        return getLocalPath(contentPath);
    }
    else if (resourcePath.startsWith(includesPath)) {

        return getLocalPath(includesPath);
    }
    else if (resourcePath.startsWith(globalIncludes)) {

        return getGlobalPath(globalIncludes);
    }
    else {
        return getGlobalPath(globalContentPath);
    }
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

    let absoluteOutputPath = path.resolve(outputPath);

    if (!filePath.startsWith(absoluteOutputPath)) {

        throw new Error('The file is not in the output directory. file path: ' + filePath + '; output path: ' + absoluteOutputPath);
    }

    if (!resourcePath) {

        throw new Error('The resource path is invalid');
    }

    if (isUrl(resourcePath) || isAbsolute(resourcePath)) {

        return resourcePath;
    }

    let result = rewritePath(outputPath, filePath, resourcePath);

    return linuxStylePath(getRelativePath(outputPath, filePath, result));
};