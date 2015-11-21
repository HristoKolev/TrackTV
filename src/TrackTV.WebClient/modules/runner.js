'use strict';

let path = require('path'),
    gulp = require('gulp'),
    task = require('gulp-task'),
    q = require('q');

let streamFiles = require('./plugins/streamFiles');

function saveFile() {

    return gulp.dest(function (file) {

        return path.dirname(file.path);
    });
}

function source(files) {

    return gulp.src(files);
}

function resolvePaths(files, output) {

    let paths = files.slice();

    for (let i = 0; i < paths.length; i += 1) {

        paths[i] = output(paths[i]).value();
    }

    return paths;
}

function formatUpdates(data) {

    let updates = {};

    for (let i = 0; i < data.length; i += 1) {

        let update = data[i];

        updates[update.name] = update.files;
    }

    return updates;
}

function runner() {

    var that = Object.create(null),
        tasks = {};

    that.use = function (module) {

        if (!module) {

            throw new Error('The module is invalid.');
        }

        if (!module.name) {

            throw new Error('The module name is invalid.');
        }

        if (!module.task) {

            throw new Error('The module task is invalid. ' + JSON.stringify(module));
        }

        if (tasks[module.name]) {

            throw new Error('There is already a task with that name. Name: ' + module.name);
        }

        tasks[module.name] = module.task;
    };

    function getTask(name) {

        let task = tasks[name];

        if (!task) {

            throw new Error('There is no task with that name. Name: ' + name);
        }

        return task;
    }

    function runTasks(stream, tasks) {

        for (let i = 0; i < tasks.length; i += 1) {

            let name = tasks[i];

            let task = getTask(name);

            stream = task(stream);
        }

        return stream;
    }

    function getPromise(include, output) {

        return task.run(function () {

            let stream = source(resolvePaths(include.files, output));

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

    function merge(promises, includes) {

        return q.all(promises).then(function (data) {

            let updates = formatUpdates(data);

            for (let i = 0; i < includes.length; i += 1) {

                let include = includes[i];

                let files = updates[include.name];

                if (files) {

                    include.files = files;
                }
            }

            return includes;
        });
    }

    that.run = function (includes, output) {

        if (!includes) {

            throw new Error('The includes are invalid.');
        }

        if (!Array.isArray(includes)) {

            throw new Error('The includes argument is not an array.');
        }

        if (!output) {

            throw new Error('The output is invalid.');
        }

        let promises = [];

        for (var i = 0; i < includes.length; i += 1) {

            let include = includes[i];

            promises.push(getPromise(include, output));
        }

        return merge(promises, includes);
    };

    return that;
}

module.exports = runner();