"use strict";

var expect = require('chai').expect,
    sinon = require('sinon'),
    mockery = require('mockery');

var assertCompositionMultitest = require('../testing/assertComposition').multitest;

function resetMocks() {

}

function mockRequire(moduleName) {

    mockery.registerAllowable(moduleName);

    mockery.enable();

    var module = require(moduleName);

    mockery.disable();

    return module;
}

var includer = mockRequire('../modules/includer');

describe('#includer', function () {

    describe('module exports', function () {

        assertCompositionMultitest.object('includer', includer, [
            ['instance', 'function']
        ]);
    });

    describe('instance exports', function () {

        //var instance = includer.instance({}, {});
        //
        //assertCompositionMultitest.object('instance', includer, [
        //    ['instance', 'function']
        //]);
    });

});