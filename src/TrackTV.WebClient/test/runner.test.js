'use strict';

var chai = require('chai'),
    expect = chai.expect,
    sinon = require('sinon'),
    chaiAsPromised = require("chai-as-promised"),
    sinonChai = require("sinon-chai"),
    mockery = require('mockery');

chai.use(chaiAsPromised);
chai.use(sinonChai);

var assertCompositionMultitest = require('../testing/assertComposition').multitest;

function mockRequire(moduleName) {

    mockery.registerAllowable(moduleName);

    mockery.enable();

    var module = require(moduleName);

    mockery.disable();

    return module;
}

var runner;

function resetRunner() {

    runner = mockRequire('../modules/runner');
}

var gulp = {
    src: function () {
    },
    dest: function () {
    }
};

var srcSpy = sinon.spy(gulp, 'src');
var destSpy = sinon.spy(gulp, 'dest');

function resetMocks() {

    srcSpy.reset();
    destSpy.reset();
}

resetRunner();

mockery.registerMock('gulp', gulp);

describe('#runner', function () {

    describe('module exports', function () {

        assertCompositionMultitest.object('runner', runner, [
            ['use', 'function'],
            ['run', 'function']
        ]);
    });

    describe('#use()', function () {

        describe('validation', function () {

            beforeEach(function () {

                resetRunner();
            });

            it('should throw if the module is falsy', function () {

                expect(function () {

                    runner.use(null);

                }).to.throw(/module is invalid/);
            });

            it('should throw if the module name is falsy', function () {

                expect(function () {

                    runner.use({
                        name: null,
                        task: function () {
                        }
                    });

                }).to.throw(/name is invalid/);
            });

            it('should throw if the task is falsy', function () {

                expect(function () {

                    runner.use({
                        name: 'name',
                        task: null
                    });

                }).to.throw(/task is invalid/);
            });

            it('should throw if module with the same name is already passed', function () {

                var name = 'name';

                runner.use({
                    name: name,
                    task: function () {
                    }
                });

                expect(function () {

                    runner.use({
                        name: name,
                        task: function () {
                        }
                    });

                }).to.throw(/already a task with that name/);

            });
        });
    });

    describe('#run()', function () {

        describe('validation', function () {

            beforeEach(function () {

                resetRunner();
            });

            it('should throw if the includes are falsy', function () {

                expect(function () {

                    runner.run(null, {});

                }).to.throw(/includes are invalid/);
            });

            it('should throw if the includes are not an array', function () {

                expect(function () {

                    runner.run('includes', {});

                }).to.throw(/includes argument is not an array/);
            });

            it('should throw if the output is falsy', function () {

                expect(function () {

                    runner.run([], null);

                }).to.throw(/output is invalid/);
            });
        });

        beforeEach(function () {

            resetRunner();
            resetMocks();
        });

    });
});