'use strict';

var path = require('path'),
    gulp = require('gulp'),
    task = require('gulp-task'),
    q = require('q');

var streamFiles = require('./plugins/streamFiles');

function saveFile() {

    return gulp.dest(function (file) {

        return path.dirname(file.path);
    });
}

function source(files) {

    return gulp.src(files);
}

function runner() {

    var that = Object.create(null),
        tasks = {};

    that.use = function (module) {

        if (tasks[module.name]) {

            throw new Error('There is already a task registered with that name. Name: ' + module.name);
        }

        tasks[module.name] = module.task;
    };

    function resolvePaths(files, output) {

        var paths = files.slice();

        for (var i = 0; i < paths.length; i += 1) {

            paths[i] = output(paths[i]).value();
        }

        return paths;
    }

    function getTask(name) {

        var task = tasks[name];

        if (!task) {

            throw new Error('There is no task with that name. Name: ' + name);
        }

        return task;
    }

    function runTasks(stream, tasks) {

        for (var i = 0; i < tasks.length; i += 1) {

            var name = tasks[i];

            var task = getTask(name);

            stream = task(stream);
        }

        return stream;
    }

    function getPromise(include, output) {

        return task.run(function () {

            var stream = source(resolvePaths(include.files, output));

            return runTasks(stream, include.tasks)
                .pipe(streamFiles(path.resolve(output.value()), include.name))
                .pipe(saveFile());

        }).then(function () {

            return {
                name: include.name,
                files: streamFiles.get(include.name)
            };
        });
    }

    function formatUpdates(data) {

        var updates = {};

        for (var i = 0; i < data.length; i += 1) {

            var update = data[i];

            updates[update.name] = update.files;
        }

        return updates;
    }

    function merge(promises, includes) {

        return q.all(promises).then(function (data) {

            var updates = formatUpdates(data);

            for (var i = 0; i < includes.length; i += 1) {

                var include = includes[i];

                var files = updates[include.name];

                if (files) {

                    include.files = files;
                }
            }

            return includes;
        });
    }

    that.run = function (includes, output) {

        var promises = [];

        for (var i = 0; i < includes.length; i += 1) {

            var include = includes[i];

            promises.push(getPromise(include, output));
        }

        return merge(promises, includes);
    };

    return that;
}

module.exports = runner();