"use strict";

var expect = require('chai').expect,
    sinon = require('sinon'),
    mockery = require('mockery');

var pathChain = require('../modules/pathChain');

var assertCompositionMultitest = require('../testing/assertComposition').multitest;

var readStub = sinon.stub();

var fs = {
    readFileSync: function () {

        return readStub.apply(this, arguments);
    },
    writeFileSync: function () {
    }
};

var readSpy = sinon.spy(fs, 'readFileSync');
var writeSpy = sinon.spy(fs, 'writeFileSync');

mockery.registerMock('fs', fs);

var copyStructureStub = sinon.stub();
var copyStub = sinon.stub();

var copyFiles = {
    copy: function () {

        return copyStub.apply(this, arguments);
    },
    copyStructure: function () {

        return copyStructureStub.apply(this, arguments);
    }
};

var copySpy = sinon.spy(copyFiles, 'copy');
var copyStructureSpy = sinon.spy(copyFiles, 'copyStructure');

mockery.registerMock('./copyFiles', copyFiles);

var fillContent = {
    func: function () {
    }
};

var fillContentSpy = sinon.spy(fillContent, 'func');

mockery.registerMock('./fillContent', fillContent.func);

function resetMocks() {

    readStub.reset();
    readSpy.reset();

    writeSpy.reset();

    copySpy.reset();
    copyStub.reset();

    copyStructureSpy.reset();
    copyStructureStub.reset();

    fillContentSpy.reset();
}

function mockRequire(moduleName) {

    mockery.registerAllowable(moduleName);

    mockery.enable();

    var module = require(moduleName);

    mockery.disable();

    return module;
}

mockery.registerAllowable('./fillContent');
mockery.registerAllowable('./list-resources');
mockery.registerAllowable('./copyFiles');

var includer = mockRequire('../modules/includer');

