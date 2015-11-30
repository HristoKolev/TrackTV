'use strict';

let chai = require('chai'),
    expect = chai.expect,
    sinonChai = require("sinon-chai"),
    mockery = require('mockery'),
    assertCompositionMultitest = require('../testing/assertComposition').multitest,
    mockHelper = require('../testing/mockHelper'),
    path = require('path');

chai.use(sinonChai);

const globMock = mockHelper('glob-all', {
    sync: ['stub', 'spy']
});

let globSync = globMock.sync;

const fsMock = mockHelper('fs', {
    readdirSync: ['stub', 'spy'],
    statSync: ['stub', 'spy']
});

mockery.registerAllowables([
    './linuxStylePath',
    'path',
    'fs'
]);

const appBuilder = mockHelper.require('../modules/appBuilder');

describe('#appBuilder', function () {

    beforeEach(function () {

        globMock.resetMocks();
        fsMock.resetMocks();

        mockery.enable();
    });

    afterEach(function () {

        mockery.disable();
    });

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
            ['modulesDir', 'string'],

            ['getModules', 'function']
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

    const defaultAppPath = 'app';

    let absoluteAppPath = path.resolve(defaultAppPath);

    function createDefaultInstance(appPath) {

        appPath = appPath || defaultAppPath;

        return appBuilder.instance(appPath);
    }

    let defaultPaths = [
        'main',
        'kitten',
        'dir1',
        'dir2',
        'global_content',
        'global_include',
        'someFile.html',
        'someOtherFile.js'
    ];

    function registerStats() {

        function isDirectory(p) {

            return function () {

                console.log(p);
                return p.indexOf('.') === -1;
            };
        }

        for (let p of defaultPaths) {

            fsMock.statSync.stub
                .withArgs(path.join(absoluteAppPath, p))
                .returns({
                    isDirectory: isDirectory(p)
                });
        }
    }

    describe('#getModules()', function () {

        function getModules() {

            fsMock.readdirSync.stub.returns(defaultPaths);

            registerStats();

            let instance = createDefaultInstance();

            return instance.getModules();
        }

        it('should read the app directory', function () {

            let instance = createDefaultInstance();

            instance.getModules();

            expect(fsMock.readdirSync.spy).to.be.calledOnce;
            expect(fsMock.readdirSync.spy).to.always.be.calledWithExactly(defaultAppPath);
        });

        it('should call stat for every file and directory in the application path ' +
            'excluding the global content directories', function () {

            getModules();

            expect(fsMock.statSync.spy).to.have.callCount(defaultPaths.length - 2);
        });

        it('should return module objects with properties "name" and "fullPath"', function () {

            let modules = getModules();

            let expected = [
                {
                    name: 'main',
                    fullPath: path.join(absoluteAppPath, 'main')
                },
                {
                    name: 'kitten',
                    fullPath: path.join(absoluteAppPath, 'kitten')
                },
                {
                    name: 'dir1',
                    fullPath: path.join(absoluteAppPath, 'dir1')
                },
                {
                    name: 'dir2',
                    fullPath: path.join(absoluteAppPath, 'dir2')
                }
            ];

            expect(modules).to.deep.equal(expected);
        });
    });
});