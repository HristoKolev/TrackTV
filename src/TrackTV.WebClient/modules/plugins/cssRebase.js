'use strict';

var gutil = require('gulp-util'),
    through = require('through2'),
    fs = require('fs'),
    path = require('path');

var pluginName = 'cssRebase';

function cssRebase() {

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

            return callback(null, file);
        }
    });
}

module.exports = cssRebase;