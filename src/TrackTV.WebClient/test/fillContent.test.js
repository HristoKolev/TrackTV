"use strict";

var expect = require('chai').expect,
    sinon = require('sinon'),
    mockery = require('mockery');

var assertCompositionMultitest = require('../testing/assertComposition').multitest,
    assertComposition = require('../testing/assertComposition');

var fs = {
    readFileSync: function () {
    },

    writeFileSync: function () {
    }
};

var fsMock = sinon.mock(fs);

mockery.registerMock('fs', fs);

mockery.registerAllowable('../modules/fillContent');

mockery.enable();

var fillContent = require('../modules/fillContent');

mockery.disable();

describe('#fillContent()', function () {

    describe('module exports', function () {

        assertCompositionMultitest.function(fillContent, 'fillContent');
    });

    describe('#replaceContent()', function () {

        before(function () {

            mockery.enable();
        });

        beforeEach(function () {

            fsMock.restore();
        });

        after(function () {

            fsMock.restore();

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

            fsMock.expects('readFileSync').once().withArgs(defaultDestination).returns('');

            fillContent(defaultDestination, defaultPlaceholder, defaultReplacement);

            fsMock.verify();
            fsMock.restore();
        });

        it('should write to the destination file', function () {

            fsMock.expects('readFileSync').once().withArgs(defaultDestination).returns('');

            fillContent(defaultDestination, defaultPlaceholder, defaultReplacement);

            fsMock.verify();
            fsMock.restore();

        });
    });
});