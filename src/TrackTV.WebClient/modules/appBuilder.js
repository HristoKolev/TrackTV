'use strict';

var fs = require('fs'),
    path = require('path');

function appBuilder(rootPath) {

    var that = Object.create(null);

    that.appPath = function (relativePath) {

        if (Array.isArray(relativePath)) {

            for (var i = 0; i < relativePath.length; i += 1) {

                relativePath[i] = that.appPath(relativePath[i]);
            }

            return relativePath;

        } else {

            relativePath = relativePath || '';

            return path.join(rootPath, relativePath);
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
        '!' + that.moduleLibraries
    ];

    that.contentPath = that.appPath('/content');

    that.modulesDir = that.appPath('/modules');

    return that;
}

module.exports = {
    instance: appBuilder
};