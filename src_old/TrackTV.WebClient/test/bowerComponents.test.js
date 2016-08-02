'use strict';

let expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest,
    assertComposition = require('../testing/assertComposition');

let bowerComponents = require('../modules/bowerComponents');

describe('#bowerComponents', function () {

    describe('module exports', function () {

        assertCompositionMultitest.object('bowerComponents', bowerComponents, [
            ['instance', 'function']
        ]);
    });

    let defaultBasePath = 'app/path';

    describe('#instance()', function () {

        let components = bowerComponents.instance({}, defaultBasePath);

        assertCompositionMultitest.object('bowerComponents.instance()', components, [
            ['basePath', 'string'],
            ['resolve', 'function']
        ]);
    });

    describe('#resolve()', function () {

        it('should return the base path if falsy or no argument is provided', function () {

            let components = bowerComponents.instance({}, defaultBasePath);

            expect(components.resolve()).to.equal(defaultBasePath);

        });

        it('should convert windows style paths to linux style paths', function () {

            let basePath = 'app\\path';

            let components = bowerComponents.instance({}, basePath);

            let path = 'dir\\dir1\\file';

            expect(components.resolve(path)).to.equal('app/path/dir/dir1/file');
        });

        it('should return component relative path given a relative path', function () {

            let basePath = 'app';

            let components = bowerComponents.instance({}, basePath);

            let path = 'file';

            expect(components.resolve(path)).to.equal('app/file');
        });

        it('should resolve all elements when an array is passed', function () {

            let basePath = 'app';

            let paths = ['path1', 'path2', 'path3'];

            let components = bowerComponents.instance({}, basePath);

            let resolved = components.resolve(paths);

            expect(resolved).to.deep.equal(['app/path1', 'app/path2', 'app/path3']);
        });
    });

    it('should throw if the basePath is falsy', function () {

        expect(function () {

            bowerComponents.instance({}, null);

        }).to.throw(/base path is invalid/);
    });

    it('should throw if the includes are falsy', function () {

        expect(function () {

            bowerComponents.instance(null, defaultBasePath);

        }).to.throw(/includes object is invalid/);
    });

    it('should expose the basePath', function () {

        let components = bowerComponents.instance({}, defaultBasePath);

        expect(components.basePath).to.equal(defaultBasePath);
    });

    it('should expose all of the inlcudes', function () {

        let includes = {
            path1: 'path1',
            path2: 'path2'
        };

        let components = bowerComponents.instance(includes, defaultBasePath);

        let properties = [];

        for (let key of Object.keys(includes)) {

            properties.push([key, 'string']);
        }

        assertComposition.object('components', components, properties);

    });

    it('should resolve all of the includes', function () {

        let basePath = 'app';

        let includes = {
            path1: 'path1',
            path2: 'path2'
        };

        let components = bowerComponents.instance(includes, basePath);

        for (let key of Object.keys(includes)) {

            expect(components[key]).to.equal('app/' + includes[key]);
        }
    });
});