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

var list = getTaskNames('./modules/tasks/');

function tasks() {

    var that = Object.create(null);

    that.load = function (runner) {

        for (var i = 0; i < list.length; i += 1) {

            runner.use(require('./tasks/' + list[i]));
        }
    };

    return that;
}

module.exports = tasks();