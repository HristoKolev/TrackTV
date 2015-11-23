'use strict';

let path = require('path'),
    fs = require('fs');

function wrapScript(name, jsonString) {

    return `<script>window["${ name }"] = ${ jsonString };</script>`;
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

    let jsonObject = {};

    for (let i = 0; i < paths.length; i += 1) {

        let fileName = paths[i];

        let fileContent = fs.readFileSync(fileName).toString();

        let propertyName = path.basename(fileName, path.extname(fileName));

        jsonObject[propertyName] = JSON.parse(fileContent);
    }

    let jsonString = JSON.stringify(jsonObject);

    return wrapScript(name, jsonString);
}

module.exports = expose;