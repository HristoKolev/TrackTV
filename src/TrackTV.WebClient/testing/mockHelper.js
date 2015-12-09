'use strict';

const sinon = require('sinon'),
    mockery = require('mockery'),
    _ = require('underscore');

function isFunction(functionToCheck) {

    return functionToCheck && {}.toString.call(functionToCheck) === '[object Function]';
}

function parseSettings(settings) {

    return {
        shouldStub: settings.indexOf('stub') !== -1,
        shouldSpy: settings.indexOf('spy') !== -1
    };
}

function forwardFunction(func) {

    return function (...args) {

        if (func) {

            return func(...args);
        }
    };
}

function mockRequire(moduleName) {

    mockery.registerAllowable(moduleName);

    mockery.enable();

    let module = require(moduleName);

    mockery.disable();

    return module;
}

function getMock(moduleName, optionsObj, originalModule) {

    let functionNames = Object.keys(optionsObj);

    let result = {};
    let mock = {};

    let substitutes = [];

    for (let funcName of functionNames) {

        let settings = parseSettings(optionsObj[funcName]);

        result[funcName] = {};

        if (settings.shouldStub) {

            let stub = sinon.stub();
            substitutes.push(stub);

            mock[funcName] = forwardFunction(stub);

            result[funcName].stub = stub;
        }
        else {

            let mockFunc = _.find(optionsObj[funcName], e => isFunction(e));

            if (mockFunc) {

                mock[funcName] = mockFunc;

            } else if (originalModule) {

                let fallthrough = originalModule[funcName];

                mock[funcName] = forwardFunction(fallthrough);
            }
            else {

                mock[funcName] = forwardFunction();
            }
        }

        if (settings.shouldSpy) {

            let spy = sinon.spy(mock, funcName);
            substitutes.push(spy);

            result[funcName].spy = spy;
        }
    }

    if (moduleName !== null) {

        mockery.registerMock(moduleName, mock);
    }

    result.resetMocks = function () {

        for (let substitute of substitutes) {

            substitute.reset();
        }
    };

    result.deregister = function () {

        mockery.deregisterMock(moduleName);
    };

    result.mock = mock;

    return result;
}

function fromFunction(moduleName, optionsArray, originalFunction) {

    const homeObject = {};
    const result = {};

    const substitutes = [];

    const options = parseSettings(optionsArray);

    if (options.shouldStub) {

        let stub = sinon.stub();

        homeObject.func = forwardFunction(stub);

        result.stub = stub;

        substitutes.push(stub);
    }
    else {

        let mockFunc = _.find(optionsArray, e => isFunction(e));

        if (mockFunc) {

            homeObject.func = mockFunc;

        } else if (originalFunction) {

            homeObject.func = forwardFunction(originalFunction);
        }
        else {

            homeObject.func = forwardFunction();
        }
    }

    if (options.shouldSpy) {

        let spy = sinon.spy(homeObject, 'func');

        result.spy = spy;

        substitutes.push(spy);
    }

    if (moduleName !== null) {

        mockery.registerMock(moduleName, homeObject.func);
    }

    result.resetMocks = function () {

        for (let substitute of substitutes) {

            substitute.reset();
        }
    };

    result.deregister = function () {

        mockery.deregisterMock(moduleName);
    };

    result.mock = homeObject.func;

    return result;
}

module.exports = getMock;
module.exports.require = mockRequire;
module.exports.mockFunction = fromFunction;