'use strict';

let path = require('path');

let linuxStylePath = require('./linuxStylePath');

function appBuilder(rootPath) {

    let that = Object.create(null);

    that.appPath = function (relativePath) {

        if (Array.isArray(relativePath)) {

            for (let i = 0; i < relativePath.length; i += 1) {

                relativePath[i] = that.appPath(relativePath[i]);
            }

            return relativePath;

        } else {

            relativePath = relativePath || '';

            return linuxStylePath(path.join(rootPath, relativePath));
        }
    };

    var patterns = {
        indexFile: '/index.html',
        initFile: '/init.js',
        routeConfig: '/routeConfig.js',

        moduleHeaders: '/modules/*/module.js',
        npmModuleFiles: '/modules/*/npmModules.js',
        moduleConstants: '/modules/*/constants.js',
        moduleLibraries: '/modules/*/libraries.js',

        scripts: '/modules/*/*/**/*.js',
        templates: '/modules/*/*/**/*.html',
        lessFiles: '/modules/*/*/**/*.less',

        globalScripts: '/*.js',
        globalLess: '/*.less',

        globalModuleScripts: '/modules/*/*.js',
        globalModuleLess: '/modules/*/*.less'
    };

    for (let key  of Object.keys(patterns)) {

        that[key] = that.appPath(patterns[key]);
    }

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
        '!' + that.moduleLibraries
    ];

    that.contentPath = that.appPath('/content');

    that.modulesDir = that.appPath('/modules');

    return that;
}

module.exports = {
    instance: appBuilder
};