'use strict';

const assertCompositionMultitest = require('../testing/assertComposition').multitest,
    chai = require('chai'),
    sinonChai = require("sinon-chai"),
    expect = chai.expect,
    mockHelper = require('../testing/mockHelper'),
    path = require('path');

chai.use(sinonChai);

const basePath = '/base/path';
const appPath = 'app';
const defaultOutputPath = 'output';

function stat(p) {

    let basePath = p.replace('\\content', '').replace('\\include', '');
    let isGlobal = p === path.join(appPath, 'global_include') || p === path.join(appPath, 'global_content');

    let lastCharAsANumber = parseInt(basePath[basePath.length - 1], 10);

    let isEven = () => lastCharAsANumber % 2 === 0;
    let isOdd = () => lastCharAsANumber % 2 !== 0;

    let isDirectory;

    if (p.endsWith('\\content')) {

        isDirectory = isEven;

    } else if (p.endsWith('\\include')) {

        isDirectory = isOdd;
    }
    else if (isGlobal) {

        isDirectory = () => true;
    }
    else {
        isDirectory = () => false;
    }

    return {
        isDirectory
    };
}

const fsMock = mockHelper('fs', {
    statSync: [stat]
});

function getModules() {

    function format(name) {

        return {
            name,
            fullPath: path.join(basePath, appPath, name)
        };
    }

    return [
        format('module1'),
        format('module2')
    ];
}

function getSubmodules(fullPath) {

    function format(name) {

        return {
            name,
            fullPath: path.join(fullPath, name)
        };
    }

    return [
        format('sub1'),
        format('sub1/sub2')
    ];
}

const appBuilderMock = mockHelper(null, {

    appPath: ['stub', 'spy'],
    getModules: ['spy', getModules],
    getSubmodules: ['spy', getSubmodules]
});

const copyContent = mockHelper.require('../modules/copyContent');

describe('#copyContent()', function () {

    assertCompositionMultitest.function(copyContent, 'copyContent');

    beforeEach(function () {

        fsMock.resetMocks();
        appBuilderMock.resetMocks();

        appBuilderMock.appPath.stub.returns(appPath);
    });

    describe('validations', function () {

        it('should throw if the app builder is falsy', function () {

            expect(function () {

                copyContent(null, defaultOutputPath);
            }).to.throw(/app builder is invalid/);
        });

        it('should throw if the output path is falsy', function () {

            expect(function () {

                copyContent({}, null);
            }).to.throw(/app output path is invalid/);
        });

    });

    it('should call appBuilder.getModules()', function () {

        copyContent(appBuilderMock.mock, defaultOutputPath);

        expect(appBuilderMock.getModules.spy).to.be.calledOnce;
    });

    it('should return the right paths', function () {

        let result = copyContent(appBuilderMock.mock, defaultOutputPath);

        let expected = [
            {
                targetPath: '\\base\\path\\app\\module1\\sub1\\include',
                destinationPath: 'output\\include\\local\\module1\\sub1',
                basePath: '\\base\\path\\app\\module1\\sub1\\include'
            },
            {
                targetPath: '\\base\\path\\app\\module1\\sub1\\sub2\\content',
                destinationPath: 'output\\content\\local\\module1\\sub1\\sub2',
                basePath: '\\base\\path\\app\\module1\\sub1\\sub2\\content'
            },
            {
                targetPath: '\\base\\path\\app\\module2\\sub1\\include',
                destinationPath: 'output\\include\\local\\module2\\sub1',
                basePath: '\\base\\path\\app\\module2\\sub1\\include'
            },
            {
                targetPath: '\\base\\path\\app\\module2\\sub1\\sub2\\content',
                destinationPath: 'output\\content\\local\\module2\\sub1\\sub2',
                basePath: '\\base\\path\\app\\module2\\sub1\\sub2\\content'
            },
            {
                targetPath: '\\base\\path\\app\\module1\\include',
                destinationPath: 'output\\include\\module\\module1',
                basePath: '\\base\\path\\app\\module1\\include'
            },
            {
                targetPath: '\\base\\path\\app\\module2\\content',
                destinationPath: 'output\\content\\module\\module2',
                basePath: '\\base\\path\\app\\module2\\content'
            },
            {
                targetPath: 'app\\global_content',
                destinationPath: 'output\\content\\global',
                basePath: 'app\\global_content'
            },
            {
                targetPath: 'app\\global_include',
                destinationPath: 'output\\include\\global',
                basePath: 'app\\global_include'
            }];

        expect(result).to.deep.equal(expected);
    });
});