'use strict';

const _ = require('underscore'),
    glob = require('glob-all').sync,
    path = require('path'),
    expect = require('chai').expect;

function getPaths() {

    const appPath = require('./config/appConfig').appPath;

    const absoluteAppPath = path.resolve(appPath);

    const pattern = path.join(absoluteAppPath, '**/*.*');

    function shorten(p) {

        return path.relative(absoluteAppPath, p);
    }

    return _(glob(pattern)).map(shorten);
}

const paths = getPaths();

function assertContains(p) {

    it('should contain ' + p, function () {

        expect(_(paths).contains(p)).to.be.true;
    });
}

describe('App validations', function () {

    assertContains('index.html');
    assertContains('init.js');
    assertContains('routeConfig.js');
});
