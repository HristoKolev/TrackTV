'use strict';

var gutil = require('gulp-util'),
    through = require('through2'),
    fs = require('fs'),
    path = require('path');

var pluginName = 'streamFiles';

var record = [];

function reset() {

    record = [];
}

function recordFile(file) {

    record.push(file.path);
}

function streamFiles(basePath) {

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

            recordFile(file);

            return callback(null, file);
        }
    });
}

module.exports = streamFiles;
module.exports.reset = reset;
module.exports.record = function () {

    return record;
};

