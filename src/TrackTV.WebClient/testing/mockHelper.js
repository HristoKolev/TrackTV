'use strict';

const sinon = require('sinon'),
    mockery = require('mockery');

function parseSettings(settings) {

    return {
        shouldStub: settings.indexOf('stub') !== -1,
        shouldSpy: settings.indexOf('spy') !== -1
    };
}

function getMock(moduleName, optionsObj) {

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

            mock[funcName] = function () {

                return stub.apply(this, arguments);
            };

            result[funcName].stub = stub;

        }
        else {

            mock[funcName] = function () {
            };
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