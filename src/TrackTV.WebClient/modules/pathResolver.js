'use strict';

function pathResolver(pathsConfig) {

    var that = Object.create(null);

    that.publicPath = function (path) {

        if (Array.isArray(path)) {

            for (var i = 0; i < path.length; i += 1) {

                path[i] = that.publicPath(path[i]);
            }

            return path;

        } else {

            path = path || '';

            return pathsConfig.rootPath + path;
        }
    };

    that.bowerComponent = function (path) {

        if (Array.isArray(path)) {

            for (var i = 0; i < path.length; i += 1) {

                path[i] = that.bowerComponent(path[i]);
            }

            return path;

        } else {

            path = path || '';

            return pathsConfig.bowerRootPath + path;
        }
    };

    that.npmComponent = function (path) {

        if (!path) {

            throw Error('You must specify the path of the component.');
        }

        if (Array.isArray(path)) {

            for (var i = 0; i < path.length; i += 1) {

                path[i] = that.npmComponent(path[i]);
            }

            return path;

        } else {

            return pathsConfig.npmRootPath + path;
        }
    };

    return that;
}

module.exports = {
    instance: function (pathConfig) {

        return pathResolver(pathConfig);
    }
};