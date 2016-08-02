'use strict';

var gutil = require('gulp-util'),
    through = require('through2'),
    cheerio = require('cheerio'),
    fs = require('fs'),
    path = require('path'),
    mime = require('mime'),
    multimatch = require('multimatch');

var pluginName = 'gulp-embed-media';

module.exports = function (options) {

    options = options || {};

    processOptions(options);

    function processOptions(options) {

        options.selectors = options.selectors || ['img'];
        options.attributes = options.attributes || ['src'];
        options.verbose = !!options.verbose;
        options.skipIfNotFound = true;

        if (!Array.isArray(options.selectors)) {
            options.selectors = [options.selectors];
        }

        if (!Array.isArray(options.attributes)) {
            options.attributes = [options.attributes];
        }

        if (options.resourcePattern && !Array.isArray(options.resourcePattern)) {
            options.resourcePattern = [options.resourcePattern];
        }
    }

    function log(message) {

        if (options.verbose) {

            console.log(message);
        }
    }

    function error(obj, message) {

        obj.emit('error', new gutil.PluginError(pluginName, message));
    }

    function isFunction(functionToCheck) {
        var getType = {};
        return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
    }

    function getBaseDirectory(file) {

        if (isFunction(options.resolveBaseDir)) {

            var result = options.resolveBaseDir(file);

            if (result) {

                return result;
            }
        }

        return options.baseDir || file.base;
    }

    function encodeResource(sourcePath, baseDirectory) {

        var isEncoded = sourcePath && sourcePath.indexOf('data:') === 0;

        if (isEncoded) {

            log('\t Already encoded: ' + sourcePath);
        }

        if (sourcePath && !isEncoded) {

            var resourceFilePath = path.join(baseDirectory, sourcePath);

            var mimeType = mime.lookup(resourceFilePath);

            if (mimeType !== 'application/octet-stream') {

                log('\t Source: ' + sourcePath);

                var fileContent;

                try {

                    fileContent = fs.readFileSync(resourceFilePath);

                } catch (e) {

                    if (e.code === 'ENOENT') {

                        if (options.skipIfNotFound) {

                            log('\t Skipping: ' + sourcePath);

                        } else {

                            error(this, 'File not found. : ' + resourceFilePath);
                        }
                    } else {

                        error(this, e);
                    }

                }

                var encodedContent = new Buffer(fileContent).toString('base64');

                return 'data:' + mimeType + ';base64,' + encodedContent;
            }
        }
    }

    function shouldProcessResource(source) {

        if (!source) {
            return false;
        }

        if (options.resourcePattern && !(multimatch(source, options.resourcePattern).length)) {

            return false;
        }

        return true;
    }

    function processSelector($, selector, attributes, file) {

        $(selector).each(function (index, element) {

            element = $(element);

            for (var j = 0; j < attributes.length; j += 1) {

                var attribute = attributes[j];

                var source = element.attr(attribute);

                if (shouldProcessResource(source)) {

                    var encodedSource = encodeResource(source, getBaseDirectory(file));

                    if (encodedSource) {

                        element.attr(attribute, encodedSource);
                    }
                }
            }
        });
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

            log('File: ' + file.path);

            var $ = cheerio.load(String(file.contents));

            for (var i = 0; i < options.selectors.length; i += 1) {

                var selector = options.selectors[i];

                processSelector($, selector, options.attributes, file);
            }

            var output = $.html();
            file.contents = new Buffer(output);
            return callback(null, file);
        }
    });
};