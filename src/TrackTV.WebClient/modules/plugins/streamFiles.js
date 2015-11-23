'use strict';

var gutil = require('gulp-util'),
    through = require('through2'),
    fs = require('fs'),
    path = require('path');

var pluginName = 'streamFiles';

var records = {};

function removeBaseDir(fileName, basePath) {

    if (fileName.indexOf(basePath) !== 0) {

        throw new Error('This file does not start with the provided base path. basePath: ' + basePath + '; fileName: ' + fileName);
    }

    fileName = path.join('./', fileName.slice(basePath.length));

    return fileName;
}

function getRecord(name) {

    if (!records[name]) {

        records[name] = [];
    }

    return records[name];
}

function recordFile(file, basePath, name) {

    var record = getRecord(name);

    record.push(removeBaseDir(file.path, basePath));
}

function streamFiles(basePath, name) {

    if (!basePath) {

        throw new Error('The base path is invalid');
    }

    return through.obj(function (file, enc, callback) {

        var that = this;

        function error(message) {

            that.emit('error', new gutil.PluginError(pluginName, message));
        }

        if (file.isNull()) {

            this.push(file);

            return callback();
        }

        if (file.isStream()) {

            error('Streaming is not supported');

            return callback();
        }

        if (file.isBuffer()) {

            recordFile(file, basePath, name);

            return callback(null, file);
        }
    });
}

module.exports = streamFiles;

module.exports.get = function (name) {

    return records[name] || [];
};