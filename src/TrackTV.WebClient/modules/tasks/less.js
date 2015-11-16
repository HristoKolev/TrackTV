'use strict';

var less = require('gulp-less');

function task(stream) {

    return stream.pipe(less());
}

module.exports = {
    name: 'less',
    task: task
};