'use strict';

var gutil = require('gulp-util'),
    through = require('through2'),
    fs = require('fs');

var pluginName = 'fill-content';

function commentPlaceholder(placeholder) {

    return new RegExp('<!--\\s*?' + placeholder + '\\s*?-->', 'g');
}

function replaceContent(destinationFile, placeholder, replacement) {

    var regex = commentPlaceholder(placeholder);

    var content = fs.readFileSync(destinationFile).toString();

    content = content.replace(regex, replacement);

    fs.writeFileSync(destinationFile, content);
}

function stream(destinationFile, placeholder) {

    function error(obj, message) {

        obj.emit('error', new gutil.PluginError(pluginName, message));
    }

    return through.obj(function (file, enc, callback) {

        if (file.isNull()) {
            this.push(file);
            return callback();
        }

        if (file.isStream()) {

            error(this, 'Streaming is not supported');
            return callback();
        }

        if (file.isBuffer()) {

            replaceContent(destinationFile, placeholder, file.contents);

            return callback(null, file);
        }
    });
}

module.exports = stream;

module.exports.external = replaceContent;