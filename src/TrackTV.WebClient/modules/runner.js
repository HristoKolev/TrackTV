'use strict';

var path = require('path'),
    gulp = require('gulp'),
    savefile = require('gulp-savefile'),
    task = require('gulp-task'),
    q = require('q');

var streamFiles = require('./plugins/streamFiles');

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

    function runTasks(stream, tasks) {

        for (var j = 0; j < tasks.length; j += 1) {

            var name = tasks[j];

            var task = getTask(name);

            task.run(stream);
        }
    }

    var basePath = path.resolve(output.value());

    function getPromise(include) {

        return task(function () {

            var stream = getSourceStream(getFiles(include));

            runTasks(stream, include.tasks);

            stream.pipe(savefile());

            stream.pipe(streamFiles(basePath, include.name));

            return stream;

        }).then(function () {

            return {
                name: include.name,
                files: streamFiles.get(include.name)
            };
        });
    }

    that.run = function (includes) {

        var promises = [];

        for (var i = 0; i < includes.length; i += 1) {

            var include = includes[i];

            promises.push(getPromise(include));
        }

        return q.all(promises).then(function (data) {

            var i;

            var updates = {};

            for (i = 0; i < data.length; i += 1) {

                var update = data[i];

                updates[update.name] = update.files;
            }

            for (i = 0; i < includes.length; i += 1) {

                var include = includes[i];

                var files = updates[include.name];

                if (files) {

                    include.files = files;
                }
            }

            return includes;
        });

    };

    return that;
}

module.exports = {
    instance: runner
};