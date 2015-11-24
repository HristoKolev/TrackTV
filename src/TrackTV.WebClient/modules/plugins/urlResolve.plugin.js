'use strict';

const rework = require('rework'),
    reworkUrl = require('rework-plugin-url');

const pluginWrapper = require('./pluginWrapper'),
    urlResolve = require('../urlResolve');

module.exports = function (options) {

    if (!options) {

        throw new Error('The options are invalid.');
    }

    if (!options.outputPath) {

        throw new Error('The output path is invalid.');
    }

    return pluginWrapper('urlResolve', function (file) {

        var contents = file.contents.toString();

        var newContents = rework(contents).use(reworkUrl(url => urlResolve(options.outputPath, file.path, url))).toString();

        file.contents = new Buffer(newContents);
    });
};