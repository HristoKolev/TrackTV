'use strict';

var urlResolve = require('../plugins/urlResolve.plugin');

module.exports = {
    name: 'url-resolve',
    task: function (stream, context) {

        return stream.pipe(urlResolve(context));
    }
};