'use strict';

var path = require('path'),
    gulp = require('gulp'),
    savefile = require('gulp-savefile');

function runner(output) {

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

    function getTask(name) {

        var task = tasks[name];

        if (!task) {

            throw new Error('There is no task with that name. Name: ' + name);
        }

        return task;
    }

    that.registerTask = function (name, task) {

        if (tasks[name]) {

            throw new Error('There is already a task registered with that name. Name: ' + name);
        }

        tasks[name] = task;
    };

    that.run = function (includes) {

        for (var i = 0; i < includes.length; i += 1) {

            var include = includes[i];

            var stream = getSourceStream(getFiles(include));

            for (var j = 0; j < include.tasks.length; j += 1) {

                var name = include.tasks[j];

                var task = getTask(name);

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