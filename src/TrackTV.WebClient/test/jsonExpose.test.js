"use strict";

var expect = require('chai').expect,
    sinon = require('sinon'),
    mockery = require('mockery');

var assertCompositionMultitest = require('../testing/assertComposition').multitest;

var readStub = sinon.stub();
var readSpy = sinon.spy(readStub);

function resetMocks() {

    readStub.reset();
    readSpy.reset();

    readStub.returns('{}');
}

var fs = {
    readFileSync: readStub
};

mockery.registerAllowable('path');

function mockRequire(moduleName) {

    mockery.registerAllowable(moduleName);

    mockery.enable();

    var module = require(moduleName);

    mockery.disable();

    return module;
}

mockery.registerMock('fs', fs);

var jsonExpose = mockRequire('../modules/json-expose');

describe('#jsonExpose()', function () {

    describe('module exports', function () {

        assertCompositionMultitest.function(jsonExpose, 'jsonExpose');
    });

    describe('#expose()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            resetMocks();
        });

        after(function () {

            resetMocks();

            mockery.disable();
        });

        var defaultName = 'settings';

        var defaultPaths = ['file1.json', 'file2.json', 'file3.json'];

        it('should throw if the name is falsy', function () {

            expect(function () {

                jsonExpose(null, defaultPaths);

            }).to.throw(/name is invalid/);

        });

        it('should throw if the paths argument is falsy', function () {

            expect(function () {

                jsonExpose(defaultName, null);

            }).to.throw(/paths are invalid/);
        });

        it('should throw if the paths argument is not an array', function () {

            expect(function () {

                jsonExpose(defaultName, 'paths');

            }).to.throw(/paths is not an array/);
        });

        it('should read every file', function () {

            for (var i = 0; i < defaultPaths.length; i += 1) {

                readStub.calledWith(defaultPaths[i]);
            }

            jsonExpose(defaultName, defaultPaths);
        });

        it('should format the json content in a script tag', function () {

            var paths = ['file1.json'];

            readStub.withArgs('file1.json').returns('{"content":"file1-content"}');

            var name = 'settings';

            var result = jsonExpose(name, paths);

            var expected = '<script>window.settings = {"file1":{"content":"file1-content"}};</script>';

            expect(result).to.equal(expected);
        });

        it('should format all of the json content in a script tag', function () {

            var paths = [
                'file1.json',
                'file2.json',
                'file3.json'
            ];

            var values = [
                '{"content":"file1-content"}',
                '{"content":"file2-content"}',
                '{"content":"file2-content"}'
            ];

            for (var i = 0; i < paths.length; i += 1) {

                readStub.withArgs(paths[i]).returns(values[i]);
            }

            var result = jsonExpose('settings', paths);

            var expected = '<script>window.settings = {"file1":{"content":"file1-content"},' +
                '"file2":{"content":"file2-content"},"file3":{"content":"file2-content"}};</script>';

            expect(result).to.equal(expected);
        });
    });
});
