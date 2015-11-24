'use strict';

let expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest;

let appBuilder = require('../modules/appBuilder');

describe('#appBuilder', function () {

    describe('module exports', function () {

        assertCompositionMultitest.object('appBuilder', appBuilder, [
            ['instance', 'function']
        ]);
    });

    describe('instance exports', function () {

        let builder = appBuilder.instance('./');

        assertCompositionMultitest.object('appBuilder', builder, [

            ['indexFile', 'string'],
            ['initFile', 'string'],
            ['routeConfig', 'string'],

            ['moduleHeaders', 'array'],
            ['npmModuleFiles', 'array'],
            ['moduleConstants', 'array'],
            ['moduleLibraries', 'array'],

            ['scripts', 'array'],
            ['templates', 'array'],
            ['lessFiles', 'array'],

            ['globalScripts', 'array'],
            ['globalLess', 'string'],

            ['globalModuleScripts', 'array'],
            ['globalModuleLess', 'array'],

            ['appPath', 'function'],

            ['sourceFiles', 'array'],

            ['contentPath', 'string'],
            ['modulesDir', 'string']
        ]);
    });

    describe('#appPath()', function () {

        it('should return the application root if called with no or falsy arguments', function () {

            let root = 'path';

            let builder = appBuilder.instance(root);

            expect(builder.appPath()).to.be.equal(root);
            expect(builder.appPath(null)).to.be.equal(root);
        });

        it('should return application relative path given a relative path', function () {

            let rootPath = 'app';

            let builder = appBuilder.instance(rootPath);

            let path = 'file';

            expect(builder.appPath(path)).to.be.equal('app/file');
        });

        it('should convert windows style paths to linux style paths', function () {

            let builder = appBuilder.instance('dir\\path\\path1');

            let path = builder.appPath('lib\\file');

            expect(path).to.equal('dir/path/path1/lib/file');
        });
    });

    describe('all properties of type string', function () {

        function assertBasePath(name, path, basePath) {

            it(name, function () {

                expect(path.indexOf(basePath)).to.be.equal(0);
            });
        }

        let basePath = 'app';

        let builder = appBuilder.instance(basePath);

        for (let key of Object.keys(builder)) {

            let value = builder[key];

            if (typeof value === 'string') {

                let name = 'should start with the application base path. Property:  #' + key;

                assertBasePath(name, value, basePath);
            }
        }
    });
});