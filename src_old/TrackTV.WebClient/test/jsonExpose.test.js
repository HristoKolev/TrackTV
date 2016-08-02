"use strict";

let expect = require('chai').expect,
    mockery = require('mockery');

let assertCompositionMultitest = require('../testing/assertComposition').multitest,
    mockHelper = require('../testing/mockHelper');

const fsMock = mockHelper('fs', {
    readFileSync: ['stub']
});

mockery.registerAllowable('path');

function resetMocks() {

    fsMock.resetMocks();

    fsMock.readFileSync.stub.returns('{}');
}

let jsonExpose = mockHelper.require('../modules/json-expose');

describe('#jsonExpose()', function () {

    beforeEach(function () {

        resetMocks();

        mockery.enable();
    });

    afterEach(function () {

        mockery.disable();
    });

    describe('module exports', function () {

        assertCompositionMultitest.function(jsonExpose, 'jsonExpose');
    });

    describe('#expose()', function () {

        let defaultName = 'settings';

        let defaultPaths = ['file1.json', 'file2.json', 'file3.json'];

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

            for (let i = 0; i < defaultPaths.length; i += 1) {

                fsMock.readFileSync.stub.calledWith(defaultPaths[i]);
            }

            jsonExpose(defaultName, defaultPaths);
        });

        it('should format the json content in a script tag', function () {

            let paths = ['file1.json'];

            fsMock.readFileSync.stub.withArgs('file1.json').returns('{"content":"file1-content"}');

            let name = 'settings';

            let result = jsonExpose(name, paths);

            let expected = '<script>window["settings"] = {"file1":{"content":"file1-content"}};</script>';

            expect(result).to.equal(expected);
        });

        it('should format all of the json content in a script tag', function () {

            let paths = [
                'file1.json',
                'file2.json',
                'file3.json'
            ];

            let values = [
                '{"content":"file1-content"}',
                '{"content":"file2-content"}',
                '{"content":"file2-content"}'
            ];

            for (let i = 0; i < paths.length; i += 1) {

                fsMock.readFileSync.stub.withArgs(paths[i]).returns(values[i]);
            }

            let result = jsonExpose('settings', paths);

            let expected = '<script>window["settings"] = {"file1":{"content":"file1-content"},' +
                '"file2":{"content":"file2-content"},"file3":{"content":"file2-content"}};</script>';

            expect(result).to.equal(expected);
        });
    });
});