"use strict";

let chai = require('chai'),
    sinonChai = require("sinon-chai"),
    expect = chai.expect,
    sinon = require('sinon'),
    mockery = require('mockery'),
    mockHelper = require('../testing/mockHelper');

chai.use(sinonChai);

let pathChain = require('../modules/pathChain');

let assertCompositionMultitest = require('../testing/assertComposition').multitest;

let fsMock = mockHelper('fs', {
    readFileSync: ['stub', 'spy'],
    writeFileSync: ['spy']
});

let copyFilesMock = mockHelper('./copyFiles', {
    copy: ['stub', 'spy'],
    copyStructure: ['stub', 'spy']
});

let fillContentMock = mockHelper.mockFunction('./fillContent', ['spy']);

mockery.registerAllowables([
    './fillContent',
    './list-resources',
    './copyFiles',
    'path'
]);

let includer = mockHelper.require('../modules/includer');

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
            ['instance', 'function']
        ]);
    });

    let defaultOutput = pathChain.instance('app'),
        defaultLogFile = 'app\\includes.json',
        defaultIndex = 'index.html',
        defaultOutputIndex = 'app\\index.html';

    function createDefaultInstance() {

        return includer.instance(defaultIndex, defaultOutput);
    }

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

    describe('instance exports', function () {

        let instance = createDefaultInstance();

        assertCompositionMultitest.object('instance', instance, [
            ['formatters', 'object'],
            ['createIncludeLog', 'function'],
            ['logInclude', 'function'],
            ['copyIndex', 'function'],
            ['updateIncludes', 'function'],

            ['readIncludes', 'function'],
            ['writeIncludes', 'function'],

            ['includeDirectory', 'function'],
            ['includeFile', 'function'],
            ['includeModuleFiles', 'function'],
            ['includeSeparatedModuleFiles', 'function']
        ]);
    });

    describe('instance validation', function () {

        it('should throw if the output is falsy', function () {

            expect(function () {
                includer.instance(defaultIndex, null);

            }).to.throw(/output is invalid/);
        });

        it('should throw if the index file is falsy', function () {

            expect(function () {
                includer.instance(null, defaultOutput);

            }).to.throw(/index file is invalid/);
        });
    });

    describe('#formatters', function () {

        let instance = createDefaultInstance();

        assertCompositionMultitest.object('formatters', instance.formatters, [
            ['scriptFormatter', 'string'],
            ['styleFormatter', 'string']
        ]);

        it('should have #scriptFormatter with the right value', function () {

            expect(instance.formatters.scriptFormatter).to.equal('script');
        });

        it('should have #styleFormatter with the right value', function () {

            expect(instance.formatters.styleFormatter).to.equal('style');
        });

    });

    describe('#createIncludeLog()', function () {

        it('should write to the includes file', function () {

            let instance = createDefaultInstance();

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

                let instance = createDefaultInstance();

                expect(function () {

                    instance.logInclude(null, defaultFiles, defaultFormatter, defaultTasks);

                }).to.throw(/name is invalid/);
            });

            it('should throw if the files array is falsy', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.logInclude(defaultName, null, defaultFormatter, defaultTasks);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if the files is not an array', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.logInclude(defaultName, 'files', defaultFormatter, defaultTasks);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if the formatter is falsy', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.logInclude(defaultName, defaultFiles, null, defaultTasks);

                }).to.throw(/formatter is invalid/);
            });

            it('should throw if the tasks argument is not an array', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.logInclude(defaultName, defaultFiles, defaultFormatter, 'tasks');

                }).to.throw(/tasks argument is not an array/);
            });
        });

        it('should read the includes file', function () {

            let instance = createDefaultInstance();

            returnEmptyArray(defaultLogFile);

            instance.logInclude(defaultName, defaultFiles, defaultFormatter, defaultTasks);

            expect(fsMock.readFileSync.spy).to.be.calledOnce;
        });

        it('should write to the includes file', function () {

            let instance = createDefaultInstance();

            returnEmptyArray(defaultLogFile);

            instance.logInclude(defaultName, defaultFiles, defaultFormatter, defaultTasks);

            expect(fsMock.writeFileSync.spy).to.be.calledOnce;
            expect(fsMock.writeFileSync.spy).to.be.always.calledWith(defaultLogFile);
        });

        it('should add the includes to the log file', function () {

            let instance = createDefaultInstance();

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

    describe('#copyIndex()', function () {

        it('should call #copyFiles.copy() with the index file and the output location.', function () {

            let instance = createDefaultInstance();

            instance.copyIndex();

            expect(copyFilesMock.copy.spy).to.be.calledOnce;
            expect(copyFilesMock.copy.spy).to.be.always.calledWithExactly(defaultIndex, 'app');
        });
    });

    describe('#updateIncludes()', function () {

        it('should call #copyIndex()', function () {

            let instance = createDefaultInstance();

            let copyIndexSpy = sinon.spy(instance, 'copyIndex');

            instance.updateIncludes();

            expect(copyIndexSpy).to.be.calledOnce;
        });

        it('should read the includes file', function () {

            let instance = createDefaultInstance();

            instance.updateIncludes();

            expect(fsMock.readFileSync.spy).to.be.calledOnce;
            expect(fsMock.readFileSync.spy).to.be.always.calledWithExactly(defaultLogFile);
        });

        it('should call #fillContent() for every include', function () {

            let instance = createDefaultInstance();

            registerIncludes(defaultIncludes);

            instance.updateIncludes();

            for (let i = 0; i < defaultIncludes.length; i += 1) {

                let include = defaultIncludes[i];

                expect(fillContentMock.spy).to.be.calledWith(defaultOutputIndex, include.name);
            }
        });

        it('should call #fillContent() with the correct formatted includes', function () {

            registerIncludes(defaultIncludes);

            let instance = createDefaultInstance();

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

            let instance = createDefaultInstance();

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

            let instance = createDefaultInstance();

            expect(function () {

                instance.updateIncludes();

            }).to.throw(/Unknown formatter name/);
        });
    });

    describe('#readIncludes()', function () {

        it('should read the includes file', function () {

            registerIncludes(defaultIncludes);

            let instance = createDefaultInstance();

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

                    let instance = createDefaultInstance();
                    instance.writeIncludes(null);

                }).to.throw(/includes are invalid/);
            });

            it('should throw if the includes are not an array', function () {

                expect(function () {

                    let instance = createDefaultInstance();

                    instance.writeIncludes('includes');

                }).to.throw(/includes are not an array/);
            });
        });

        it('should write the includes to the file', function () {

            let instance = createDefaultInstance();

            instance.writeIncludes(defaultIncludes);

            expect(fsMock.writeFileSync.spy).to.be.calledOnce;
            expect(fsMock.writeFileSync.spy).to.be.always.calledWith(defaultLogFile);

            let includes = JSON.parse(fsMock.writeFileSync.spy.args[0][1]);

            expect(includes).to.deep.equal(defaultIncludes);
        });
    });

    function assertCopiedStructure(files, output, baseDir) {

        expect(copyFilesMock.copyStructure.spy).to.be.calledOnce;

        expect(copyFilesMock.copyStructure.spy).to.be.always.calledWith(files, output, baseDir);
    }

    function assertCopied(files, output) {

        expect(copyFilesMock.copy.spy).to.be.calledOnce;

        expect(copyFilesMock.copy.spy).to.be.always.calledWith(files, output);
    }

    function assertLoggedIncludes(spy, name, paths, formatter, tasks) {

        expect(spy).to.be.calledOnce;

        expect(spy).to.be.always.calledWithExactly(name, paths, formatter, tasks);
    }

    describe('#includeDirectory()', function () {

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

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(null, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/name is invalid/);
            });

            it('should throw if passed a falsy files argument', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(defaultName, null, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if files is not an array', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(defaultName, 'files', defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if passed a falsy basePath', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(defaultName, defaultFiles, null, defaultFormatter, defaultTasks);

                }).to.throw(/base path is invalid/);
            });

            it('should throw if passed a falsy formatter', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(defaultName, defaultFiles, defaultBasePath, null, defaultTasks);

                }).to.throw(/formatter is invalid/);

            });

            it('should throw if the tasks argument is not an array', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(defaultName, defaultFiles, defaultBasePath, defaultFormatter, 'tasks');

                }).to.throw(/tasks argument is not an array/);

            });
        });

        it('should call #copyFiles.copyStructure() with the specified arguments', function () {

            copyFilesMock.copyStructure.stub.returns([]);

            let instance = createDefaultInstance();

            instance.includeDirectory(defaultName, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks);

            assertCopiedStructure(defaultFiles, 'app\\name', defaultBasePath);
        });

        it('should call #logInclude() with the specified arguments', function () {

            let copiedPaths = [
                'app\\name\\path\\file1',
                'app\\name\\path\\file2',
                'app\\name\\path1\\file1',
                'app\\name\\path1\\file2'
            ];

            copyFilesMock.copyStructure.stub.returns(copiedPaths);

            let instance = createDefaultInstance();

            let logIncludesSpy = sinon.spy(instance, 'logInclude');

            instance.includeDirectory(defaultName, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks);

            let expectedPaths = [
                'name\\path\\file1',
                'name\\path\\file2',
                'name\\path1\\file1',
                'name\\path1\\file2'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultName, expectedPaths, defaultFormatter, defaultTasks);
        });
    });

    describe('#includeFile()', function () {

        let defaultPlaceholder = 'placeholder',
            defaultFile = 'source/file1',
            defaultBasePath = 'source',
            defaultFormatter = 'formatter',
            defaultTasks = ['task1', 'task2'];

        describe('validation', function () {

            it('should throw if passed a falsy name', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeFile(null, defaultFile, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/placeholder is invalid/);
            });

            it('should throw if passed a falsy file', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeFile(defaultPlaceholder, null, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/file is invalid/);
            });

            it('should throw if passed a falsy basePath', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeFile(defaultPlaceholder, defaultFile, null, defaultFormatter, defaultTasks);

                }).to.throw(/base path is invalid/);
            });

            it('should throw if passed a falsy formatter', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeFile(defaultPlaceholder, defaultFile, defaultBasePath, null, defaultTasks);

                }).to.throw(/formatter is invalid/);

            });

            it('should throw if the tasks argument is not an array', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeFile(defaultPlaceholder, defaultFile, defaultBasePath, defaultFormatter, 'tasks');

                }).to.throw(/tasks argument is not an array/);

            });
        });

        it('should call #copyFiles.copyStructure() with the specified arguments', function () {

            copyFilesMock.copyStructure.stub.returns([]);

            let instance = createDefaultInstance();

            instance.includeFile(defaultPlaceholder, defaultFile, defaultBasePath, defaultFormatter, defaultTasks);

            assertCopiedStructure([defaultFile], 'app', defaultBasePath);
        });

        it('should call #logInclude() with the specified arguments', function () {

            let copiedPaths = [
                'app\\file1'
            ];

            copyFilesMock.copyStructure.stub.returns(copiedPaths);

            let instance = createDefaultInstance();

            let logIncludesSpy = sinon.spy(instance, 'logInclude');

            instance.includeFile(defaultPlaceholder, defaultFile, defaultBasePath, defaultFormatter, defaultTasks);

            let expectedPaths = [
                'file1'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultPlaceholder, expectedPaths, defaultFormatter, defaultTasks);
        });
    });

    describe('#includeSeparatedModuleFiles()', function () {

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

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(null, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/name is invalid/);
            });

            it('should throw if passed a falsy files argument', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(defaultName, null, defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if files is not an array', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(defaultName, 'files', defaultBasePath, defaultFormatter, defaultTasks);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if passed a falsy basePath', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(defaultName, defaultFiles, null, defaultFormatter, defaultTasks);

                }).to.throw(/base path is invalid/);
            });

            it('should throw if passed a falsy formatter', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(defaultName, defaultFiles, defaultBasePath, null, defaultTasks);

                }).to.throw(/formatter is invalid/);

            });

            it('should throw if the tasks argument is not an array', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(defaultName, defaultFiles, defaultBasePath, defaultFormatter, 'tasks');

                }).to.throw(/tasks argument is not an array/);

            });
        });

        it('should call #copyFiles.copyStructure() with the specified arguments', function () {

            copyFilesMock.copyStructure.stub.returns([]);

            let instance = createDefaultInstance();

            instance.includeSeparatedModuleFiles(defaultName, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks);

            assertCopiedStructure(defaultFiles, 'app\\name', defaultBasePath);
        });

        it('should call #logInclude() with the specified arguments', function () {

            let copiedPaths = [
                'app\\path\\file1',
                'app\\path\\file2',
                'app\\path1\\file1',
                'app\\path1\\file2'
            ];

            copyFilesMock.copyStructure.stub.returns(copiedPaths);

            let instance = createDefaultInstance();

            let logIncludesSpy = sinon.spy(instance, 'logInclude');

            instance.includeSeparatedModuleFiles(defaultName, defaultFiles, defaultBasePath, defaultFormatter, defaultTasks);

            let expectedPaths = [
                'path\\file1',
                'path\\file2',
                'path1\\file1',
                'path1\\file2'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultName, expectedPaths, defaultFormatter, defaultTasks);
        });

    });

    describe('#includeModuleFiles()', function () {

        let defaultName = 'name',
            defaultFiles = [
                'dir/path/file1',
                'dir/path/file2',
                'dir/path1/file1',
                'dir/path1/file2'
            ],
            defaultFormatter = 'formatter',
            defaultTasks = ['task1', 'task2'];

        describe('validation', function () {

            it('should throw if passed a falsy name', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeModuleFiles(null, defaultFiles, defaultFormatter, defaultTasks);

                }).to.throw(/name is invalid/);
            });

            it('should throw if passed a falsy files argument', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeModuleFiles(defaultName, null, defaultFormatter, defaultTasks);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if files is not an array', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeModuleFiles(defaultName, 'files', defaultFormatter, defaultTasks);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if passed a falsy formatter', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeModuleFiles(defaultName, defaultFiles, null, defaultTasks);

                }).to.throw(/formatter is invalid/);

            });

            it('should throw if the tasks argument is not an array', function () {

                let instance = createDefaultInstance();

                expect(function () {

                    instance.includeModuleFiles(defaultName, defaultFiles, defaultFormatter, 'tasks');

                }).to.throw(/tasks argument is not an array/);

            });
        });

        it('should call #copyFiles.copy() with the specified arguments', function () {

            copyFilesMock.copy.stub.returns([]);

            let instance = createDefaultInstance();

            instance.includeModuleFiles(defaultName, defaultFiles, defaultFormatter, defaultTasks);

            assertCopied(defaultFiles, 'app\\name');
        });

        it('should call #logInclude() with the specified arguments', function () {

            let copiedPaths = [
                'app\\name\\path-file1',
                'app\\name\\path-file2',
                'app\\name\\path1-file1',
                'app\\name\\path1-file2'
            ];

            copyFilesMock.copy.stub.returns(copiedPaths);

            let instance = createDefaultInstance();

            let logIncludesSpy = sinon.spy(instance, 'logInclude');

            instance.includeModuleFiles(defaultName, defaultFiles, defaultFormatter, defaultTasks);

            let expectedPaths = [
                'name\\path-file1',
                'name\\path-file2',
                'name\\path1-file1',
                'name\\path1-file2'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultName, expectedPaths, defaultFormatter, defaultTasks);
        });
    });
});