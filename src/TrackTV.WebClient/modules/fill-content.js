'use strict';

var gutil = require('gulp-util'),
    through = require('through2'),
    fs = require('fs');

var pluginName = 'fill-content';

module.exports = function (destinationFile, placeholder) {

    function error(message) {

        this.emit('error', new gutil.PluginError(pluginName, message));
    }

    function commentPlaceholder(placeholder) {

        return new RegExp('<!--\\s*?' + placeholder + '\\s*?-->', 'g');
    }

    return through.obj(function (file, enc, callback) {

        if (file.isNull()) {
            this.push(file);
            return callback();
        }

        if (file.isStream()) {

            error('Streaming is not supported');
            return callback();
        }

        if (file.isBuffer()) {

            var regex = commentPlaceholder(placeholder);

            var content = fs.readFileSync(destinationFile).toString();
            content = content.replace(regex, file.contents);
            fs.writeFileSync(destinationFile, content);

            return callback(null, file);
        }
    });
};