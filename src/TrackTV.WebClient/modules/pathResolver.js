'use strict';

function PathResolver (pathsConfig) {

    PathResolver.prototype.publicPath = function (path) {

        if (path instanceof Array) {

            for (var index in path) {
                path[index] = this.publicPath(path[index]);
            }

            return path;

        } else {
            path = path || '';

            return pathsConfig.rootPath + path;
        }
    };

    PathResolver.prototype.bowerComponent = function (path) {

        if (!path) {
            throw Error('You must specify the path of the component.');
        }

        if (path instanceof Array) {

            for (var index in path) {
                path[index] = this.bowerComponent(path[index]);
            }

            return path;

        } else {

            return pathsConfig.bowerRootPath + path;
        }
    };

    PathResolver.prototype.npmComponent = function (path) {

        if (!path) {
            throw Error('You must specify the path of the component.');
        }

        if (path instanceof Array) {

            for (var index in path) {
                path[index] = this.npmComponent(path[index]);
            }

            return path;

        } else {

            return pathsConfig.npmRootPath + path;
        }
    };
}

module.exports = {
    instance : function (pathConfig) {
        return new PathResolver(pathConfig);
    }
};