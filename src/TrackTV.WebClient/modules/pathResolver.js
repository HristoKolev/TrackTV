'use strict';

function pathResolver (pathsConfig) {

    var that = Object.create(null);

    that.publicPath = function (path) {

        if (path instanceof Array) {

            for (var index in path) {

                path[index] = that.publicPath(path[index]);
            }

            return path;

        } else {

            path = path || '';

            return pathsConfig.rootPath + path;
        }
    };

    that.bowerComponent = function (path) {

        if (!path) {

            throw Error('You must specify the path of the component.');
        }

        if (path instanceof Array) {

            for (var index in path) {

                path[index] = that.bowerComponent(path[index]);
            }

            return path;

        } else {

            return pathsConfig.bowerRootPath + path;
        }
    };

    that.npmComponent = function (path) {

        if (!path) {

            throw Error('You must specify the path of the component.');
        }

        if (path instanceof Array) {

            for (var index in path) {

                path[index] = that.npmComponent(path[index]);
            }

            return path;

        } else {

            return pathsConfig.npmRootPath + path;
        }
    };

    return that;
}

module.exports = {
    instance : function (pathConfig) {

        return pathResolver(pathConfig);
    }
};