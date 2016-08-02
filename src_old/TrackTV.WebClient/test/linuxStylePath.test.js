'use strict';

let expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest;

let linuxStylePath = require('../modules/linuxStylePath');

describe('#linuxStylePath()', function () {

    describe('module exports', function () {

        assertCompositionMultitest.function(linuxStylePath, 'linuxStylePath');
    });

    it('should throw if passed a falsy path that is not an empty sting', function () {

        expect(function () {

            linuxStylePath(null);

        }).to.throw(/path is invalid/);
    });

    it('should return an empty sting when passed an empty sting', function () {

        expect(linuxStylePath('')).to.be.equal('');
    });

    it('should return a passed sting if the sting does not contain "\\"', function () {

        let path = '/dir/path/file';

        expect(linuxStylePath(path)).to.be.equal(path);
    });

    it('should replace \\ with /', function () {

        let path = 'dir\\path\\file';
        let linuxPath = 'dir/path/file';

        expect(linuxStylePath(path)).to.be.equal(linuxPath);
    });
});

