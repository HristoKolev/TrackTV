'use strict';

function bowerComponents(pathResolver, includes) {

    var that = Object.create(null);

    Object.keys(includes).forEach(function (key) {

        that[key] = pathResolver.bowerComponent(includes[key]);
    });

    that.baseDir = pathResolver.bowerComponent();

    return that;
}

module.exports = {
    instance: bowerComponents
};