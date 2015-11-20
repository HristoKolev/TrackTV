'use strict';

var path = require('path');

var expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest;

var urlResolve = require('../modules/urlResolve');

describe('#urlResolve()', function () {

    describe('module exports', function () {

        assertCompositionMultitest.function(urlResolve, 'urlResolve');
    });

    var defaultOutput = './wwwroot',
        defaultFilePath = path.resolve('.\\wwwroot\\main-less-styles\\main\\calendar\\clendar.less'),
        defaultResourcePath = 'content/dir/file1';

    describe('validation', function () {

        it('should throw if the output path is falsy', function () {

            expect(function () {

                urlResolve(null, defaultFilePath, defaultResourcePath);

            }).to.throw(/output path is invalid/);

        });

        it('should throw if the file path is falsy', function () {

            expect(function () {

                urlResolve(defaultOutput, null, defaultResourcePath);

            }).to.throw(/file path is invalid/);

        });

        it('should throw if the resource path is falsy', function () {

            expect(function () {

                urlResolve(defaultOutput, defaultFilePath, null);

            }).to.throw(/resource path is invalid/);

        });

        it('should throw if the file path is not absolute', function () {

            expect(function () {

                urlResolve(defaultOutput, 'path/dir/file', defaultResourcePath);

            }).to.throw(/file path is not absolute/);

        });
    });

    it('should throw if the file is not in the output directory', function () {

        var filePath = path.resolve('.\\dist\\file1');

        expect(function () {

            urlResolve(defaultOutput, filePath, defaultResourcePath);

        }).to.throw(/file is not in the output directory/);
    });

    it('should return the path if it is absolute', function () {

        var resourcePath = 'C:\\windows';

        var result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal(resourcePath);

    });

    it('should return the path if it is an url', function () {

        var resourcePath = 'http://google.com';

        var result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal(resourcePath);

    });

    it('should return the path if it is relative protocol url', function () {

        var resourcePath = '//google.com';

        var result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal(resourcePath);

    });

    it('should return the resolved path', function () {

        var result = urlResolve(defaultOutput, defaultFilePath, defaultResourcePath);

        expect(result).to.equal('../../../content/main/calendar/dir/file1');
    });

    it('should return resolved global path if no local path is selected', function () {

        var resourcePath = 'dir/file1';

        var result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal('../../../global_content/dir/file1');
    });

    it('should return resolved global path if explicitly selected ', function () {

        var resourcePath = 'global_content/dir/file1';

        var result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal('../../../global_content/dir/file1');
    });
});