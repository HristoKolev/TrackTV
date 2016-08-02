'use strict';

const htmlRebase = require('../plugins/html-rebase');

module.exports = {
    name: 'html-rebase',
    task: function (stream, context) {

        return stream.pipe(htmlRebase(context));
    }
};