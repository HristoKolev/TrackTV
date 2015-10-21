var pathResolver = new PathResolver();

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

function ModulePathResolver () {

    ModulePathResolver.prototype.modulePath = function (moduleName, path) {

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

    ModulePathResolver.prototype.getSourceFilesPattern = function () {

        return sourseSettings.moduleRootPath + '/' + Array(sourseSettings.fetchLevel + 1).join('**/') + '*.js';
    };
}

function SourceListBuilder (pathResolver, modulePathResolver) {

    this._pathResolver = pathResolver;
    this._modulePathResolver = modulePathResolver;
    this._files = [];

    SourceListBuilder.prototype.addModule = function (name) {

        if (name instanceof Array) {

            for (var index in name) {

                this.addModule(name[index]);
            }

        } else {

            this.addFile(this._modulePathResolver.modulePath(name, '/module.js'));
            this.addFile(this._modulePathResolver.modulePath(name, '/constants.js'));
            this.addFile(this._modulePathResolver.modulePath(name, '/libraries.js'));
            this.addFile(this._modulePathResolver.modulePath(name, '/scripts/**/**/*.js'));
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

        this.addFile(this._modulePathResolver.modulePath(moduleName, path));

        return this;
    };

    SourceListBuilder.prototype.src = function () {

        return this._files;
    };

    SourceListBuilder.prototype.clear = function () {

        this._files = [];
    };

}

var modulePath = new ModulePathResolver();

module.exports = {
    pathResolve : pathResolver,
    modulePath : modulePath,
    createSourceListBuilder : function () {
        return new SourceListBuilder(pathResolver, modulePath);
    },
    setSettings : setSettings
};