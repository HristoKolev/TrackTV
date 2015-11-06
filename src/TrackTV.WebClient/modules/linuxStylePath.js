"use strict";

var path = require('path');

var separatorRegex = new RegExp('\\' + path.sep, 'g');

module.exports = function (pathToProcess) {

    if (pathToProcess === '') {

        return '';
    }

    if (!pathToProcess) {

        throw new Error('The path is invalid.');
    }

    return pathToProcess.replace(separatorRegex, '/');
};