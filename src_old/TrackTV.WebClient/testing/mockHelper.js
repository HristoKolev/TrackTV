'use strict';

const sinon = require('sinon'),
    mockery = require('mockery'),
    _ = require('underscore');

const isFunction = require('../modules/isFunction');

function parseSettings(settings) {

    if (!settings) {

        throw new Error('The settings object is invalid');
    }

    if (Array.isArray(settings)) {

        settings = {
            flags: settings
        };
    }

    if (!settings.flags) {

        settings.flags = [];
    }

    let flags = {
        stub: settings.flags.indexOf('stub') !== -1,
        spy: settings.flags.indexOf('spy') !== -1,
        stubObject: settings.flags.indexOf('stubObject') !== -1
    };

    const result = {};

    function attachFlags(obj) {

        if (!obj) {

            obj = flags;
        }

        for (let index in obj) { // jshint ignore:line

            //noinspection JSUnfilteredForInLoop
            result[index] = obj[index];
        }
    }

    if (settings.returnUndefined) {

        result.shouldReturn = true;
        result.returnValue = undefined;
    }
    else if (settings.returnValue !== undefined) {

        result.shouldReturn = true;
        result.returnValue = settings.returnValue;

        if (isFunction(result.returnValue)) {

            attachFlags({
                spy: flags.spy
            });
        }
    }
    else if (flags.stubObject) {

        attachFlags({
            stubObject: flags.stubObject
        });
    }
    else {

        attachFlags();
    }

    return result;
}

function forwardFunction(func) {

    return function (...args) {

        if (func) {

            return func(...args);
        }
    };
}

function mockRequire(moduleName, useCleanCache) {

    mockery.registerAllowable(moduleName);

    mockery.enable({useCleanCache: !!useCleanCache});

    let module = require(moduleName);

    mockery.disable();

    return module;
}

function getMock(moduleName, optionsObj, originalModule) {

    let keys = Object.keys(optionsObj);

    let result = {};
    let mock = {};

    let substitutes = [];

    for (let memberName of keys) {

        let settings = parseSettings(optionsObj[memberName]);

        result[memberName] = {};

        if (settings.stubObject) {

            mock[memberName] = undefined;

            result[memberName].setValue = function (value) {

                mock[memberName] = value;
            };
        } else if (settings.stub) {

            let stub = sinon.stub();
            substitutes.push(stub);

            mock[memberName] = forwardFunction(stub);

            result[memberName].stub = stub;
        }
        else if (settings.shouldReturn) {

            mock[memberName] = settings.returnValue;
        }
        else if (originalModule) {

            mock[memberName] = forwardFunction(originalModule[memberName]);
        }
        else {

            mock[memberName] = forwardFunction();
        }

        if (settings.spy) {

            let spy = sinon.spy(mock, memberName);
            substitutes.push(spy);

            result[memberName].spy = spy;
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

    result.deregister = () => mockery.deregisterMock(moduleName);

    result.mock = mock;

    return result;
}

function fromFunction(moduleName, optionsArray) {

    const homeObject = {};
    const result = {};

    const substitutes = [];

    const options = parseSettings(optionsArray);

    if (isFunction(options)) {

        homeObject.func = options;
    }
    else {

        if (options.stub) {

            let stub = sinon.stub();

            homeObject.func = forwardFunction(stub);

            result.stub = stub;

            substitutes.push(stub);
        }
        else if (options.shouldReturn) {

            homeObject.func = options.returnValue;
        }
        else {

            homeObject.func = forwardFunction();
        }

        if (options.spy) {

            let spy = sinon.spy(homeObject, 'func');

            result.spy = spy;

            substitutes.push(spy);
        }
    }

    if (moduleName !== null) {

        mockery.registerMock(moduleName, homeObject.func);
    }

    result.resetMocks = function () {

        for (let substitute of substitutes) {

            substitute.reset();
        }
    };

    result.deregister = () => mockery.deregisterMock(moduleName);

    result.mock = homeObject.func;

    return result;
}

module.exports = getMock;
module.exports.require = mockRequire;
module.exports.mockFunction = fromFunction;