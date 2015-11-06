'use strict';

var path = require('path'),
    gulp = require('gulp');

function pathChain(basePath) {

    if (!basePath) {

        throw new Error('The base path is invalid.');
    }

    var that = function (newPath) {

        return pathChain(path.join(basePath, newPath));
    };

    that.value = function () {

        return basePath;
    };

    return that;
}

module.exports = {
    instance: pathChain
};