'use strict';

const _ = require('underscore').chain,
    glob = require('glob-all').sync,
    path = require('path'),
    expect = require('chai').expect,
    assert = require('chai').assert,
    fs = require('fs');

const configs = _(glob('./config/*.json'))
    .map(config => path.relative(path.resolve('./config'), config))
    .value();

function pathExists(p) {

    try {

        fs.statSync(p);
        return true;
    }
    catch (err) {

        return false;
    }
}

function directoryExists(dirPath) {

    try {

        return fs.statSync(dirPath).isDirectory();
    }
    catch (err) {

        return false;
    }
}

function fileExists(filePath) {

    try {

        return fs.statSync(filePath).isFile();
    }
    catch (err) {

        return false;
    }
}

function getModules(appPath) {

    return _(fs.readdirSync(appPath))
        .without('global_content', 'global_include')
        .map(function (p) {

            return {
                name: p,
                fullPath: path.resolve(path.join(appPath, p))
            };
        })
        .filter(p => directoryExists(p.fullPath))
        .value();
}

function getSubmodules(modulePath) {

    let directories = _(glob(path.join(modulePath, '**/*')))
        .filter(p => directoryExists(p))
        .map(p => ({
            name: path.basename(p),
            fullPath: p
        }));

    return directories.value();
}

let appConfig;

describe('configuration validation', function () {

    describe('appConfig.json', function () {

        const fileName = 'appConfig.json';

        it('should exist', function () {

            expect(_(configs).contains(fileName).value()).to.be.true;
        });

        appConfig = require('./' + path.join('config', fileName));

        it('should have property appPath ', function () {

            expect(appConfig.appPath).to.exist;
        });

        it('should have property appPath that points to existing directory', function () {

            expect(directoryExists(path.resolve(appConfig.appPath))).to.be.true;
        });
    });
});

const paths = (function getPaths() {

    const appPath = require('./config/appConfig').appPath,
        absoluteAppPath = path.resolve(appPath),
        pattern = path.join(absoluteAppPath, '**/*.*');

    function shorten(p) {

        return path.relative(absoluteAppPath, p);
    }

    return _(glob(pattern))
        .map(shorten)
        .value();

}());

function assertContains(p) {

    it('root directory should contain ' + p, function () {

        expect(_(paths).contains(p).value()).to.be.true;
    });
}

function assertModulePath(name) {

    function createTest(module) {

        it(`module "${module.name}" should contain ${name}`, function () {

            let targetPath = path.join(module.fullPath, name);

            expect(pathExists(targetPath));
        });
    }

    let modules = getModules(appConfig.appPath);

    for (let module of modules) {

        createTest(module);
    }
}

function validateSubmodules() {

    let modules = getModules(appConfig.appPath);

    function testSubmodule(groupes, module, submoduleName) {

        it(`there should be only one submodule named "${submoduleName}" in module "${module.name}"`, function () {

            let paths = _(groupes[submoduleName]).map(e => e.fullPath).value();

            if (paths.length > 1) {

                assert.fail(null, null, `There are more than one submodule called "${submoduleName}" in module "${module.name}": \n ${paths.join(',\n')}`);
            }
        });
    }

    for (let module of modules) {

        let submodules = getSubmodules(module.fullPath);

        let groupes = _(submodules).groupBy(p => p.name).value();

        for (let submoduleName of Object.keys(groupes)) {

            testSubmodule(groupes, module, submoduleName);
        }
    }
}

describe('structure validations', function () {

    assertContains('index.html');
    assertContains('init.js');
    assertContains('routeConfig.js');

    assertModulePath('module.js');

    validateSubmodules();
});