'use srtict';

var expect = require('chai').expect,
    sinon = require('sinon'),
    path = require('path'),
    mockery = require('mockery');

var assertComposition = require('../testing/assertComposition').multitest;

var spy = sinon.spy();

mockery.registerMock('fs-extra', {
    copySync: spy
});

mockery.registerAllowable('../modules/copyFiles');
mockery.registerAllowable('path');

mockery.enable();

var copyFiles = require('../modules/copyFiles');

mockery.disable();

describe('#copyFiles', function () {

    describe('module exports', function () {

        assertComposition.object('copyFiles', copyFiles, [
            ['copy', 'function'],
            ['copyStructure', 'function'],
        ]);
    });

    describe('#copy()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {
            spy.reset();
        });

        after(function () {

            spy.reset();
            mockery.disable();
        });

        var files = [
            'source/dir/dir/file1.txt',
            'source/dir/file2.txt',
            'source/dir2/file3.txt'
        ];

        var destination = 'dest';

        it('should copy each individual file', function () {

            copyFiles.copy(files, destination);

            expect(spy.callCount).to.equal(files.length);

            for (var i = 0; i < files.length; i += 1) {

                expect(spy.args[i][0]).to.equal(files[i]);
            }
        });

        it('should return the new paths', function () {

            var newPaths = copyFiles.copy(files, destination);

            for (var i = 0; i < files.length; i += 1) {

                expect(spy.args[i][1]).to.equal(newPaths[i]);
            }
        });

        it('should return flattened file paths starting with the destination path', function () {

            var expected = [
                'dest\\file1.txt',
                'dest\\file2.txt',
                'dest\\file3.txt'
            ];

            var newPaths = copyFiles.copy(files, destination);

            expect(newPaths).to.deep.equal(expected);
        });

        it('should be able to process single file', function () {

            var newPaths = copyFiles.copy('dir/file.txt', destination);

            expect(newPaths).to.have.length(1);

            expect(newPaths[0]).to.equal('dest\\file.txt');
        });

        it('should use the provided processing function insted of flattening if such function is available', function () {

            var func = function (fileName) {
                return fileName + '!';
            };
            var newPaths = copyFiles.copy('file.txt', destination, func);

            expect(newPaths[0]).to.equal('dest\\file.txt!');

        });

        it('should throw if the process function does not return', function () {

            var func = function () {};

            expect(function () {
                copyFiles.copy('file.txt', destination, func);
            }).to.throw(/falsy path/);

        });

        it('should throw if passed a falsy paths.', function () {

            expect(function () {

                copyFiles.copy(null, destination);

            }).to.throw(/paths are invalid/);

        });

        it('should throw if passed a falsy destination.', function () {

            expect(function () {

                copyFiles.copy('base/file.txt', null);

            }).to.throw(/target directory is invalid/);

        });

        it('should throw if the paths array is empty.', function () {

            expect(function () {

                copyFiles.copy([], destination);

            }).to.throw(/array is empty/);
        });
    });

    describe('#copyStructure()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {
            spy.reset();
        });

        after(function () {

            spy.reset();
            mockery.disable();
        });

        var files = [
            'source/dir/dir/file1.txt',
            'source/dir/file2.txt',
            'source/dir2/file3.txt'
        ];

        var destination = 'dest';

        var baseDir = 'source';

        it('should copy each individual file', function () {

            copyFiles.copyStructure(files, destination, baseDir);

            expect(spy.callCount).to.equal(files.length);

            for (var i = 0; i < files.length; i += 1) {

                expect(spy.args[i][0]).to.equal(files[i]);
            }
        });

        it('should return the new paths', function () {

            var newPaths = copyFiles.copyStructure(files, destination, baseDir);

            for (var i = 0; i < files.length; i += 1) {

                expect(spy.args[i][1]).to.equal(newPaths[i]);
            }
        });

        it('should be able to process single file', function () {

            var newPaths = copyFiles.copyStructure('dir/file.txt', destination, 'dir');

            expect(newPaths).to.have.length(1);

            expect(newPaths[0]).to.equal('dest\\file.txt');
        });

        it('should use the provided processing function insted of removing the base path if such function is available', function () {

            var func = function (fileName) {
                return fileName + '!';
            };

            var newPaths = copyFiles.copyStructure('base/file.txt', destination, 'base', func);

            expect(newPaths[0]).to.equal('dest\\base\\file.txt!');

        });

        it('should throw if the process function does not return', function () {

            var func = function () {};

            expect(function () {
                copyFiles.copyStructure('base/file.txt', destination, 'base', func);
            }).to.throw(/falsy path/);

        });

        it('should copy the files while preserving the file structure', function () {

            var expected = [
                'dest\\dir\\dir\\file1.txt',
                'dest\\dir\\file2.txt',
                'dest\\dir2\\file3.txt'
            ];

            var newPaths = copyFiles.copyStructure(files, destination, 'source');

            expect(newPaths).to.deep.equal(expected);

        });

        it('should throw if tries to remove base path that a file does not start with', function () {

            expect(function () {

                copyFiles.copyStructure('base/file.txt', destination, 'notBase');

            }).to.throw(/does not start with the provided base directory/);
        });

        it('should throw if passed a falsy paths.', function () {

            expect(function () {

                copyFiles.copyStructure(null, destination, 'base');

            }).to.throw(/paths are invalid/);

        });

        it('should throw if passed a falsy destination.', function () {

            expect(function () {

                copyFiles.copyStructure('base/file.txt', null, 'base');

            }).to.throw(/target directory is invalid/);

        });

        it('should throw if the paths array is empty.', function () {

            expect(function () {

                copyFiles.copyStructure([], destination, 'base');

            }).to.throw(/array is empty/);
        });

        it('should throw if passed a falsy base directory.', function () {

            expect(function () {

                copyFiles.copyStructure('base/file.txt', destination, null);

            }).to.throw(/base directory is invalid/);
        });
    });
});