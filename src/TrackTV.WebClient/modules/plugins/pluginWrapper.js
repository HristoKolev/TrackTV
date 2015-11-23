'use strict';

const gutil = require('gulp-util'),
    through = require('through2');

function pluginWrapper(pluginName, fileHandler) {

    if (!pluginName) {

        throw new Error('The plugin name ios invalid.');
    }

    if (!fileHandler) {

        throw new Error('The file handler is invalid.');
    }

    function getErrorHandler(thisArg) {

        return function error(message) {

            thisArg.emit('error', new gutil.PluginError(pluginName, message));
        };
    }

    return through.obj(function (file, enc, callback) {

        const errorHandler = getErrorHandler(this);

        if (file.isNull()) {

            this.push(file);

            return callback();
        }

        if (file.isStream()) {

            errorHandler('Streaming is not supported.');

            return callback();
        }

        if (file.isBuffer()) {

            try {

                fileHandler(file, enc);

            } catch (e) {

                errorHandler(e);
            }

            return callback(null, file);
        }
    });
}

module.exports = pluginWrapper;