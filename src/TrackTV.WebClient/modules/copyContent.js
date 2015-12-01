'use strict';

const fs = require('fs'),
    path = require('path');

const specialDirectories = ['content', 'include'];

function directoryExists(dirPath) {

    try {

        return fs.statSync(dirPath).isDirectory();
    }
    catch (err) {

        return false;
    }
}

function getLocalList(modules, outputPath, appBuilder) {

    let list = [];

    for (let module of modules) {

        let submodules = appBuilder.getSubmodules(module.fullPath);

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

module.exports = function (appBuilder, outputPath) {

    if (!outputPath) {

        throw new Error('Invalid output path');
    }

    if (!appBuilder) {

        throw new Error('Invalid app builder');
    }

    let modules = appBuilder.getModules();

    let localList = getLocalList(modules, outputPath, appBuilder);

    let moduleList = getModuleList(modules, outputPath);

    let globalList = getGlobalList(appBuilder.appPath(), outputPath);

    return localList.concat(moduleList, globalList);
};