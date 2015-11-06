'use strict';

var expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest,
    assertComposition = require('../testing/assertComposition');

var bowerComponents = require('../modules/bowerComponents');

describe('#bowerComponents', function () {

    describe('module exports', function () {

        assertCompositionMultitest.object('bowerComponents', bowerComponents, [
            ['instance', 'function']
        ]);
    });

    var defaultBasePath = 'app/path';

    describe('#instance()', function () {

        var components = bowerComponents.instance({}, defaultBasePath);

        assertCompositionMultitest.object('bowerComponents.instance()', components, [
            ['basePath', 'string'],
            ['resolve', 'function']
        ]);
    });

    describe('#resolve()', function () {

        it('should return the base path if falsy or no argument is provided', function () {

            var components = bowerComponents.instance({}, defaultBasePath);

            expect(components.resolve()).to.equal(defaultBasePath);

        });

        it('should convert windows style paths to linux style paths', function () {

            var basePath = 'app\\path';

            var components = bowerComponents.instance({}, basePath);

            var path = 'dir\\dir1\\file';

            expect(components.resolve(path)).to.equal('app/path/dir/dir1/file');
        });

        it('should return component relative path given a relative path', function () {

            var basePath = 'app';

            var components = bowerComponents.instance({}, basePath);

            var path = 'file';

            expect(components.resolve(path)).to.equal('app/file');
        });

        it('should resolve all elements when an array is passed', function () {

            var basePath = 'app';

            var paths = ['path1', 'path2', 'path3'];

            var components = bowerComponents.instance({}, basePath);

            var resolved = components.resolve(paths);

            expect(resolved).to.deep.equal(['app/path1', 'app/path2', 'app/path3']);
        });
    });

    it('should expose the basePath', function () {

        var components = bowerComponents.instance({}, defaultBasePath);

        expect(components.basePath).to.equal(defaultBasePath);
    });

    it('should expose all of the inlcudes', function () {

        var includes = {
            path1: 'path1',
            path2: 'path2'
        };

        var components = bowerComponents.instance(includes, defaultBasePath);

        var properties = [];

        Object.keys(includes).forEach(function (key) {

            properties.push([key, 'string']);
        });

        assertComposition.object('components', components, properties);

    });

    it('should resolve all of the includes', function () {

        var basePath = 'app';

        var includes = {
            path1: 'path1',
            path2: 'path2'
        };

        var components = bowerComponents.instance(includes, basePath);

        Object.keys(includes).forEach(function (key) {

            expect(components[key]).to.equal('app/' + includes[key]);
        });

    });
});