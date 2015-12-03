'use strict';

const sinon = require('sinon'),
    mockery = require('mockery');

function parseSettings(settings) {

    return {
        shouldStub: settings.indexOf('stub') !== -1,
        shouldSpy: settings.indexOf('spy') !== -1
    };
}

function forwardFunction(func) {

    if (func) {

        return function () {

            return func.apply(this, arguments);
        };
    }
    else {

        return function () {
        };
    }
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

            if (originalModule) {

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

    mockery.registerMock(moduleName, mock);

    result.resetMocks = function () {

        for (let substitute of substitutes) {

            substitute.reset();
        }
    };

    return result;
}

function mockRequire(moduleName) {

    mockery.registerAllowable(moduleName);

    mockery.enable();

    let module = require(moduleName);

    mockery.disable();

    return module;
}

module.exports = getMock;
module.exports.require = mockRequire;