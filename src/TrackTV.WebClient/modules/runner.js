'use strict';

const path = require('path'),
    gulp = require('gulp');

const streamFiles = require('./plugins/streamFiles');

function saveFile() {

    return gulp.dest(function (file) {

        return path.dirname(file.path);
    });
}

function source(files) {

    return gulp.src(files);
}

function resolvePaths(files, outputPath) {

    let paths = files.slice();

    for (let i = 0; i < paths.length; i += 1) {

        paths[i] = path.join(outputPath, paths[i]);
    }

    return paths;
}

function formatUpdates(data) {

    let updates = {};

    for (let update of data) {

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

    function runTasks(stream, taskNames, context) {

        for (let taskName of taskNames) {

            let task = getTask(taskName);

            stream = task(stream, context);
        }

        return stream;
    }

    function streamToPromise(stream) {

        return new Promise(function (resolve) {

            return stream.on('finish', function () {

                return resolve();
            });
        });
    }

    function getPromise(include, context) {

        let stream = source(resolvePaths(include.files, context.outputPath));

        stream = runTasks(stream, include.tasks, context)
            .pipe(streamFiles(path.resolve(context.outputPath), include.name))
            .pipe(saveFile());

        function format() {

            return {
                name: include.name,
                files: streamFiles.get(include.name)
            };
        }

        return streamToPromise(stream).then(format);
    }

    function merge(promises, includes) {

        return Promise.all(promises).then(function (data) {

            let updates = formatUpdates(data);

            for (let include of includes) {

                let files = updates[include.name];

                if (files) {

                    include.files = files;
                }
            }

            return includes;
        });
    }

    that.run = function (includes, outputPath) {

        if (!includes) {

            throw new Error('The includes are invalid.');
        }

        if (!Array.isArray(includes)) {

            throw new Error('The includes argument is not an array.');
        }

        if (!outputPath) {

            throw new Error('The output path is invalid.');
        }

        let promises = [];

        var context = {
            outputPath
        };

        for (let include of includes) {

            promises.push(getPromise(include, context));
        }

        return merge(promises, includes);
    };

    return that;
}

module.exports = runner();