'use strict';

const assertCompositionMultitest = require('../testing/assertComposition').multitest,
    chai = require('chai'),
    sinonChai = require("sinon-chai"),
    expect = chai.expect,
    mockHelper = require('../testing/mockHelper');

chai.use(sinonChai);

const fsMock = mockHelper('fs', {
    statSync: ['stub']
});

const appBuilderMock = mockHelper(null, {

    getModules: ['stub', 'spy'],
    appPath: ['stub', 'spy']
});

const copyContent = mockHelper.require('../modules/copyContent');

describe('#copyContent()', function () {

    beforeEach(function () {

        fsMock.resetMocks();
        appBuilderMock.resetMocks();
    });

    assertCompositionMultitest.function(copyContent, 'copyContent');

    const defaultOutputPath = 'output';
    let defaultAppPath = 'app';

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

        appBuilderMock.appPath.stub.returns(defaultAppPath);

        appBuilderMock.getModules.stub.returns([]);

        copyContent(appBuilderMock.mock, defaultOutputPath);

        expect(appBuilderMock.getModules.spy).to.be.calledOnce;
    });
});