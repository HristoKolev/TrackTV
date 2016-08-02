'use strict';

const path = require('path'),
    glob = require('glob-all').sync,
    _ = require('underscore'),
    fs = require('fs');

const linuxStylePath = require('./linuxStylePath'),
    fsExtend = require('../modules/fs-extend');

let appConfig = require('../config/appConfig');

const specialDirectories = ['content', 'include'],
    globalSpecialDirectories = _.map(specialDirectories, p => 'global_' + p);

let exclusionList = [];

function appPath(relativePath) {

    if (Array.isArray(relativePath)) {

        for (let i = 0; i < relativePath.length; i += 1) {

            relativePath[i] = appPath(relativePath[i]);
        }

        return relativePath;

    } else {

        relativePath = relativePath || '';

        return linuxStylePath(path.join(appConfig.appPath, relativePath));
    }
}

function excludeSpecial(array) {

    return _.filter(array, function (e) {

        for (let directory of globalSpecialDirectories) {

            if (e.startsWith(directory)) {

                return false;
            }
        }

        return true;
    });
}

function localize(array) {

    return _.map(array, p => path.relative(appConfig.appPath, p));
}

function addToExclusionList(array) {

    if (Array.isArray(array)) {

        exclusionList = exclusionList.concat(array);
    }
    else {
        exclusionList.push(array);
    }
}

function excludeFiles(array) {

    return _.filter(array, e => !_.contains(exclusionList, e));
}

function localGlob(p) {

    let paths = excludeFiles(excludeSpecial(localize(glob(appPath(p)))));

    addToExclusionList(paths);

    return paths;
}

function getModules() {

    return _.chain(fs.readdirSync(appConfig.appPath))
        .without(...globalSpecialDirectories)
        .map(p => ({
            name: p,
            fullPath: path.resolve(path.join(appConfig.appPath, p))
        }))
        .filter(p => fsExtend.directoryExists(p.fullPath))
        .value();
}

function getSubmodules(modulePath) {

    if (!modulePath) {

        throw new Error('The module path is invalid.');
    }

    let directories = _.chain(glob(path.join(modulePath, '**/*')))
        .filter(fsExtend.directoryExists)
        .map(p => ({
            name: path.basename(p),
            fullPath: p
        }))
        .filter(p => specialDirectories.indexOf(p.name) === -1);

    return directories.value();
}

module.exports = {

    appPath,
    getModules,
    getSubmodules,

    indexFile: localGlob('index.html')[0] || '',
    initFile: localGlob('init.js')[0] || '',
    routeConfig: localGlob('routeConfig.js')[0] || '',

    moduleHeaders: localGlob('*/module.js'),
    npmModuleFiles: localGlob('*/npmModules.js'),

    scripts: localGlob('*/*/**/*.js'),
    templates: localGlob('*/*/**/*.html'),
    lessFiles: localGlob('*/*/**/*.less'),

    globalScripts: localGlob('*.js'),
    globalLess: localGlob('*.less'),

    globalModuleScripts: localGlob('*/*.js'),
    globalModuleLess: localGlob('*/*.less')
};