"use strict";

var chai = require('chai'),
    expect = chai.expect,
    sinonChai = require('sinon-chai'),
    sinon = require('sinon'),
    mockery = require('mockery');

chai.use(sinonChai);

var assertCompositionMultitest = require('../testing/assertComposition').multitest;

var readStub = sinon.stub();
var readSpy = sinon.spy(readStub);
var writeSpy = sinon.spy();

function resetMocks() {

    readStub.reset();
    readStub.returns('');

    readSpy.reset();
    writeSpy.reset();
}

var fs = {
    readFileSync: readSpy,
    writeFileSync: writeSpy
};

function mockRequire(moduleName) {

    mockery.registerAllowable(moduleName);

    mockery.enable();

    var module = require(moduleName);

    mockery.disable();

    return module;
}

mockery.registerMock('fs', fs);

var fillContent = mockRequire('../modules/fillContent');

describe('#fillContent()', function () {

    describe('module exports', function () {

        assertCompositionMultitest.function(fillContent, 'fillContent');
    });

    describe('#replaceContent()', function () {

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

        var defaultDestination = 'dest';
        var defaultPlaceholder = 'placeholder';
        var defaultReplacement = 'value';

        it('should throw if the destination path is falsy', function () {

            expect(function () {

                fillContent(null, defaultPlaceholder, defaultReplacement);

            }).to.throw(/destination path is invalid/);
        });

        it('should throw if the placeholder is falsy', function () {

            expect(function () {

                fillContent(defaultDestination, null, defaultReplacement);

            }).to.throw(/placeholder is invalid/);
        });

        it('should throw if the replacement is falsy', function () {

            expect(function () {

                fillContent(defaultDestination, defaultPlaceholder, null);

            }).to.throw(/replacement is invalid/);
        });

        it('should read the destination file', function () {

            fillContent(defaultDestination, defaultPlaceholder, defaultReplacement);

            expect(readSpy).to.be.calledWith(defaultDestination);
        });

        it('should write to the destination file', function () {

            fillContent(defaultDestination, defaultPlaceholder, defaultReplacement);

            expect(writeSpy).to.be.calledWith(defaultDestination);
        });

        it('should replace the placeholder with the replacer', function () {

            var replacement = '[new content]';

            var content = 'Lorem <!-- placeholder --> ipsum dolor sit amet, consectetur adipiscing elit.';

            readStub.returns(content);

            fillContent(defaultDestination, defaultPlaceholder, replacement);

            expect(readSpy).to.be.calledOnce;
            expect(readSpy).to.be.always.calledWithExactly(defaultDestination);

            var expectedContent = 'Lorem [new content] ipsum dolor sit amet, consectetur adipiscing elit.';

            expect(writeSpy).to.be.calledOnce;
            expect(writeSpy).to.be.always.calledWithExactly(defaultDestination, expectedContent);
        });

        it('should replace all occurrences of the placeholder', function () {

            var replacement = '[new content]';

            var content = 'Lorem <!-- placeholder --> ipsum dolor sit amet,' +
                ' consectetur adipiscing <!-- placeholder --> elit.';

            readStub.returns(content);

            fillContent(defaultDestination, defaultPlaceholder, replacement);

            var expectedContent = 'Lorem [new content] ipsum dolor sit amet,' +
                ' consectetur adipiscing [new content] elit.';

            expect(writeSpy).to.be.always.calledWithExactly(defaultDestination, expectedContent);
        });
    });
});