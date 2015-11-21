'use strict';

let path = require('path');

let expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest;

let urlResolve = require('../modules/urlResolve');

describe('#urlResolve()', function () {

    describe('module exports', function () {

        assertCompositionMultitest.function(urlResolve, 'urlResolve');
    });

    let defaultOutput = './wwwroot',
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

        let filePath = path.resolve('.\\dist\\file1');

        expect(function () {

            urlResolve(defaultOutput, filePath, defaultResourcePath);

        }).to.throw(/file is not in the output directory/);
    });

    it('should return the path if it is absolute', function () {

        let resourcePath = 'C:\\windows';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal(resourcePath);

    });

    it('should return the path if it is an url', function () {

        let resourcePath = 'http://google.com';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal(resourcePath);

    });

    it('should return the path if it is relative protocol url', function () {

        let resourcePath = '//google.com';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal(resourcePath);

    });

    it('should resolve local path', function () {

        let result = urlResolve(defaultOutput, defaultFilePath, defaultResourcePath);

        expect(result).to.equal('../../../content/main/calendar/dir/file1');
    });

    it('should resolve local include path', function () {

        let resourcePath = 'include/dir/file1';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal('../../../include/main/calendar/dir/file1');
    });

    it('should resolve global path if explicitly selected ', function () {

        let resourcePath = 'global_content/dir/file1';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal('../../../global_content/dir/file1');
    });

    it('should resolve global include path if explicitly selected ', function () {

        let resourcePath = 'global_include/dir/file1';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal('../../../global_include/dir/file1');
    });

    it('should resolve global path if no local path is selected', function () {

        let resourcePath = 'dir/file1';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal('../../../global_content/dir/file1');
    });

    it('should throw if the file path does not match the build system pattern', function () {

        let filePath = path.resolve('.\\wwwroot\\main\\calendar\\clendar.less');

        expect(function () {

            urlResolve(defaultOutput, filePath, defaultResourcePath);

        }).to.throw(/relative file path/);

    });
});