'use strict';

const expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest;

let modulePathParse = require('../modules/modulePathParse');

describe('#modulePathParse()', function () {

    describe('module exports', function () {

        assertCompositionMultitest.function(modulePathParse, 'modulePathParse');
    });

    describe('validation', function () {

        it('should throw if the file path is falsy', function () {

            expect(function () {

                modulePathParse(null);

            }).to.throw(/file path is invalid/);
        });

        it('should throw if the file path has less than 2 parts', function () {

            expect(function () {

                modulePathParse('file');

            }).to.throw(/minimum of 2 parts/);
        });
    });

    it('should parse global path', function () {

        var info = modulePathParse('name/file1');

        expect(info.fileClass).to.equal(modulePathParse.fileClass.global);
    });

    it('should parse global module path', function () {

        var info = modulePathParse('name/module/file1');

        expect(info.fileClass).to.equal(modulePathParse.fileClass.moduleGlobal);
        expect(info.moduleName).to.equal('module');
    });

    it('should parse local path', function () {

        var info = modulePathParse('name/module/submodule/file1');

        expect(info.fileClass).to.equal(modulePathParse.fileClass.local);
        expect(info.moduleName).to.equal('module');
        expect(info.submoduleName).to.equal('submodule');
    });

    it('should parse nested submodules', function () {

        var info = modulePathParse('name/module/submodule1/submodule2/submoduleN/file1');

        expect(info.fileClass).to.equal(modulePathParse.fileClass.local);
        expect(info.moduleName).to.equal('module');
        expect(info.submoduleName).to.equal('submoduleN');
    });
});
