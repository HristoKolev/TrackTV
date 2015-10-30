var fs = require('fs'),
    path = require('path');

function appBuilder(pathResolver, rootPath, fetchLevel) {

    fetchLevel = fetchLevel || 4;

    var that = Object.create(null);

    var files = [],
        modules = [];

    function getFolderNames(dir) {

        return fs.readdirSync(dir).filter(function (file) {

            return fs.statSync(path.join(dir, file)).isDirectory();
        });
    }

    that.addModule = function (name) {

        if (name instanceof Array) {

            for (var index in name) {

                that.addModule(name[index]);
            }

        } else {

            modules.push(name);

            var headerFiles = [
                '/constants.js',
                '/libraries.js'
            ];

            for (var index in headerFiles) {
                var header = headerFiles[index];

                that.addFile(that.modulePath(name, header));
            }

            that.addFile(that.modulePath(name, '/scripts/' + Array(fetchLevel + 1).join('**/') + '*.js'));
        }

        return that;
    };

    that.addPublicFile = function (path) {

        return that.addFile(pathResolver.publicPath(path));
    };

    that.addAppFile = function (path) {

        return that.addFile(that.appPath(path));
    };

    that.addFile = function (path) {

        if (path instanceof Array) {

            for (var index in path) {

                that.addFile(path[index]);
            }

        } else {

            files.push(path);
        }

        return that;

    };

    that.addModuleFile = function (moduleName, path) {

        that.addFile(that.modulePath(moduleName, path));

        return that;
    };

    that.scripts = function () {

        return files.concat([]);
    };

    that.templates = function () {

        var templatesPaths = [];

        for (var index in modules) {

            var module = modules[index];

            templatesPaths.push(that.modulePath(module, '/templates/*.html'));
        }

        return templatesPaths;
    };

    that.lessFiles = function () {

        var templatesPaths = [];

        for (var index in modules) {

            var module = modules[index];

            templatesPaths.push(that.modulePath(module, '/styles/*.less'));
        }

        return templatesPaths;
    };

    that.clear = function () {

        files = [];
    };

    that.modulePath = function (moduleName, path) {

        if (!moduleName) {

            throw Error('You must specifiy the name of the module.');
        }

        if (path instanceof Array) {

            for (var index in path) {

                path[index] = that.modulePath(moduleName, path[index]);
            }

            return path;

        } else {

            path = path || '';

            return that.appPath('/' + moduleName + path);
        }
    };

    that.appPath = function (path) {

        if (path instanceof Array) {

            for (var index in path) {

                path[index] = that.appPath(path[index]);
            }

            return path;

        } else {

            path = path || '';

            return rootPath + path;
        }
    };

    that.npmModuleFiles = function () {

        var files = [];

        for (var i = 0; i < modules.length; i += 1) {

            files.push(that.modulePath(modules[i], '/npmModules.js'));
        }

        return files;
    };

    that.moduleHeaders = function () {

        var files = [that.appPath('/init.js')];

        for (var i = 0; i < modules.length; i += 1) {

            files.push(that.modulePath(modules[i], '/module.js'));
        }

        return files;

    };

    that.sourceFiles = function () {

        return that.scripts().concat(that.moduleHeaders());
    };

    that.addModule(getFolderNames(that.appPath()))
        .addAppFile('/routeConfig.js');

    return that;
}

module.exports = {
    instance: appBuilder
};