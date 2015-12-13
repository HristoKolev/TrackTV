'use strict';

const glob = require('glob-all').sync;

module.exports = function (appBuilder) {

    if (!appBuilder) {

        throw new Error('The app builder is invalid.');
    }

    let files = glob(appBuilder.templates);

    return files;
};