'use strict';

var list = [
    'less'
];

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