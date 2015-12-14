'use strict';

const path = require('path');

const glob = require('glob-all').sync,
    _ = require('underscore');

function parsePath(filePath) {

    let parts = filePath.split(/[\/\\]/);

    if (parts.length < 3) {

        throw new Error('The file path is not in a valid format. filePath: ' + filePath);
    }

    parts.pop();

    let submoduleName = parts.pop();

    parts.reverse();

    let moduleName = parts.pop();

    return {
        moduleName,
        submoduleName
    };
}

module.exports = function (templateFilePaths, appPath, outputPath) {

    if (!templateFilePaths) {

        throw new Error('The template file paths argument is invalid.');
    }

    if (!Array.isArray(templateFilePaths)) {

        throw new Error('The template file paths argument is not an array.');
    }

    if (!appPath) {

        throw new Error('The app path is invalid.');
    }

    if (!outputPath) {

        throw new Error('The output path is invalid.');
    }

    let filePaths = _.map(templateFilePaths, p => path.relative(path.resolve(appPath), path.resolve(p)));

    let list = [];

    for (let filePath of filePaths) {

        let info = parsePath(filePath);

        let destinationPath = path.join(outputPath, 'templates', info.moduleName, info.submoduleName);

        list.push({
            targetPath: filePath,
            destinationPath
        });
    }

    return list;
};