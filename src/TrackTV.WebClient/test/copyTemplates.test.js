'use strict';

const expect = require('chai').expect;

const copyTemplates = require('../modules/copyTemplates');

describe('#copyTemplates()', function () {

    let appPath = 'app';
    let outputPath = 'output';

    describe('validation', function () {

        it('should throw if the files argument is falsy', function () {

            expect(function () {

                copyTemplates(null, appPath, outputPath);
            }).to.throw(/template file paths argument is invalid/);
        });

        it('should throw if the files argument is not an array', function () {

            expect(function () {

                copyTemplates('string', appPath, outputPath);
            }).to.throw(/template file paths argument is not an array/);
        });

        it('should throw if the app path is falsy', function () {

            expect(function () {

                copyTemplates([], null, outputPath);
            }).to.throw(/app path is invalid/);
        });

        it('should throw if the output path is falsy', function () {

            expect(function () {

                copyTemplates([], appPath, null);

            }).to.throw(/output path is invalid/);

        });

        it('should throw if any of the paths is not formatted properly', function () {

            expect(function () {

                copyTemplates(['app/path/dir'], appPath, outputPath);

            }).to.throw(/file path is not in a valid format/);
        });
    });

    it('should return an array', function () {

        let result = copyTemplates([], appPath, outputPath);

        expect(result).to.be.an('array');
    });

    it('should return an array of object that have targetPath and destinationPath properties', function () {

        let result = copyTemplates(['module/submodule/index.html'], appPath, outputPath);

        expect(result[0].targetPath).to.exist;
        expect(result[0].destinationPath).to.exist;
    });
});