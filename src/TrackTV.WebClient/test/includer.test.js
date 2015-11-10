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

var copyFiles = {
    copy: function () {
    },
    copyStructure: function () {
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
    copyStructureSpy.reset();

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

    var defaultOutput = pathChain.instance('app');

    var defaultLogFile = 'app\\includes.json';

    var defaultIndex = 'index.html';

    var defaultOutputIndex = 'app\\index.html';

    function createDefaultInstance() {

        return includer.instance(defaultOutput, defaultIndex);
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

    describe('#formatters', function () {

        var instance = includer.instance(defaultOutput, {});

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
});