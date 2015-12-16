'use strict';

const cheerio = require('cheerio');

const pluginWrapper = require('./pluginWrapper'),
    urlResolve = require('../urlResolve');

module.exports = function (options) {

    if (!options) {

        throw new Error('The options are invalid.');
    }

    if (!options.outputPath) {

        throw new Error('The output path is invalid.');
    }

    return pluginWrapper('css-rebase', function (file) {

        const $ = cheerio.load(file.contents.toString());

        function rebase($elements, attributeName) {

            $elements.each(function (i, e) {

                let $element = $(e);

                let value = $element.attr(attributeName);

                if (value) {

                    let newValue = urlResolve(options.outputPath, file.path, value);

                    $element.attr(attributeName, newValue);
                }
            });
        }

        rebase($('img[src]'), 'src');
        rebase($('link[rel=icon]'), 'href');

        file.contents = new Buffer($.html());
    });
};