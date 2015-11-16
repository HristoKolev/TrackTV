'use strict';

var less = require('gulp-less');

module.exports = {
    name: 'less',
    task: function (stream) {

        return stream.pipe(less());
    }
};