'use strict';

const expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest;

const structureValidator = require('../modules/structureValidator');

describe('#structureValidator', function () {

    describe('module exports', function () {

        assertCompositionMultitest.object('structureValidator', structureValidator, [
            ['validate', 'function']
        ]);
    });

    describe('#validate()', function () {

        const defaultAppRootPath = 'app',
            defaultPaths = ['app/file1'];

        describe('validation', function () {

            const defaultValidations = function () {
            };

            it('should throw if the paths argument is falsy', function () {

                expect(function () {

                    structureValidator.validate(null, defaultAppRootPath, defaultValidations);

                }).to.throw(/paths are invalid/);
            });

            it('should throw if the paths argument is not an array', function () {

                expect(function () {

                    structureValidator.validate('paths', defaultAppRootPath, defaultValidations);

                }).to.throw(/is not an array/);
            });

            it('should throw the paths array is empty', function () {

                expect(function () {

                    structureValidator.validate([], defaultAppRootPath, defaultValidations);

                }).to.throw(/array is empty/);
            });

            it('should throw if the app root path is falsy', function () {

                expect(function () {

                    structureValidator.validate(defaultPaths, null, defaultValidations);

                }).to.throw(/app root path is invalid/);
            });

            it('should throw if the validations argument is falsy', function () {

                expect(function () {

                    structureValidator.validate(defaultPaths, defaultAppRootPath, null);

                }).to.throw(/validations are invalid/);
            });

            it('should throw if the paths array contains a path that is not in the app directory', function () {

                expect(function () {

                    structureValidator.validate(['dir/file'], defaultAppRootPath, defaultValidations);

                }).to.throw(/is not in the app directory/);
            });
        });

        let defaultEvaluationName = 'evaluation';

        it('should call validations with the specified arguments', function () {

            let called = false;

            function validations(evaluate) {

                expect(evaluate).to.be.a('function');

                called = true;
            }

            structureValidator.validate(defaultPaths, defaultAppRootPath, validations);

            expect(called).to.be.true;
        });

        it('should throw if the evaluation name is falsy', function () {

            function validations(evaluate) {

                evaluate(null, function (paths) {

                });
            }

            expect(function () {

                structureValidator.validate(defaultPaths, defaultAppRootPath, validations);
            }).to.throw(/evaluation name is invalid/);
        });

        it('should throw if the evaluation function is falsy', function () {

            function validations(evaluate) {

                evaluate(defaultEvaluationName, null);
            }

            expect(function () {

                structureValidator.validate(defaultPaths, defaultAppRootPath, validations);
            }).to.throw(/evaluation function is invalid/);
        });

        it('should call the evaluation function passing the relative paths as an argument', function () {

            let appRootPath = 'app';

            let filePaths = [
                'app\\dir1\\file1',
                'app\\dir2\\file2'
            ];

            let expected = [
                'dir1\\file1',
                'dir2\\file2'
            ];

            let called = false;

            function validations(evaluate) {

                evaluate(defaultEvaluationName, function (paths) {

                    called = true;

                    expect(paths).to.deep.equal(expected);
                });
            }

            structureValidator.validate(filePaths, appRootPath, validations);

            expect(called).to.be.true;
        });

        it('should return valid report', function () {

            function validations(evaluate) {

                evaluate(defaultEvaluationName, function () {

                    return 'errorMessage1';
                });

                evaluate(defaultEvaluationName, function () {

                    return 'errorMessage2';
                });

                evaluate(defaultEvaluationName, function () {

                    // no error message returned
                });
            }

            let report = structureValidator.validate(defaultPaths, defaultAppRootPath, validations);

            let expected = [
                {
                    name: defaultEvaluationName,
                    message: 'errorMessage1'
                },
                {
                    name: defaultEvaluationName,
                    message: 'errorMessage2'
                }
            ];

            expect(report).to.deep.equal(expected);
        });
    });
});