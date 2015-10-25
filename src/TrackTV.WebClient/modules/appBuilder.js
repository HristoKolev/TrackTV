var fs = require('fs'),
    path = require('path'); 

function appBuilder(pathResolver, rootPath) {

    this._pathResolver = pathResolver;
    this._files = [];
    this._modules = [];

    appBuilder.prototype.loadModules = function () {

        function getFolderNames (dir) {
            return fs.readdirSync(dir).filter(function (file) {
                return fs.statSync(path.join(dir, file)).isDirectory();
            });
        }

        this.addAppFile('/init.js')
            .addModule(getFolderNames(this.appPath()))
            .addAppFile('/routeConfig.js');

        return this;
    };

    appBuilder.prototype.addModule = function (name) {

        if (name instanceof Array) {

            for (var index in name) {

                this.addModule(name[index]);
            }

        } else {

            this._modules.push(name);

            var headerFiles = [
                '/module.js',
                '/constants.js',
                '/libraries.js'
            ];

            for (var index in headerFiles) {
                var header = headerFiles[index];

                this.addFile(this.modulePath(name, header));
            }

            this.addFile(this.modulePath(name, '/scripts/**/**/*.js'));
        }

        return this;
    };

    appBuilder.prototype.addPublicFile = function (path) {

        return this.addFile(this._pathResolver.publicPath(path));
    };

    appBuilder.prototype.addAppFile = function (path) {

        return this.addFile(this.appPath(path));
    };

    appBuilder.prototype.addFile = function (path) {

        if (path instanceof Array) {

            for (var index in path) {

                this.addFile(path[index]);
            }

        } else {

            this._files.push(path);
        }

        return this;

    };

    appBuilder.prototype.addModuleFile = function (moduleName, path) {

        this.addFile(this.modulePath(moduleName, path));

        return this;
    };

    appBuilder.prototype.scripts = function () {

        return this._files;
    };

    appBuilder.prototype.templates = function () {

        var templatesPaths = [];

        for (var index in this._modules) {
            var module = this._modules[index];

            templatesPaths.push(this.modulePath(module, '/templates/*.html'));
        }

        return templatesPaths;
    };

    appBuilder.prototype.lessFiles = function () {

        var templatesPaths = [];

        for (var index in this._modules) {
            var module = this._modules[index];

            templatesPaths.push(this.modulePath(module, '/styles/*.less'));
        }

        return templatesPaths;
    };

    appBuilder.prototype.clear = function () {

        this._files = [];
    };

    appBuilder.prototype.modulePath = function (moduleName, path) {

        if (!moduleName) {
            throw Error('You must specifiy the name of the module.');
        }

        if (path instanceof Array) {

            for (var index in path) {

                path[index] = this.modulePath(moduleName, path[index]);
            }

            return path;

        } else {

            path = path || '';

            return this.appPath('/' + moduleName + path);
        }
    };

    appBuilder.prototype.appPath = function (path) {

        if (path instanceof Array) {

            for (var index in path) {
                path[index] = this.appPath(path[index]);
            }

            return path;

        } else {

            path = path || '';

            return rootPath + path;
        }
    };

    this.loadModules();

    //appBuilder.prototype.getSourceFilesPattern = function () {
    //    return sourseSettings.moduleRootPath + '/' + Array(sourseSettings.fetchLevel + 1).join('**/') + '*.js';
    //};

}

module.exports = {
    instance: function (pathResolver, rootPath) {
        return new appBuilder(pathResolver, rootPath);
    }
};