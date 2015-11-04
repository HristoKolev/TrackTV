var fs = require('fs'),
    path = require('path'),
    glob = require('glob');

function appBuilder(pathResolver, rootPath) {

    var that = Object.create(null);

    function getFolderNames(dir) {

        return fs.readdirSync(dir).filter(function (file) {

            return fs.statSync(path.join(dir, file)).isDirectory();
        });
    }

    that.appPath = function (path) {

        if (Array.isArray(path)) {

            for (var i = 0; i < path.length; i += 1) {

                path[i] = that.appPath(path[i]);
            }

            return path;

        } else {

            path = path || '';

            return rootPath + path;
        }
    };

    var patterns = {
        indexFile: '/index.html',
        initFile: '/init.js',
        moduleHeaders: '/modules/*/module.js',
        npmModuleFiles: '/modules/*/npmModules.js',
        moduleConstants: '/modules/*/constants.js',
        moduleLibraries: '/modules/*/libraries.js',
        scripts: '/modules/*/*/**/*.js',
        routeConfig: '/routeConfig.js',
        templates: '/modules/*/*/**/*.html',
        lessFiles: '/modules/*/*/**/*.less',
        globalScripts: '/*.js',
        globalModuleScripts: '/modules/*/*.js'
    };

    Object.keys(patterns).forEach(function (index) {

        that[index] = that.appPath(patterns[index]);
    });

    that.sourceFiles = [
        that.initFile,
        that.moduleHeaders,
        that.moduleConstants,
        that.moduleLibraries,
        that.scripts,
        that.routeConfig
    ];

    that.globalScripts = [
        that.globalScripts,
        '!' + that.initFile,
        '!' + that.routeConfig
    ];

    that.globalModuleScripts = [
        that.globalModuleScripts,
        '!' + that.moduleHeaders,
        '!' + that.npmModuleFiles,
        '!' + that.moduleConstants,
        '!' + that.moduleLibraries,
    ];

    that.contentPath = that.appPath('/content');

    that.modulesDir = that.appPath('/modules');

    return that;
}

module.exports = {
    instance: appBuilder
};