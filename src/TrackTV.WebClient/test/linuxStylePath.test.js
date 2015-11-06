'use strict';

var expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest;

var linuxStylePath = require('../modules/linuxStylePath');

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

        var path = '/dir/path/file';

        expect(linuxStylePath(path)).to.be.equal(path);
    });

    it('should replace \\ with /', function () {

        var path = 'dir\\path\\file';
        var linuxPath = 'dir/path/file';

        expect(linuxStylePath(path)).to.be.equal(linuxPath);
    });
});

