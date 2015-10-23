﻿'use strict';

var gutil = require('gulp-util'),
    through = require('through2'),
    cheerio = require('cheerio'),
    fs = require('fs'),
    path = require('path'),
    mime = require('mime');

module.exports = function (options) {

    options = options || {};
    options.selectors = options.selectors || ['img'];
    options.attributes = options.attributes || ['src'];
    options.verbose = !!options.verbose;
    options.skipIfNotFound = true;

    if (!(options.selectors instanceof Array)) {
        options.selectors = [options.selectors];
    }

    if (!(options.attributes instanceof Array)) {
        options.attributes = [options.attributes];
    }

    function log (message) {

        if (options.verbose) {

            console.log(message);
        }
    }

    function isFunction (functionToCheck) {
        var getType = {};
        return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
    }

    function getBaseDirectory (file) {

        if (isFunction(options.resolveBaseDir)) {

            var result = options.resolveBaseDir(file);

            if (result) {

                return result;
            }
        }

        return options.baseDir || file.base;
    }

    function encodeResource (sourcePath, baseDirectory) {

        var isEncoded = sourcePath && sourcePath.indexOf('data:') === 0;

        if (isEncoded) {

            log('\t Already encoded: ' + sourcePath);
        }

        if (sourcePath && !isEncoded) {

            var resourceFilePath = path.join(baseDirectory, sourcePath);

            var mimeType = mime.lookup(resourceFilePath);

            if (mimeType != 'application/octet-stream') {

                log('\t Source: ' + sourcePath);

                var fileContent;

                try {

                    fileContent = fs.readFileSync(resourceFilePath);

                } catch (e) {

                    if (e.code === 'ENOENT' && options.skipIfNotFound) {

                        log('\t Skipping: ' + sourcePath);

                    } else {
                        throw e;
                    }
                }

                var encodedContent = new Buffer(fileContent).toString('base64');

                return 'data:' + mimeType + ';base64,' + encodedContent;
            }
        }
    }

// create a stream through which each file will pass
    return through.obj(function (file, enc, callback) {

        if (file.isNull()) {

            this.push(file);
            // do nothing if no contents
            return callback();
        }

        if (file.isStream()) {
            this.emit('error', new gutil.PluginError('gulp-img64', 'Streaming not supported'));
            return callback();
        }

        if (file.isBuffer()) {

            log('File: ' + file.path);

            var $ = cheerio.load(String(file.contents));

            for (var selectorIndex in options.selectors) {

                var selector = options.selectors[selectorIndex];

                $(selector).each(function (index, element) {

                    element = $(element);

                    for (var attributeIndex in options.attributes) {

                        var attribute = options.attributes[attributeIndex];

                        var source = element.attr(attribute);

                        if (source) {

                            var encodedSource = encodeResource(source, getBaseDirectory(file));

                            if (encodedSource) {

                                element.attr(attribute, encodedSource);
                            }
                        }
                    }
                });
            }

            var output = $.html();

            file.contents = new Buffer(output);

            return callback(null, file);
        }
    });
};