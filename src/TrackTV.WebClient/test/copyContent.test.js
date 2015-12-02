'use strict';

const assertCompositionMultitest = require('../testing/assertComposition').multitest,
    chai = require('chai'),
    expect = chai.expect;

const copyContent = require('../modules/copyContent');

describe('#copyContent()', function () {

    assertCompositionMultitest.function(copyContent, 'copyContent');

    describe('validations', function () {

        const defaultOutputPath = 'app';

        it('should throw if the app builder is falsy', function () {

            expect(function () {

                copyContent(null, defaultOutputPath);
            }).to.throw(/app builder is invalid/);
        });

        it('should throw if the output path is falsy', function () {

            expect(function () {

                copyContent({}, null);
            }).to.throw(/app output path is invalid/);
        });
    });
});