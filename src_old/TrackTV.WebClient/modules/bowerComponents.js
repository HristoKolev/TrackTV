'use strict';

let path = require('path'),
    linuxStylePath = require('../modules/linuxStylePath');

function bowerComponents(includes, basePath) {

    if (!includes) {

        throw new Error('The includes object is invalid.');
    }

    if (!basePath) {

        throw new Error('The base path is invalid.');
    }

    let that = Object.create(null);

    that.basePath = basePath;

    that.resolve = function (relativePath) {

        if (Array.isArray(relativePath)) {

            let paths = relativePath;

            for (let i = 0; i < paths.length; i += 1) {

                paths[i] = that.resolve(paths[i]);
            }

            return paths;
        }

        relativePath = relativePath || '';

        return linuxStylePath(path.join(that.basePath, relativePath));
    };

    for (let key of Object.keys(includes)) {

        that[key] = that.resolve(includes[key]);
    }

    return that;
}

module.exports = {
    instance: bowerComponents
};