describe('#includer', function () {

    describe('module exports', function () {

        assertCompositionMultitest.object('includer', includer, [
            ['instance', 'function']
        ]);
    });

    var defaultOutput = pathChain.instance('app'),
        defaultLogFile = 'app\\includes.json',
        defaultIndex = 'index.html',
        defaultOutputIndex = 'app\\index.html';

    function createDefaultInstance() {

        return includer.instance(defaultIndex, defaultOutput);
    }

    function returnEmptyArray(name) {

        readStub.withArgs(name).returns('[]');
    }

    describe('instance exports', function () {

        var instance = createDefaultInstance();

        assertCompositionMultitest.object('instance', instance, [
            ['formatters', 'object'],
            ['createIncludeLog', 'function'],
            ['logIncludes', 'function'],
            ['copyIndex', 'function'],
            ['updateIncludes', 'function'],
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

        var instance = createDefaultInstance();

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

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            resetMocks();
        });

        after(function () {

            resetMocks();

            mockery.disable();
        });

        it('should write to the includes file', function () {

            var instance = createDefaultInstance();

            instance.createIncludeLog();

            expect(writeSpy.calledOnce).to.be.true;

            expect(writeSpy.alwaysCalledWithExactly(defaultLogFile, '[]')).to.be.true;
        });

    });

    describe('#logIncludes()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            resetMocks();
        });

        after(function () {

            resetMocks();

            mockery.disable();
        });

        var defaultName = 'name';
        var defaultFiles = ['file1', 'file2', 'file3'];
        var defaultFormatter = 'formatter';

        it('should throw if the name is falsy', function () {

            var instance = createDefaultInstance();

            expect(function () {

                instance.logIncludes(null, defaultFiles, defaultFormatter);

            }).to.throw(/name is invalid/);
        });

        it('should throw if the files array is falsy', function () {

            var instance = createDefaultInstance();

            expect(function () {

                instance.logIncludes(defaultName, null, defaultFormatter);

            }).to.throw(/files argument is invalid/);
        });

        it('should throw if the files is not an array', function () {

            var instance = createDefaultInstance();

            expect(function () {

                instance.logIncludes(defaultName, 'files', defaultFormatter);

            }).to.throw(/files argument is not an array/);
        });

        it('should throw if the files is not an array', function () {

            var instance = createDefaultInstance();

            expect(function () {

                instance.logIncludes(defaultName, defaultFiles, null);

            }).to.throw(/formatter is invalid/);
        });

        it('should read the includes file', function () {

            var instance = createDefaultInstance();

            returnEmptyArray(defaultLogFile);

            instance.logIncludes(defaultName, defaultFiles, defaultFormatter);

            expect(readSpy.calledOnce).to.be.true;
        });

        it('should write to the includes file', function () {

            var instance = createDefaultInstance();

            returnEmptyArray(defaultLogFile);

            instance.logIncludes(defaultName, defaultFiles, defaultFormatter);

            expect(writeSpy.calledOnce).to.be.true;
            expect(writeSpy.alwaysCalledWith(defaultLogFile)).to.be.true;
        });

        it('should add the includes to the log file', function () {

            var instance = createDefaultInstance();

            returnEmptyArray(defaultLogFile);

            instance.logIncludes(defaultName, defaultFiles, defaultFormatter);

            var expected = [
                {
                    name: 'name',
                    files: [
                        'file1',
                        'file2',
                        'file3'
                    ],
                    formatter: 'formatter'
                }
            ];

            var result = JSON.parse(writeSpy.args[0][1]);

            expect(result).to.deep.equal(expected);
        });
    });

    describe('#copyIndex()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            resetMocks();
        });

        after(function () {

            resetMocks();

            mockery.disable();
        });

        it('should call #copyFiles.copy() with the index file and the output location.', function () {

            var instance = createDefaultInstance();

            instance.copyIndex();

            expect(copySpy.calledOnce).to.be.true;
            expect(copySpy.alwaysCalledWithExactly(defaultIndex, 'app')).to.be.true;
        });

    });

    describe('#updateIncludes()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            resetMocks();
        });

        after(function () {

            resetMocks();

            mockery.disable();
        });

        var defaultIncludes = [
            {
                name: 'name',
                files: [
                    'file1',
                    'file2',
                    'file3'
                ],
                formatter: 'script'
            }
        ];

        function registerIncludes(includes) {

            readStub.withArgs(defaultLogFile).returns(JSON.stringify(includes));
        }

        it('should call #copyIndex()', function () {

            var instance = createDefaultInstance();

            var copyIndexSpy = sinon.spy(instance, 'copyIndex');

            instance.updateIncludes();

            expect(copyIndexSpy.calledOnce).to.be.true;
        });

        it('should read the includes file', function () {

            var instance = createDefaultInstance();

            instance.updateIncludes();

            expect(readSpy.calledOnce).to.be.true;
            expect(readSpy.alwaysCalledWithExactly(defaultLogFile)).to.be.true;

        });

        it('should call #fillContent() for every include', function () {

            var instance = createDefaultInstance();

            registerIncludes(defaultIncludes);

            instance.updateIncludes();

            for (var i = 0; i < defaultIncludes.length; i += 1) {

                var include = defaultIncludes[i];

                expect(fillContentSpy.calledWith(defaultOutputIndex, include.name)).to.be.true;
            }
        });

        it('should call #fillContent() with the correct formatted includes', function () {

            registerIncludes(defaultIncludes);

            var instance = createDefaultInstance();

            instance.updateIncludes();

            var expected = [
                '<script src="file1"></script>\n' +
                '<script src="file2"></script>\n' +
                '<script src="file3"></script>'
            ]

            for (var i = 0; i < defaultIncludes.length; i += 1) {

                var include = defaultIncludes[i];

                expect(fillContentSpy.calledWithExactly(defaultOutputIndex, include.name, expected[i])).to.be.true;
            }
        });

        it('should format the includes with the correct formatter', function () {

            var includes = [
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

            var expected = [
                '<script src="file1"></script>\n' +
                '<script src="file2"></script>\n' +
                '<script src="file3"></script>',

                '<link rel="stylesheet" href="style1">\n' +
                '<link rel="stylesheet" href="style2">\n' +
                '<link rel="stylesheet" href="style3">'
            ];

            var instance = createDefaultInstance();

            instance.updateIncludes();

            for (var i = 0; i < includes.length; i += 1) {

                var include = includes[i];

                expect(fillContentSpy.calledWithExactly(defaultOutputIndex, include.name, expected[i])).to.be.true;
            }
        });

        it('should throw if the includes refer to non existent formatter', function () {

            var includes = [
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

            var instance = createDefaultInstance();

            expect(function () {

                instance.updateIncludes();

            }).to.throw(/Unknown formatter name/);
        });
    });

    function assertCopiedStructure(files, output, baseDir) {

        expect(copyStructureSpy.calledOnce).to.be.true;

        expect(copyStructureSpy.alwaysCalledWith(files, output, baseDir)).to.be.true;
    }

    function assertCopied(files, output) {

        expect(copySpy.calledOnce).to.be.true;

        expect(copySpy.alwaysCalledWith(files, output)).to.be.true;
    }

    function assertLoggedIncludes(spy, name, paths, formatter) {

        expect(spy.calledOnce).to.be.true;

        expect(spy.alwaysCalledWithExactly(name, paths, formatter)).to.be.true;
    }

    describe('#includeDirectory()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            resetMocks();
        });

        after(function () {

            resetMocks();

            mockery.disable();
        });

        var defaultName = 'name',
            defaultFiles = [
                'dir/path/file1',
                'dir/path/file2',
                'dir/path1/file1',
                'dir/path1/file2'
            ],
            defaultBasePath = 'dir',
            defaultFormatter = 'formatter';

        describe('validation', function () {

            it('should throw if passed a falsy name', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(null, defaultFiles, defaultBasePath, defaultFormatter);

                }).to.throw(/name is invalid/);
            });

            it('should throw if passed a falsy files argument', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(defaultName, null, defaultBasePath, defaultFormatter);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if files is not an array', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(defaultName, 'files', defaultBasePath, defaultFormatter);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if passed a falsy basePath', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(defaultName, defaultFiles, null, defaultFormatter);

                }).to.throw(/base path is invalid/);
            });

            it('should throw if passed a falsy formatter', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeDirectory(defaultName, defaultFiles, defaultBasePath, null);

                }).to.throw(/formatter is invalid/);

            });
        });

        it('should call #copyFiles.copyStructure() with the specified arguments', function () {

            copyStructureStub.returns([]);

            var instance = createDefaultInstance();

            instance.includeDirectory(defaultName, defaultFiles, defaultBasePath, defaultFormatter);

            assertCopiedStructure(defaultFiles, 'app\\name', defaultBasePath);
        });

        it('should call #logIncludes() with the specified arguments', function () {

            var copiedPaths = [
                'app\\name\\path\\file1',
                'app\\name\\path\\file2',
                'app\\name\\path1\\file1',
                'app\\name\\path1\\file2'
            ];

            copyStructureStub.returns(copiedPaths);

            var instance = createDefaultInstance();

            var logIncludesSpy = sinon.spy(instance, 'logIncludes');

            instance.includeDirectory(defaultName, defaultFiles, defaultBasePath, defaultFormatter);

            var expectedPaths = [
                'name\\path\\file1',
                'name\\path\\file2',
                'name\\path1\\file1',
                'name\\path1\\file2'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultName, expectedPaths, defaultFormatter);
        });
    });

    describe('#includeFile()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            resetMocks();
        });

        after(function () {

            resetMocks();

            mockery.disable();
        });

        var defaultPlaceholder = 'placeholder',
            defaultFile = 'source/file1',
            defaultBasePath = 'source',
            defaultFormatter = 'formatter';

        describe('validation', function () {

            it('should throw if passed a falsy name', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeFile(null, defaultFile, defaultBasePath, defaultFormatter);

                }).to.throw(/placeholder is invalid/);
            });

            it('should throw if passed a falsy file', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeFile(defaultPlaceholder, null, defaultBasePath, defaultFormatter);

                }).to.throw(/file is invalid/);
            });

            it('should throw if passed a falsy basePath', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeFile(defaultPlaceholder, defaultFile, null, defaultFormatter);

                }).to.throw(/base path is invalid/);
            });

            it('should throw if passed a falsy formatter', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeFile(defaultPlaceholder, defaultFile, defaultBasePath, null);

                }).to.throw(/formatter is invalid/);

            });
        });

        it('should call #copyFiles.copyStructure() with the specified arguments', function () {

            copyStructureStub.returns([]);

            var instance = createDefaultInstance();

            instance.includeFile(defaultPlaceholder, defaultFile, defaultBasePath, defaultFormatter);

            assertCopiedStructure([defaultFile], 'app', defaultBasePath);
        });

        it('should call #logIncludes() with the specified arguments', function () {

            var copiedPaths = [
                'app\\file1'
            ];

            copyStructureStub.returns(copiedPaths);

            var instance = createDefaultInstance();

            var logIncludesSpy = sinon.spy(instance, 'logIncludes');

            instance.includeFile(defaultPlaceholder, defaultFile, defaultBasePath, defaultFormatter);

            var expectedPaths = [
                'file1',
            ];

            assertLoggedIncludes(logIncludesSpy, defaultPlaceholder, expectedPaths, defaultFormatter);
        });

    });

    describe('#includeSeparatedModuleFiles()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            resetMocks();
        });

        after(function () {

            resetMocks();

            mockery.disable();
        });

        var defaultName = 'name',
            defaultFiles = [
                'dir/path/file1',
                'dir/path/file2',
                'dir/path1/file1',
                'dir/path1/file2'
            ],
            defaultBasePath = 'dir',
            defaultFormatter = 'formatter';

        describe('validation', function () {

            it('should throw if passed a falsy name', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(null, defaultFiles, defaultBasePath, defaultFormatter);

                }).to.throw(/name is invalid/);
            });

            it('should throw if passed a falsy files argument', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(defaultName, null, defaultBasePath, defaultFormatter);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if files is not an array', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(defaultName, 'files', defaultBasePath, defaultFormatter);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if passed a falsy basePath', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(defaultName, defaultFiles, null, defaultFormatter);

                }).to.throw(/base path is invalid/);
            });

            it('should throw if passed a falsy formatter', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeSeparatedModuleFiles(defaultName, defaultFiles, defaultBasePath, null);

                }).to.throw(/formatter is invalid/);

            });
        });

        it('should call #copyFiles.copyStructure() with the specified arguments', function () {

            copyStructureStub.returns([]);

            var instance = createDefaultInstance();

            instance.includeSeparatedModuleFiles(defaultName, defaultFiles, defaultBasePath, defaultFormatter);

            assertCopiedStructure(defaultFiles, 'app\\name', defaultBasePath);
        });

        it('should call #logIncludes() with the specified arguments', function () {

            var copiedPaths = [
                'app\\path\\file1',
                'app\\path\\file2',
                'app\\path1\\file1',
                'app\\path1\\file2'
            ];

            copyStructureStub.returns(copiedPaths);

            var instance = createDefaultInstance();

            var logIncludesSpy = sinon.spy(instance, 'logIncludes');

            instance.includeSeparatedModuleFiles(defaultName, defaultFiles, defaultBasePath, defaultFormatter);

            var expectedPaths = [
                'path\\file1',
                'path\\file2',
                'path1\\file1',
                'path1\\file2'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultName, expectedPaths, defaultFormatter);
        });

    });

    describe('#includeModuleFiles()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            resetMocks();
        });

        after(function () {

            resetMocks();

            mockery.disable();
        });

        var defaultName = 'name',
            defaultFiles = [
                'dir/path/file1',
                'dir/path/file2',
                'dir/path1/file1',
                'dir/path1/file2'
            ],
            defaultFormatter = 'formatter';

        describe('validation', function () {

            it('should throw if passed a falsy name', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeModuleFiles(null, defaultFiles, defaultFormatter);

                }).to.throw(/name is invalid/);
            });

            it('should throw if passed a falsy files argument', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeModuleFiles(defaultName, null, defaultFormatter);

                }).to.throw(/files argument is invalid/);
            });

            it('should throw if files is not an array', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeModuleFiles(defaultName, 'files', defaultFormatter);

                }).to.throw(/files argument is not an array/);
            });

            it('should throw if passed a falsy formatter', function () {

                var instance = createDefaultInstance();

                expect(function () {

                    instance.includeModuleFiles(defaultName, defaultFiles, null);

                }).to.throw(/formatter is invalid/);

            });
        });

        it('should call #copyFiles.copy() with the specified arguments', function () {

            copyStub.returns([]);

            var instance = createDefaultInstance();

            instance.includeModuleFiles(defaultName, defaultFiles, defaultFormatter);

            assertCopied(defaultFiles, 'app\\name');
        });

        it('should call #logIncludes() with the specified arguments', function () {

            var copiedPaths = [
                'app\\name\\path-file1',
                'app\\name\\path-file2',
                'app\\name\\path1-file1',
                'app\\name\\path1-file2'
            ];

            copyStub.returns(copiedPaths);

            var instance = createDefaultInstance();

            var logIncludesSpy = sinon.spy(instance, 'logIncludes');

            instance.includeModuleFiles(defaultName, defaultFiles, defaultFormatter);

            var expectedPaths = [
                'name\\path-file1',
                'name\\path-file2',
                'name\\path1-file1',
                'name\\path1-file2'
            ];

            assertLoggedIncludes(logIncludesSpy, defaultName, expectedPaths, defaultFormatter);
        });

    });
});