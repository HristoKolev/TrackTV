'use strict';

var path = require('path'),
    validator = require('validator');

function isAbsolute(p) {

    return path.normalize(p) === path.resolve(p);
}

module.exports = function (appPath, outputPath, filePath, resource) {

    if (isAbsolute(resource) && validator.isURL(resource)) {

        return resource;
    }

    var contentPath = 'content/';
    var globalContentPath = 'global_content/';

    var resourceFileName = path.basename(resource);

    if (resource.startsWith(contentPath)) {

        var modulePath = path.join(appPath, 'modules');
        var paths = path.dirname(filePath).split('/');

        var moduleName = path.relative(modulePath, filePath).split('/')[0];
        var submoduleName = paths[paths.length - 1];

        return path.join(outputPath, moduleName, submoduleName, contentPath, resourceFileName);
    }
    else if (resource.startsWith(globalContentPath)) {

        return path.join(outputPath, globalContentPath, resourceFileName);
    }
    else {
        throw new Error('Unknown resource path: ' + resource);
    }
};