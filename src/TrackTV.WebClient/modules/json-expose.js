'use strict';

var path = require('path');
var fs = require('fs');

function wrapScript(name, jsonString) {

    return '<script>window["' + name + '"] = ' + jsonString + ';</script>';
}

function expose(name, paths) {

    if (!name) {

        throw new Error('The name is invalid.');
    }

    if (!paths) {

        throw new Error('The paths are invalid.');
    }

    if (!Array.isArray(paths)) {

        throw new Error('The paths is not an array.');
    }

    var jsonObject = {};

    for (var i = 0; i < paths.length; i += 1) {

        var fileName = paths[i];

        var fileContent = fs.readFileSync(fileName).toString();

        var propertyName = path.basename(fileName, path.extname(fileName));

        jsonObject[propertyName] = JSON.parse(fileContent);
    }

    var jsonString = JSON.stringify(jsonObject);

    return wrapScript(name, jsonString);
}

module.exports = expose;