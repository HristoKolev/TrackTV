'use strict';

const _ = require('underscore'),
    glob = require('glob-all').sync,
    path = require('path'),
    expect = require('chai').expect,
    fs = require('fs');

const configs = _(glob('./config/*.json')).map(config => path.relative(path.resolve('./config'), config));

function directoryExists(filePath) {
    try {
        return fs.statSync(filePath).isDirectory();
    }
    catch (err) {
        return false;
    }
}

describe('configuration validation', function () {

    describe('appConfig.json', function () {

        const fileName = 'appConfig.json';

        it('should exist', function () {

            expect(_(configs).contains(fileName)).to.be.true;
        });

        const appConfig = require('./' + path.join('config', fileName));

        it('should have property appPath ', function () {

            expect(appConfig.appPath).to.exist;
        });

        it('should have property appPath that points to existing directory', function () {

            expect(directoryExists(path.resolve(appConfig.appPath))).to.be.true;
        });
    });
});

const paths = (function getPaths() {

    const appPath = require('./config/appConfig').appPath;

    const absoluteAppPath = path.resolve(appPath);

    const pattern = path.join(absoluteAppPath, '**/*.*');

    function shorten(p) {

        return path.relative(absoluteAppPath, p);
    }

    return _(glob(pattern)).map(shorten);

}());

function assertContains(p) {

    it('should contain ' + p, function () {

        expect(_(paths).contains(p)).to.be.true;
    });
}

describe('structure validations', function () {

    assertContains('index.html');
    assertContains('init.js');
    assertContains('routeConfig.js');
});