'use strict';

const expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest;

const urlResolve = require('../modules/urlResolve');

describe('#urlResolve()', function () {

    describe('module exports', function () {

        assertCompositionMultitest.function(urlResolve, 'urlResolve');
    });

    const defaultOutput = '.\\wwwroot',
        defaultFilePath = '.\\wwwroot\\main\\calendar\\calendar.less',
        defaultResourcePath = 'content/dir/file1',
        defaultIncludeResourcePath = 'include/dir/file1',
        defaultImplicitResourcePath = 'dir/file1';

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
    });

    it('should throw if the file is not in the output directory', function () {

        const filePath = '.\\dist\\file1';

        expect(function () {

            urlResolve(defaultOutput, filePath, defaultResourcePath);

        }).to.throw(/file is not in the output directory/);
    });

    it('should return the path if it is absolute', function () {

        const resourcePath = 'C:\\windows';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal(resourcePath);

    });

    it('should return the path if it is an url', function () {

        const resourcePath = 'http://google.com';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal(resourcePath);

    });

    it('should return the path if it is relative protocol url', function () {

        const resourcePath = '//google.com';

        let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

        expect(result).to.equal(resourcePath);

    });

    describe('global resource', function () {

        it('should resolve global path if explicitly selected ', function () {

            const resourcePath = 'global_content/dir/file1';

            let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

            expect(result).to.equal('../../content/global/dir/file1');
        });

        it('should resolve global include path if explicitly selected ', function () {

            const resourcePath = 'global_include/dir/file1';

            let result = urlResolve(defaultOutput, defaultFilePath, resourcePath);

            expect(result).to.equal('../../include/global/dir/file1');
        });
    });

    describe('local files', function () {

        it('should resolve local path', function () {

            let result = urlResolve(defaultOutput, defaultFilePath, defaultResourcePath);

            expect(result).to.equal('../../content/local/main/calendar/dir/file1');
        });

        it('should resolve local include path', function () {

            let result = urlResolve(defaultOutput, defaultFilePath, defaultIncludeResourcePath);

            expect(result).to.equal('../../include/local/main/calendar/dir/file1');
        });

        it('should default to local content path if not explicitly stated', function () {

            let result = urlResolve(defaultOutput, defaultFilePath, defaultImplicitResourcePath);

            expect(result).to.equal('../../content/local/main/calendar/dir/file1');
        });
    });

    describe('module files', function () {

        const filePath = '.\\wwwroot\\main\\calendar.less';

        it('should resolve module path', function () {

            let result = urlResolve(defaultOutput, filePath, defaultResourcePath);

            expect(result).to.equal('../content/module/main/dir/file1');
        });

        it('should resolve module include path', function () {

            let result = urlResolve(defaultOutput, filePath, defaultIncludeResourcePath);

            expect(result).to.equal('../include/module/main/dir/file1');
        });

        it('should default to module content path if not explicitly stated', function () {

            let result = urlResolve(defaultOutput, filePath, defaultImplicitResourcePath);

            expect(result).to.equal('../content/module/main/dir/file1');
        });
    });

    describe('global files', function () {

        const filePath = '.\\wwwroot\\calendar.less';

        it('should resolve module path', function () {

            let result = urlResolve(defaultOutput, filePath, defaultResourcePath);

            expect(result).to.equal('content/global/dir/file1');
        });

        it('should resolve module include path', function () {

            let result = urlResolve(defaultOutput, filePath, defaultIncludeResourcePath);

            expect(result).to.equal('include/global/dir/file1');
        });

        it('should default to global content path if not explicitly stated', function () {

            let result = urlResolve(defaultOutput, filePath, defaultImplicitResourcePath);

            expect(result).to.equal('content/global/dir/file1');
        });
    });
});