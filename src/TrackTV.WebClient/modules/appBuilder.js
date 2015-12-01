'use strict';

const path = require('path'),
    glob = require('glob-all'),
    _ = require('underscore'),
    fs = require('fs');

const linuxStylePath = require('./linuxStylePath');

function level(input, masterArray) {

    if (!masterArray) {

        masterArray = [];
    }

    if (Array.isArray(input)) {

        for (let element of input) {

            level(element, masterArray);
        }
    }
    else {

        masterArray.push(input);
    }

    return masterArray;
}

function directoryExists(dirPath) {

    try {

        return fs.statSync(dirPath).isDirectory();
    }
    catch (err) {

        return false;
    }
}

const specialDirectories = ['content', 'include'],
    globalSpecialDirectories = _.map(specialDirectories, p => 'global_' + p),
    excludePattern = _.map(globalSpecialDirectories, p => '!' + p);

function appBuilder(rootPath) {

    const that = Object.create(null);

    that.appPath = function appPath(relativePath) {

        if (Array.isArray(relativePath)) {

            for (let i = 0; i < relativePath.length; i += 1) {

                relativePath[i] = appPath(relativePath[i]);
            }

            return relativePath;

        } else {

            relativePath = relativePath || '';

            let notChar = '!';

            let removedNot = false;

            if (relativePath.startsWith(notChar)) {

                relativePath = relativePath.slice(1);
                removedNot = true;
            }

            let result = linuxStylePath(path.join(rootPath, relativePath));

            if (removedNot) {

                result = notChar + result;
            }

            return result;
        }
    };

    const patterns = {
        indexFile: 'index.html',
        initFile: 'init.js',
        routeConfig: 'routeConfig.js',

        moduleHeaders: ['*/module.js', excludePattern.slice()],
        npmModuleFiles: ['*/npmModules.js', excludePattern.slice()],
        moduleConstants: ['*/constants.js', excludePattern.slice()],
        moduleLibraries: ['*/libraries.js', excludePattern.slice()],

        scripts: ['*/*/**/*.js', excludePattern.slice()],
        templates: ['*/*/**/*.html', excludePattern.slice()],
        lessFiles: ['*/*/**/*.less', excludePattern.slice()],

        globalScripts: '*.js',
        globalLess: '*.less',

        globalModuleScripts: ['*/*.js', excludePattern.slice()],
        globalModuleLess: ['*/*.less', excludePattern.slice()]
    };

    patterns.sourceFiles = [
        patterns.initFile,
        patterns.moduleHeaders,
        patterns.moduleConstants,
        patterns.moduleLibraries,
        patterns.scripts,
        patterns.routeConfig
    ];

    patterns.globalScripts = [
        patterns.globalScripts,
        '!' + patterns.initFile,
        '!' + patterns.routeConfig
    ];

    patterns.globalModuleScripts = [
        patterns.globalModuleScripts.slice(),
        '!' + patterns.moduleHeaders[0],
        '!' + patterns.npmModuleFiles[0],
        '!' + patterns.moduleConstants[0],
        '!' + patterns.moduleLibraries[0],
        excludePattern.slice()

    ];

    for (let key of Object.keys(patterns)) {

        let target = _.uniq(level(patterns[key]));

        if (target.length === 1) {

            target = target[0];
        }

        that[key] = that.appPath(target);
    }

    that.contentPath = that.appPath('content');

    that.modulesDir = that.appPath();

    that.getModules = function getModules() {

        return _.chain(fs.readdirSync(rootPath))
            .without('global_content', 'global_include')
            .map(p => ({
                name: p,
                fullPath: path.resolve(path.join(rootPath, p))
            }))
            .filter(p => directoryExists(p.fullPath))
            .value();
    };

    that.getSubmodules = function getSubmodules(modulePath) {

        if (!modulePath) {

            throw new Error('The module path is invalid.');
        }

        let directories = _.chain(glob.sync(path.join(modulePath, '**/*')))
            .filter(p => directoryExists(p))
            .map(p => ({
                name: path.basename(p),
                fullPath: p
            }))
            .filter(p => specialDirectories.indexOf(p.name) === -1);

        return directories.value();
    };

    return that;
}

module.exports = {
    instance: appBuilder
};