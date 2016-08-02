'use strict';

let path = require('path'),
    linuxStylePath = require('./linuxStylePath');

function isFunction(func) {

    let obj = {};

    return func && obj.toString.call(func) === '[object Function]';
}

function list(resources, formatter, basePath) {

    if (!resources) {

        throw new Error('The resources are invalid.');
    }

    if (!Array.isArray(resources)) {

        throw new Error('The resources object is not an array.');
    }

    if (!formatter) {

        throw new Error('The formatter is invalid.');
    }

    if (!isFunction(formatter)) {

        throw new Error('The formatter is not a function.');
    }

    basePath = basePath || '';

    if (typeof basePath !== 'string') {

        throw new Error('The base path is not a string.');
    }

    let lines = [];

    for (let resource of resources) {

        let resourcePath = linuxStylePath(path.join('./', basePath, resource));

        lines.push(formatter(resourcePath));
    }

    let separator = '\n';

    return lines.join(separator);
}

module.exports = list;