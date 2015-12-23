"use strict";

let chai = require('chai'),
    sinonChai = require("sinon-chai"),
    expect = chai.expect,
    sinon = require('sinon'),
    mockery = require('mockery'),
    mockHelper = require('../testing/mockHelper');

chai.use(sinonChai);

let assertCompositionMultitest = require('../testing/assertComposition').multitest;

let fsMock = mockHelper('fs', {

    readFileSync: ['stub', 'spy'],
    writeFileSync: ['spy']
});

let copyFilesMock = mockHelper('./copyFiles', {

    copy: ['stub', 'spy'],
    copyStructure: ['stub', 'spy']
});

let outputConfigMock = mockHelper('../config/outputConfig', {

    devPath: ['stubObject']
});

let fillContentMock = mockHelper.mockFunction('./fillContent', ['spy']);

mockery.registerAllowables([
    './fillContent',
    './list-resources',
    './copyFiles',
    'path'
]);

let defaultOutput = 'app',
    defaultLogFile = 'app\\includes.json',
    defaultOutputIndex = 'app\\index.html';

function createNewIncluder() {

    outputConfigMock.devPath.setValue(defaultOutput);

    return mockHelper.require('../modules/includer');
}

let includer = createNewIncluder();

describe('#includer', function () {

    beforeEach(function () {

        fsMock.resetMocks();
        copyFilesMock.resetMocks();
        fillContentMock.resetMocks();

        mockery.enable();
    });

    afterEach(function () {

        mockery.disable();
    });

    describe('module exports', function () {

        assertCompositionMultitest.object('includer', includer, [
            ['formatters', 'object'],
            ['createIncludeLog', 'function'],
            ['logInclude', 'function'],
            ['updateIncludes', 'function'],

            ['readIncludes', 'function'],
            ['writeIncludes', 'function'],

            ['copyAndIncludeFile', 'function'],
            ['copyAndIncludeFiles', 'function'],
            ['copyAndIncludeDirectory', 'function']
        ]);
    });

    function returnEmptyArray(name) {

        fsMock.readFileSync.stub.withArgs(name).returns('[]');
    }

    let defaultIncludes = [
        {
            name: 'name',
            files: [
                'file1',
                'file2',
                'file3'
            ],
            formatter: 'script',
            tasks: ['task1', 'task2']
        }
    ];

    function registerIncludes(includes) {

        fsMock.readFileSync.stub.withArgs(defaultLogFile).returns(JSON.stringify(includes));
    }

    describe('#formatters', function () {

        assertCompositionMultitest.object('formatters', includer.formatters, [
            ['scriptFormatter', 'string'],
            ['styleFormatter', 'string'],
            ['none', 'string']
        ]);

        it('should have #scriptFormatter with the right value', function () {

            expect(includer.formatters.scriptFormatter).to.equal('script');
        });

        it('should have #styleFormatter with the right value', function () {

            expect(includer.formatters.styleFormatter).to.equal('style');
        });

        it('should have #none with the right value', function () {

            expect(includer.formatters.none).to.equal('none');
        });

    });

    describe('#createIncludeLog()', function () {

        it('should write to the includes file', function () {

            let instance = createNewIncluder();

            instance.createIncludeLog();

            expect(fsMock.writeFileSync.spy).to.be.calledOnce;

            expect(fsMock.writeFileSync.spy).to.be.always.calledWithExactly(defaultLogFile, '[]');
        });
    });

    describe('#logInclude()', function () {

        let defaultName = 'name';
        let defaultFiles = ['file1', 'file2', 'file3'];
        let defaultFormatter = 'formatter';
        let defaultTasks = ['task1', 'task2'];

        describe('validation', function () {

            it('should throw if the name is falsy', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.logInclude(null, defaultFiles, defaultFormatter, defaultTasks);

                }).to.throw(/name is invalid/);
            });

            it('should throw if the files array is falsy', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.logInclude(defaultName, null, defaultFormatter, defaultTasks);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if the files is not an array', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.logInclude(defaultName, 'files', defaultFormatter, defaultTasks);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if the formatter is falsy', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.logInclude(defaultName, defaultFiles, null, defaultTasks);

                }).to.throw(/formatter is invalid/);
            });

            it('should throw if the tasks argument is not an array', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.logInclude(defaultName, defaultFiles, defaultFormatter, 'tasks');

                }).to.throw(/tasks argument is not an array/);
            });
        });

        it('should read the includes file', function () {

            let instance = createNewIncluder();

            returnEmptyArray(defaultLogFile);

            instance.logInclude(defaultName, defaultFiles, defaultFormatter, defaultTasks);

            expect(fsMock.readFileSync.spy).to.be.calledOnce;
        });

        it('should write to the includes file', function () {

            let instance = createNewIncluder();

            returnEmptyArray(defaultLogFile);

            instance.logInclude(defaultName, defaultFiles, defaultFormatter, defaultTasks);

            expect(fsMock.writeFileSync.spy).to.be.calledOnce;
            expect(fsMock.writeFileSync.spy).to.be.always.calledWith(defaultLogFile);
        });

        it('should add the includes to the log file', function () {

            let instance = createNewIncluder();

            returnEmptyArray(defaultLogFile);

            instance.logInclude(defaultName, defaultFiles, defaultFormatter, defaultTasks);

            let expected = [
                {
                    name: 'name',
                    files: [
                        'file1',
                        'file2',
                        'file3'
                    ],
                    formatter: 'formatter',
                    tasks: ['task1', 'task2']
                }
            ];

            let result = JSON.parse(fsMock.writeFileSync.spy.args[0][1]);

            expect(result).to.deep.equal(expected);
        });
    });

    describe('#updateIncludes()', function () {

        it('should read the includes file', function () {

            let instance = createNewIncluder();

            instance.updateIncludes();

            expect(fsMock.readFileSync.spy).to.be.calledOnce;
            expect(fsMock.readFileSync.spy).to.be.always.calledWithExactly(defaultLogFile);
        });

        it('should call #fillContent() for every include', function () {

            let instance = createNewIncluder();

            registerIncludes(defaultIncludes);

            instance.updateIncludes();

            for (let i = 0; i < defaultIncludes.length; i += 1) {

                let include = defaultIncludes[i];

                expect(fillContentMock.spy).to.be.calledWith(defaultOutputIndex, include.name);
            }
        });

        it('should call #fillContent() with the correct formatted includes', function () {

            registerIncludes(defaultIncludes);

            let instance = createNewIncluder();

            instance.updateIncludes();

            let expected = [
                '<script src="file1"></script>\n' +
                '<script src="file2"></script>\n' +
                '<script src="file3"></script>'
            ];

            for (let i = 0; i < defaultIncludes.length; i += 1) {

                let include = defaultIncludes[i];

                expect(fillContentMock.spy).to.be.calledWithExactly(defaultOutputIndex, include.name, expected[i]);
            }
        });

        it('should format the includes with the correct formatter', function () {

            let includes = [
                {
                    name: 'name',
                    files: [
                        'file1',
                        'file2',
                        'file3'
                    ],
                    formatter: 'script'
                },
                {
                    name: 'styles',
                    files: [
                        'style1',
                        'style2',
                        'style3',
                    ],
                    formatter: 'style'
                }
            ];

            registerIncludes(includes);

            let expected = [
                '<script src="file1"></script>\n' +
                '<script src="file2"></script>\n' +
                '<script src="file3"></script>',

                '<link rel="stylesheet" href="style1">\n' +
                '<link rel="stylesheet" href="style2">\n' +
                '<link rel="stylesheet" href="style3">'
            ];

            let instance = createNewIncluder();

            instance.updateIncludes();

            for (let i = 0; i < includes.length; i += 1) {

                let include = includes[i];

                expect(fillContentMock.spy).to.be.calledWithExactly(defaultOutputIndex, include.name, expected[i]);
            }
        });

        it('should throw if the includes refer to non existent formatter', function () {

            let includes = [
                {
                    name: 'name',
                    files: [
                        'file1',
                        'file2',
                        'file3'
                    ],
                    formatter: 'invalid formatter'
                }
            ];

            registerIncludes(includes);

            let instance = createNewIncluder();

            expect(function () {

                instance.updateIncludes();

            }).to.throw(/Unknown formatter name/);
        });
    });

    describe('#readIncludes()', function () {

        it('should read the includes file', function () {

            registerIncludes(defaultIncludes);

            let instance = createNewIncluder();

            let result = instance.readIncludes();

            expect(fsMock.readFileSync.spy).to.be.calledOnce;
            expect(fsMock.readFileSync.spy).to.be.always.calledWithExactly(defaultLogFile);

            expect(result).to.deep.equal(defaultIncludes);
        });
    });

    describe('#writeIncludes()', function () {

        describe('validation', function () {

            it('should throw if the includes are falsy', function () {

                expect(function () {

                    let instance = createNewIncluder();
                    instance.writeIncludes(null);

                }).to.throw(/includes are invalid/);
            });

            it('should throw if the includes are not an array', function () {

                expect(function () {

                    let instance = createNewIncluder();

                    instance.writeIncludes('includes');

                }).to.throw(/includes are not an array/);
            });
        });

        it('should write the includes to the file', function () {

            let instance = createNewIncluder();

            instance.writeIncludes(defaultIncludes);

            expect(fsMock.writeFileSync.spy).to.be.calledOnce;
            expect(fsMock.writeFileSync.spy).to.be.always.calledWith(defaultLogFile);

            let includes = JSON.parse(fsMock.writeFileSync.spy.args[0][1]);

            expect(includes).to.deep.equal(defaultIncludes);
        });
    });

    function assertCopiedStructure(...args) {

        expect(copyFilesMock.copyStructure.spy).to.be.calledOnce;

        expect(copyFilesMock.copyStructure.spy).to.be.always.calledWith(...args);
    }

    function assertLoggedIncludes(spy, ...args) {

        expect(spy).to.be.calledOnce;

        expect(spy).to.be.always.calledWithExactly(...args);
    }

    describe('#copyAndIncludeFile()', function () {

        let defaultPlaceholder = 'placeholder',
            defaultFile = 'source/file1',
            defaultBasePath = 'source',
            defaultFormatter = 'formatter',
            defaultTasks = ['task1', 'task2'];

        describe('validation', function () {

            it('should throw if passed a falsy name', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFile(null, defaultFile, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/placeholder is invalid/);
            });

            it('should throw if passed a falsy file', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFile(defaultPlaceholder, null, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/file is invalid/);
            });

            it('should throw if passed a falsy basePath', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFile(defaultPlaceholder, defaultFile, null, defaultFormatter, defaultTasks);

                }).to.throw(/base path is invalid/);
            });

            it('should throw if passed a falsy formatter', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFile(defaultPlaceholder, defaultFile, defaultBasePath, null, defaultTasks);

                }).to.throw(/formatter is invalid/);

            });

            it('should throw if the tasks argument is not an array', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFile(defaultPlaceholder, defaultFile, defaultBasePath, defaultFormatter, 'tasks');

                }).to.throw(/tasks argument is not an array/);

            });
        });

        it('should call #copyFiles.copyStructure() with the specified arguments', function () {

            copyFilesMock.copyStructure.stub.returns([]);

            let instance = createNewIncluder();

            instance.copyAndIncludeFile(defaultPlaceholder, defaultFile, defaultBasePath, defaultFormatter, defaultTasks);

            assertCopiedStructure([defaultFile], 'app', defaultBasePath);
        });

        it('should call #logInclude() with the specified arguments', function () {

            let copiedPaths = [
                'app\\file1'
            ];

            copyFilesMock.copyStructure.stub.returns(copiedPaths);

            let instance = createNewIncluder();

            let logIncludesSpy = sinon.spy(instance, 'logInclude');

            instance.copyAndIncludeFile(defaultPlaceholder, defaultFile, defaultBasePath, defaultFormatter, defaultTasks);

            let expectedPaths = [
                'file1'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultPlaceholder, expectedPaths, defaultFormatter, defaultTasks);
        });
    });

    describe('#copyAndIncludeDirectory()', function () {

        let defaultName = 'name',
            defaultFiles = [
                'dir/path/file1',
                'dir/path/file2',
                'dir/path1/file1',
                'dir/path1/file2'
            ],
            defaultBasePath = 'dir',
            defaultFormatter = 'formatter',
            defaultTasks = ['task1', 'task2'],
            defaultDirectory = 'dir';

        describe('validation', function () {

            it('should throw if passed a falsy name', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeDirectory(null, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks, defaultDirectory);

                }).to.throw(/name is invalid/);
            });

            it('should throw if passed a falsy files argument', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeDirectory(defaultName, null, defaultBasePath, defaultFormatter, defaultTasks, defaultDirectory);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if files is not an array', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeDirectory(defaultName, 'files', defaultBasePath, defaultFormatter, defaultTasks, defaultDirectory);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if passed a falsy basePath', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeDirectory(defaultName, defaultFiles, null, defaultFormatter, defaultTasks, defaultDirectory);

                }).to.throw(/base path is invalid/);
            });

            it('should throw if passed a falsy formatter', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeDirectory(defaultName, defaultFiles, defaultBasePath, null, defaultTasks, defaultDirectory);

                }).to.throw(/formatter is invalid/);

            });

            it('should throw if the tasks argument is not an array', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeDirectory(defaultName, defaultFiles, defaultBasePath, defaultFormatter, 'tasks', defaultDirectory);

                }).to.throw(/tasks argument is not an array/);

            });
        });

        it('should call #copyFiles.copyStructure() with the specified arguments', function () {

            copyFilesMock.copyStructure.stub.returns([]);

            let instance = createNewIncluder();

            instance.copyAndIncludeDirectory(defaultName, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks, defaultDirectory);

            assertCopiedStructure(defaultFiles, 'app\\dir', defaultBasePath);
        });

        it('should call #logInclude() with the specified arguments', function () {

            let copiedPaths = [
                'app\\dir\\path\\file1',
                'app\\dir\\path\\file2',
                'app\\dir\\path1\\file1',
                'app\\dir\\path1\\file2'
            ];

            copyFilesMock.copyStructure.stub.returns(copiedPaths);

            let instance = createNewIncluder();

            let logIncludesSpy = sinon.spy(instance, 'logInclude');

            instance.copyAndIncludeDirectory(defaultName, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks, defaultDirectory);

            let expectedPaths = [
                'dir\\path\\file1',
                'dir\\path\\file2',
                'dir\\path1\\file1',
                'dir\\path1\\file2'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultName, expectedPaths, defaultFormatter, defaultTasks);
        });
    });

    describe('#copyAndIncludeFiles()', function () {

        let defaultName = 'name',
            defaultFiles = [
                'dir/path/file1',
                'dir/path/file2',
                'dir/path1/file1',
                'dir/path1/file2'
            ],
            defaultBasePath = 'dir',
            defaultFormatter = 'formatter',
            defaultTasks = ['task1', 'task2'];

        describe('validation', function () {

            it('should throw if passed a falsy name', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFiles(null, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/name is invalid/);
            });

            it('should throw if passed a falsy files argument', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFiles(defaultName, null, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if files is not an array', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFiles(defaultName, 'files', defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if passed a falsy basePath', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFiles(defaultName, defaultFiles, null, defaultFormatter, defaultTasks);

                }).to.throw(/base path is invalid/);
            });

            it('should throw if passed a falsy formatter', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFiles(defaultName, defaultFiles, defaultBasePath, null, defaultTasks);

                }).to.throw(/formatter is invalid/);

            });

            it('should throw if the tasks argument is not an array', function () {

                let instance = createNewIncluder();

                expect(function () {

                    instance.copyAndIncludeFiles(defaultName, defaultFiles, defaultBasePath, defaultFormatter, 'tasks');

                }).to.throw(/tasks argument is not an array/);

            });
        });

        it('should call #copyFiles.copyStructure() with the specified arguments', function () {

            copyFilesMock.copyStructure.stub.returns([]);

            let instance = createNewIncluder();

            instance.copyAndIncludeFiles(defaultName, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks);

            assertCopiedStructure(defaultFiles, 'app', defaultBasePath);
        });

        it('should call #logInclude() with the specified arguments', function () {

            let copiedPaths = [
                'app\\path\\file1',
                'app\\path\\file2',
                'app\\path1\\file1',
                'app\\path1\\file2'
            ];

            copyFilesMock.copyStructure.stub.returns(copiedPaths);

            let instance = createNewIncluder();

            let logIncludesSpy = sinon.spy(instance, 'logInclude');

            instance.copyAndIncludeFiles(defaultName, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks);

            let expectedPaths = [
                'path\\file1',
                'path\\file2',
                'path1\\file1',
                'path1\\file2'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultName, expectedPaths, defaultFormatter, defaultTasks);
        });
    });
});