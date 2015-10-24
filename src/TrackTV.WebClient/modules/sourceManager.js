var fs = require('fs'),
    path = require('path');

var sourseSettings;

function setSettings (settings) {
    sourseSettings = settings;
}

function PathResolver () {

    PathResolver.prototype.publicPath = function (path) {

        if (path instanceof Array) {

            for (var index in path) {
                path[index] = this.publicPath(path[index]);
            }

            return path;

        } else {
            path = path || '';

            return sourseSettings.rootPath + path;
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

            return sourseSettings.bowerRootPath + path;
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

            return sourseSettings.npmRootPath + path;
        }
    };
}

function SourceListBuilder(pathResolver, rootPath) {

    this._pathResolver = pathResolver;
    this._files = [];
    this._modules = [];

    SourceListBuilder.prototype.addModule = function (name) {

        if (name instanceof Array) {

            for (var index in name) {

                this.addModule(name[index]);
            }

        } else {

            this._modules.push(name);

            this.addFile(this.modulePath(name, '/module.js'));
            this.addFile(this.modulePath(name, '/constants.js'));
            this.addFile(this.modulePath(name, '/libraries.js'));
            this.addFile(this.modulePath(name, '/scripts/**/**/*.js'));
        }

        return this;
    };

    SourceListBuilder.prototype.addPublicFile = function (path) {

        return this.addFile(this._pathResolver.publicPath(path));
    };

    SourceListBuilder.prototype.addFile = function (path) {

        if (path instanceof Array) {

            for (var index in path) {

                this.addFile(path[index]);
            }

        } else {

            this._files.push(path);
        }

        return this;

    };

    SourceListBuilder.prototype.addModuleFile = function (moduleName, path) {

        this.addFile(this.modulePath(moduleName, path));

        return this;
    };

    SourceListBuilder.prototype.scripts = function () {

        return this._files;
    };

    SourceListBuilder.prototype.templates = function () {

        var templatesPaths = [];

        for (var index in this._modules) {
            var module = this._modules[index];

            templatesPaths.push(this.modulePath(module, '/templates/*.html'));
        }

        return templatesPaths;
    };

    SourceListBuilder.prototype.lessFiles = function () {

        var templatesPaths = [];

        for (var index in this._modules) {
            var module = this._modules[index];

            templatesPaths.push(this.modulePath(module, '/styles/*.less'));
        }

        return templatesPaths;
    };

    SourceListBuilder.prototype.clear = function () {

        this._files = [];
    };

    SourceListBuilder.prototype.modulePath = function (moduleName, path) {

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

            return sourseSettings.moduleRootPath + '/' + moduleName + path;
        }
    };

    SourceListBuilder.prototype.getSourceFilesPattern = function () {

        return sourseSettings.moduleRootPath + '/' + Array(sourseSettings.fetchLevel + 1).join('**/') + '*.js';
    };

    function getFolderNames(dir) {
        return fs.readdirSync(dir).filter(function (file) {
            return fs.statSync(path.join(dir, file)).isDirectory();
        });
    }

    this.addFile(rootPath + '/init.js')
        .addModule(getFolderNames(rootPath))
        .addFile(rootPath + '/routeConfig.js');
}

var pathResolver = new PathResolver();

module.exports = {
    pathResolve : pathResolver,
    createSourceListBuilder : function (rootPath) {
        return new SourceListBuilder(pathResolver, rootPath);
    },
    setSettings : setSettings
};