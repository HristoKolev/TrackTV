'use strict';

var path = require('path'),
    linuxStylePath = require('./linuxStylePath');

module.exports = function (resourceArray, formatter, baseDir) {

    baseDir = baseDir || '';

    var lines = [];

    for (var i = 0; i < resourceArray.length; i += 1) {

        var resourcePath = linuxStylePath(path.join('./', baseDir, resourceArray[i]));

        lines.push(formatter(resourcePath));
    }

    return lines.join('\n');
};