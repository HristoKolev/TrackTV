'use strict';

var path = require('path'),
    gulp = require('gulp'),
    savefile = require('gulp-savefile');

function runner(includes, output) {

    var that = Object.create(null);

    function getFiles(include) {

        var files = include.files.slice();

        for (var i = 0; i < files.length; i += 1) {

            files[i] = path.join(output.value(), files[i]);
        }

        return files;
    }

    function getSourceStream(files) {

        return gulp.src(files);
    }

    var tasks = {};

    that.registerTask = function (name, task) {

        tasks[name] = task;
    };

    that.run = function () {

        for (var i = 0; i < includes.length; i += 1) {

            var include = includes[i];

            var taskNames = include.tasks;

            var stream = getSourceStream(getFiles(include));

            for (var j = 0; j < taskNames.length; j += 1) {

                var task = tasks[taskNames[j]];

                task.run(stream);
            }

            stream.pipe(savefile());
        }

    };

    return that;
}

module.exports = {
    instance: runner
};