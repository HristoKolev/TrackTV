'use strict';

var fs = require('fs-extra'),
    path = require('path');

function copyFiles() {

    var that = Object.create(null);

    function flatten(name) {

        return path.basename(name);
    }

    function removeBaseDir(fileName, baseDir) {

        if (fileName.indexOf(baseDir) !== 0) {

            throw Error('This file does not start with the provided base directory. baseDir: ' + baseDir + '; fileName: ' + fileName);
        }

        return fileName.slice(baseDir.length);

    };

    that.fsCopy = function (oldPath, newPath) {

        fs.copySync(oldPath, newPath);
    };

    that.copy = function (paths, targetDir, processPath) {

        if (!Array.isArray(paths)) {

            paths = [paths];
        }

        processPath = processPath || flatten;

        var returnPaths = [];

        for (var i = 0; i < paths.length; i += 1) {

            var newPath = path.join(targetDir, processPath(paths[i]));

            that.fsCopy(paths[i], newPath);

            returnPaths.push(newPath);
        }

        return returnPaths;
    };

    that.copyStructure = function (paths, targetDir, baseDir, processPath) {

        processPath = processPath || removeBaseDir;

        return that.copy(paths, targetDir, function (name) {

            return processPath(name, baseDir);
        });
    };

    return that;
}

module.exports = copyFiles();