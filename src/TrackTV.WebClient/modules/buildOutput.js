'use strict';

var gulp = require('gulp'),
    path = require('path');

function buildOutput(basePath) {

    var that = function (newPath) {

        return buildOutput(path.join(basePath, newPath));
    };

    that.value = function () {

        return basePath;
    };

    that.destStream = function () {

        return gulp.dest(that.value());
    };

    return that;
}

module.exports = {
    instance: buildOutput
};