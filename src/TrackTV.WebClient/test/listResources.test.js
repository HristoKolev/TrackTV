"use strict";

var expect = require('chai').expect,
    sinon = require('sinon');

var assertCompositionMultitest = require('../testing/assertComposition').multitest;

var listResources = require('../modules/list-resources');

describe('#listResources()', function () {

    describe('module exports', function () {

        assertCompositionMultitest.function(listResources, 'listResources');
    });

    describe('#list()', function () {

        var defaultResources = ['path1/file', 'path2/file', 'path3/file'];

        var defaultFormatterSpy = sinon.spy(function () {

            return '';
        });

        var echoFormatterSpy = sinon.spy(function (arg) {

            return arg;
        });

        var formatters = {
            defaultFormatter: defaultFormatterSpy,
            echoFormatter: echoFormatterSpy
        };

        function resetMocks() {

            defaultFormatterSpy.reset();
            echoFormatterSpy.reset();
        }

        var defaultBasePath = 'path';

        beforeEach(function () {

            resetMocks();
        });

        it('should throw if the resources argument are falsy', function () {

            expect(function () {

                listResources(null, formatters.defaultFormatter, defaultBasePath);

            }).to.throw(/resources are invalid/);
        });

        it('should throw if the resources argument is not an array', function () {

            var resources = 'resources';

            expect(function () {

                listResources(resources, formatters.defaultFormatter, defaultBasePath);

            }).to.throw(/resources object is not an array/);
        });

        it('should throw if the formatter is falsy', function () {

            expect(function () {

                listResources(defaultResources, null, defaultBasePath);

            }).to.throw(/formatter is invalid/);

        });

        it('should throw if the formatter is not a function', function () {

            var formatter = 'formatter';

            expect(function () {

                listResources(defaultResources, formatter, defaultBasePath);

            }).to.throw(/formatter is not a function/);

        });

        it('should throw if the basePath is provided but is not a string', function () {
            var basePath = 1;

            expect(function () {

                listResources(defaultResources, formatters.defaultFormatter, basePath);

            }).to.throw(/base path is not a string/);
        });

        it('should call the formatter for every element in the resources array', function () {

            var resources = ['path1', 'path2', 'path3'];

            listResources(resources, formatters.defaultFormatter, defaultBasePath);

            expect(defaultFormatterSpy.callCount).to.equal(resources.length);
        });

        it('should format the paths', function () {

            var basePath = 'base';

            listResources(defaultResources, formatters.echoFormatter, basePath);

            var results = [];

            for (var i = 0; i < defaultResources.length; i += 1) {

                results.push(echoFormatterSpy.args[i][0]);
            }

            var expectedPaths = [
                'base/path1/file',
                'base/path2/file',
                'base/path3/file'
            ];

            expect(results).to.deep.equal(expectedPaths);
        });

        it('should not add base path if the not specified', function () {

            listResources(defaultResources, formatters.echoFormatter);

            var results = [];

            for (var i = 0; i < defaultResources.length; i += 1) {

                results.push(echoFormatterSpy.args[i][0]);
            }

            var expectedPaths = [
                'path1/file',
                'path2/file',
                'path3/file'
            ];

            expect(results).to.deep.equal(expectedPaths);
        });

        it('should format the paths in linux style /', function () {

            var resources = [
                'path1\\file',
                'path2\\file',
                'path3\\file'
            ];

            listResources(resources, formatters.echoFormatter);

            var results = [];

            for (var i = 0; i < defaultResources.length; i += 1) {

                results.push(echoFormatterSpy.args[i][0]);
            }

            var expectedPaths = [
                'path1/file',
                'path2/file',
                'path3/file'
            ];

            expect(results).to.deep.equal(expectedPaths);
        });

        it('should remove the path separator from the start of the path', function () {

            var resources = [
                '/path1/file',
                '/path2/file',
                '/path3/file'
            ];

            listResources(resources, formatters.echoFormatter);

            var results = [];

            for (var i = 0; i < defaultResources.length; i += 1) {

                results.push(echoFormatterSpy.args[i][0]);
            }

            var expectedPaths = [
                'path1/file',
                'path2/file',
                'path3/file'
            ];

            expect(results).to.deep.equal(expectedPaths);
        });
    });
});