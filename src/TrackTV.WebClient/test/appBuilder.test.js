'use strict';

var expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest,
    assertComposition = require('../testing/assertComposition');

var appBuilder = require('../modules/appBuilder');

describe('#appBuilder', function () {

    describe('module exports', function () {

        assertCompositionMultitest.object('appBuilder', appBuilder, [
            ['instance', 'function']
        ]);
    });

    describe('instance exports', function () {

        var builder = appBuilder.instance('./');

        assertCompositionMultitest.object('appBuilder', builder, [
            ['appPath', 'function'],

            ['indexFile', 'string'],
            ['initFile', 'string'],
            ['moduleHeaders', 'string'],
            ['npmModuleFiles', 'string'],
            ['moduleConstants', 'string'],
            ['moduleLibraries', 'string'],
            ['scripts', 'string'],
            ['routeConfig', 'string'],
            ['templates', 'string'],
            ['lessFiles', 'string'],

            ['globalScripts', 'array'],
            ['globalModuleScripts', 'array'],
            ['sourceFiles', 'array'],

            ['contentPath', 'string'],
            ['modulesDir', 'string']
        ]);
    });

    describe('#appPath()', function () {

        it('should return the application root if called with no or falsy arguments', function () {

            var root = 'path';

            var builder = appBuilder.instance(root);

            expect(builder.appPath()).to.be.equal(root);
            expect(builder.appPath(null)).to.be.equal(root);
        });

        it('should return application relative path given a relative path', function () {

            var rootPath = 'app';

            var builder = appBuilder.instance(rootPath);

            var path = 'file';

            expect(builder.appPath(path)).to.be.equal('app\\file');
        });
    });

    describe('all properties of type string', function () {

        function assertBasePath(name, path, basePath) {

            it(name, function () {

                expect(path.indexOf(basePath)).to.be.equal(0);
            });
        }

        var basePath = 'app';

        var builder = appBuilder.instance(basePath);

        Object.keys(builder).forEach(function (index) {

            var value = builder[index];

            if (typeof value === 'string') {

                var name = 'should start with the application base path. Property:  #' + index;

                assertBasePath(name, value, basePath);
            }
        });
    });
});