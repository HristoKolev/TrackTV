'use strict';

const assertCompositionMultitest = require('../testing/assertComposition').multitest,
    chai = require('chai'),
    sinonChai = require("sinon-chai"),
    expect = chai.expect,
    mockHelper = require('../testing/mockHelper'),
    appBuilderMock = require('./mocks/appBuilderMock');

chai.use(sinonChai);

const basePath = '/base/path';
const appPath = 'app';
const defaultOutputPath = 'output';

const mock = appBuilderMock(appPath, basePath);

const fsMock = mockHelper('fs', {
    statSync: [mock.stat]
});

const appMock = mockHelper(null, {

    appPath: ['stub', 'spy'],
    getModules: ['spy', mock.getModules],
    getSubmodules: ['spy', mock.getSubmodules]
});

const copyContent = mockHelper.require('../modules/copyContent');

describe('#copyContent()', function () {

    assertCompositionMultitest.function(copyContent, 'copyContent');

    beforeEach(function () {

        fsMock.resetMocks();
        appMock.resetMocks();

        appMock.appPath.stub.returns(appPath);
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

        copyContent(appMock.mock, defaultOutputPath);

        expect(appMock.getModules.spy).to.be.calledOnce;
    });

    it('should return the right paths', function () {

        let result = copyContent(appMock.mock, defaultOutputPath);

        let expected = [
            {
                targetPath: '\\base\\path\\app\\module1\\sub1\\include',
                destinationPath: 'output\\include\\local\\module1\\sub1'
            },
            {
                targetPath: '\\base\\path\\app\\module1\\sub1\\sub2\\content',
                destinationPath: 'output\\content\\local\\module1\\sub1\\sub2'
            },
            {
                targetPath: '\\base\\path\\app\\module2\\sub1\\include',
                destinationPath: 'output\\include\\local\\module2\\sub1'
            },
            {
                targetPath: '\\base\\path\\app\\module2\\sub1\\sub2\\content',
                destinationPath: 'output\\content\\local\\module2\\sub1\\sub2'
            },
            {
                targetPath: '\\base\\path\\app\\module1\\include',
                destinationPath: 'output\\include\\module\\module1'
            },
            {
                targetPath: '\\base\\path\\app\\module2\\content',
                destinationPath: 'output\\content\\module\\module2'
            },
            {
                targetPath: 'app\\global_content',
                destinationPath: 'output\\content\\global'
            },
            {
                targetPath: 'app\\global_include',
                destinationPath: 'output\\include\\global'
            }];

        expect(result).to.deep.equal(expected);
    });
});