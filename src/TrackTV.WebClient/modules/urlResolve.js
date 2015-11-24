'use strict';

let path = require('path'),
    validator = require('validator');

let linuxStylePath = require('./linuxStylePath'),
    modulePathParse = require('./modulePathParse');

function isUrl(url) {

    let options = {
        allow_protocol_relative_urls: true // jshint ignore:line
    };

    return validator.isURL(url, options);
}

function isAbsolute(p) {

    return path.normalize(p) === path.resolve(p);
}

function getRelativePath(outputPath, filePath, relativeResourcePath) {

    let fileDirPath = path.dirname(path.relative(outputPath, filePath));

    return path.relative(fileDirPath, relativeResourcePath);
}

function removeExplicitPath(explicitPath, resourcePath) {

    let resource = resourcePath;

    if (resource.startsWith(explicitPath)) {

        resource = path.relative(explicitPath, resource);
    }

    return resource;
}

function getExplicitGlobalPath(baseName, basePath, resourcePath) {

    let resource = removeExplicitPath(basePath, resourcePath);

    return path.join(baseName, 'global', resource);
}

function getLocalPath(basePath, resourcePath, info) {

    let resource = removeExplicitPath(basePath, resourcePath);

    return path.join(basePath, 'local', info.moduleName, info.submoduleName, resource);
}

function getModulePath(basePath, resourcePath, info) {

    let resource = removeExplicitPath(basePath, resourcePath);

    return path.join(basePath, 'module', info.moduleName, resource);
}

function getGlobalPath(basePath, resourcePath) {

    let resource = removeExplicitPath(basePath, resourcePath);

    return path.join(basePath, 'global', resource);
}

function getStrategy(fileClass) {

    if (fileClass === modulePathParse.fileClass.local) {

        return getLocalPath;
    }
    else if (fileClass === modulePathParse.fileClass.moduleGlobal) {

        return getModulePath;

    }
    else if (fileClass === modulePathParse.fileClass.global) {

        return getGlobalPath;
    }
    else {
        throw new Error('Invalid file class: ' + fileClass);
    }
}

function rewritePath(outputPath, filePath, resourcePath) {

    let info = modulePathParse(path.relative(outputPath, filePath));

    const contentPath = 'content/';
    const globalContentPath = 'global_content/';

    const includePath = 'include/';
    const globalIncludePath = 'global_include/';

    if (resourcePath.startsWith(globalIncludePath)) {

        return getExplicitGlobalPath(includePath, globalIncludePath, resourcePath);
    }
    else if (resourcePath.startsWith(globalContentPath)) {

        return getExplicitGlobalPath(contentPath, globalContentPath, resourcePath);
    }

    let strategy = getStrategy(info.fileClass);

    if (resourcePath.startsWith(includePath)) {

        return strategy(includePath, resourcePath, info);
    }
    else {

        return strategy(contentPath, resourcePath, info);
    }
}

module.exports = function (outputPath, filePath, resourcePath) {

    if (!outputPath) {

        throw new Error('The output path is invalid');
    }

    if (!isAbsolute(outputPath)) {

        outputPath = path.resolve(outputPath);
    }

    if (!filePath) {

        throw new Error('The file path is invalid');
    }

    if (!isAbsolute(filePath)) {

        filePath = path.resolve(filePath);
    }

    if (!filePath.startsWith(outputPath)) {

        throw new Error('The file is not in the output directory. file path: ' + filePath + '; output path: ' + outputPath);
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