'use strict';

var fs = require('fs'),
    path = require('path');

function getTaskNames(taskRoot) {

    var paths = fs.readdirSync(taskRoot)
        .filter(file => fs.statSync(path.join(taskRoot, file)).isFile());

    for (var i = 0; i < paths.length; i += 1) {

        paths[i] = path.basename(paths[i]);
    }

    return paths;
}

let taskNames = getTaskNames('./modules/tasks/');

function tasks() {

    let that = Object.create(null);

    that.load = function (runner) {

        for (let taskName of taskNames) {

            runner.use(require('./tasks/' + taskName));
        }
    };

    return that;
}

module.exports = tasks();