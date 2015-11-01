'use strict';

var fs = require('fs-extra'),
    path = require('path');

function removeBaseDir(fileName, baseDir) {

    return fileName.slice(baseDir.length);
}

function preserveFileStructureCopy(files, target, baseDir, processNewFile) {

    var newFiles = [];

    for (var i = 0; i < files.length; i += 1) {

        var newFile = path.join(target, removeBaseDir(files[i], baseDir));

        if (processNewFile) {

            newFile = processNewFile(newFile);
        }

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