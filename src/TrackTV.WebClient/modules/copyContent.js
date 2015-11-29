'use strict';

const fs = require('fs'),
    path = require('path'),
    glob = require('glob-all').sync,
    _ = require('underscore').chain;

const specialDirectories = ['content', 'include'];

function directoryExists(dirPath) {

    try {

        return fs.statSync(dirPath).isDirectory();
    }
    catch (err) {

        return false;
    }
}

function getModules(appPath) {

    return _(fs.readdirSync(appPath))
        .without('global_content', 'global_include')
        .map(p => ({
            name: p,
            fullPath: path.resolve(path.join(appPath, p))
        }))
        .filter(p => directoryExists(p.fullPath))
        .value();
}

function getSubmodules(modulePath) {

    let directories = _(glob(path.join(modulePath, '**/*')))
        .filter(p => directoryExists(p))
        .map(p => ({
            name: path.basename(p),
            fullPath: p
        }))
        .filter(p => specialDirectories.indexOf(p.name) === -1);

    return directories.value();
}

function getLocalList(modules, outputPath) {

    let list = [];

    for (let module of modules) {

        let submodules = getSubmodules(module.fullPath);

        for (let submodule of submodules) {

            for (let specialDirectory of specialDirectories) {

                let targetPath = path.join(submodule.fullPath, specialDirectory);

                let destinationPath = path.join(outputPath, specialDirectory, 'local', module.name, submodule.name);

                if (directoryExists(targetPath)) {

                    list.push({
                        targetPath: targetPath,
                        destinationPath: destinationPath,
                        basePath: targetPath
                    });
                }
            }
        }
    }

    return list;
}

function getModuleList(modules, outputPath) {

    let list = [];

    for (let module of modules) {

        for (let specialDirectory of specialDirectories) {

            let targetPath = path.join(module.fullPath, specialDirectory);

            let destinationPath = path.join(outputPath, specialDirectory, 'module', module.name);

            if (directoryExists(targetPath)) {

                list.push({
                    targetPath: targetPath,
                    destinationPath: destinationPath,
                    basePath: targetPath
                });
            }
        }
    }

    return list;
}

function getGlobalList(appPath, outputPath) {

    let list = [];

    for (let specialDirectory of specialDirectories) {

        let targetPath = path.join(appPath, 'global_' + specialDirectory);

        let destinationPath = path.join(outputPath, specialDirectory, 'global');

        if (directoryExists(targetPath)) {

            list.push({

                targetPath: targetPath,
                destinationPath: destinationPath,
                basePath: targetPath
            });
        }
    }

    return list;
}

module.exports = function (appPath, outputPath) {

    if (!appPath) {

        throw new Error('Invalid application path');
    }

    if (!outputPath) {

        throw new Error('Invalid output path');
    }

    let modules = getModules(appPath);

    let localList = getLocalList(modules, outputPath);

    let moduleList = getModuleList(modules, outputPath);

    let globalList = getGlobalList(appPath, outputPath);

    return localList.concat(moduleList, globalList);
};