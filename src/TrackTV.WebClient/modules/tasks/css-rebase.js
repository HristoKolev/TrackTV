'use strict';

const cssRebase = require('../plugins/css-rebase.js');

module.exports = {
    name: 'css-rebase',
    task: function (stream, context) {

        return stream.pipe(cssRebase(context));
    }
};