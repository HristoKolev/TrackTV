'use strict';

let path = require('path');

function pathChain(basePath) {

    if (!basePath) {

        throw new Error('The base path is invalid.');
    }

    let that = function (newPath) {

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