"use strict";

let path = require('path');

let separatorRegex = new RegExp('\\' + path.sep, 'g');

module.exports = function (pathToProcess) {

    if (pathToProcess === '') {

        return '';
    }

    if (!pathToProcess) {

        throw new Error('The path is invalid.');
    }

    return pathToProcess.replace(separatorRegex, '/');
};