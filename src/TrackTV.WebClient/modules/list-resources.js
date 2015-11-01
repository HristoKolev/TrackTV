'use strict';

var path = require('path');

var separatorRegex = new RegExp('\\' + path.sep, 'g');

module.exports = function (resourceArray, formatter, baseDir) {

    baseDir = baseDir || '';

    var lines = [];

    for (var i = 0; i < resourceArray.length; i += 1) {

        var resourcePath = path.join('./', baseDir, resourceArray[i]).replace(separatorRegex, '/');

        lines.push(formatter(resourcePath));
    }

    return lines.join('\n');
};