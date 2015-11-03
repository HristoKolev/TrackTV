'use strict';

var fs = require('fs-extra'),
    path = require('path');

function removeBaseDir(fileName, baseDir) {

    if (fileName.indexOf(baseDir) !== 0) {

        throw Error('This file does not start with the provided base directory. baseDir: ' + baseDir + '; fileName: ' + fileName);
    }

    return fileName.slice(baseDir.length);
}

function preserveFileStructureCopy(files, target, baseDir, processNewFile) {

    var newFiles = [];

    processNewFile = processNewFile || removeBaseDir;

    for (var i = 0; i < files.length; i += 1) {

        var newFile = newFile = path.join(target, processNewFile(files[i], baseDir));

        fs.copySync(files[i], newFile);

        newFiles.push(newFile);
    }

    return newFiles;
}

module.exports = preserveFileStructureCopy;

module.exports.simple = function (files, target, processNewFile) {

    var newFiles = [];

    for (var i = 0; i < files.length; i += 1) {

        var newFile = path.join(target, processNewFile(files[i]));

        fs.copySync(files[i], newFile);

        newFiles.push(newFile);
    }

    return newFiles;
};