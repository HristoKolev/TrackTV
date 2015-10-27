'use strict';

var globule = require('globule');
var path = require('path');
var fs = require('fs');

module.exports = function (name, pattern) {

    if (!name || !pattern) {

        throw Error('Parameters name and pattern must be valid.');
    }

    if (!(pattern instanceof Array)) {
        pattern = [pattern];
    }

    var paths = globule.find(pattern)
        .filter(function (filePath) {
            return path.extname(filePath).toLowerCase() === '.json';
        });

    var jsonObject = {};

    for (var index in paths) {

        var fileName = paths[index];

        var fileContent = fs.readFileSync(fileName).toString();
        var propertyName = path.basename(fileName, '.json');

        jsonObject[propertyName] = JSON.parse(fileContent);
    }

    var content = '<script> window.' + name + ' = ' + JSON.stringify(jsonObject) + '; </script>';

    return content;
};