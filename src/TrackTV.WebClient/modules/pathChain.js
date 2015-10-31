'use strict';

var path = require('path'),
    gulp = require('gulp');

function pathChain(basePath) {

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