'use strict';

let fs = require('fs-extra'),
    path = require('path');

function copyFiles() {

    let that = Object.create(null);

    function flatten(name) {

        return path.basename(name);
    }

    function removeBaseDir(fileName, baseDir) {

        if (fileName.indexOf(baseDir) !== 0) {

            throw new Error('This file does not start with the provided base directory. baseDir: ' + baseDir + '; fileName: ' + fileName);
        }

        return fileName.slice(baseDir.length);
    }

    that.copy = function (paths, targetDir, processPath) {

        if (!paths) {

            throw new Error('The paths are invalid. Paths: ' + paths);
        }

        if (Array.isArray(paths) && paths.length === 0) {

            throw new Error('The paths array is empty.');
        }

        if (!targetDir) {

            throw new Error('The target directory is invalid. targetDir: ' + targetDir);
        }

        if (!Array.isArray(paths)) {

            paths = [paths];
        }

        processPath = processPath || flatten;

        let returnPaths = [];

        for (let i = 0; i < paths.length; i += 1) {

            let processedPath = processPath(paths[i]);

            if (!processedPath) {

                throw new Error('The process function returned a falsy path.');
            }

            let newPath = path.join(targetDir, processedPath);

            fs.copySync(paths[i], newPath);

            returnPaths.push(newPath);
        }

        return returnPaths;
    };

    that.copyStructure = function (paths, targetDir, baseDir, processPath) {

        if (!baseDir) {

            throw new Error('The base directory is invalid.');
        }

        processPath = processPath || removeBaseDir;

        return that.copy(paths, targetDir, name => processPath(name, baseDir));
    };

    return that;
}

module.exports = copyFiles();