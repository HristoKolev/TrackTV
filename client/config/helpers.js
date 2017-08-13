"use strict";

const fs = require('fs');
const path = require('path');

const HOST = '127.0.0.1';
const DEV_PORT = 3000;
const PROD_PORT = 8088;

const CONSTANTS = (function () {

    const EVENT = process.env.npm_lifecycle_event || '';

    const PROD = EVENT.includes('prod');

    return {
        AOT: EVENT.includes('aot'),
        ENV: PROD ? JSON.stringify('production') : JSON.stringify('development'),
        HOST: JSON.stringify(HOST),
        PORT: PROD ? PROD_PORT : DEV_PORT,
        DEV_SERVER: EVENT.includes('webdev'),
        DLL: EVENT.includes('dll'),
        WATCH: process.argv.join('').indexOf('watch') > -1,
        PROD: PROD,
    };
}());

function isFunction(functionToCheck) {

    return functionToCheck && {}.toString.call(functionToCheck) === '[object Function]';
}

const ifConst = (func, trueVal, falseVal = undefined) => {

    if (func(CONSTANTS)) {

        if (isFunction(trueVal)) {

            return trueVal();
        }

        return trueVal;
    } else {

        if (isFunction(falseVal)) {

            return falseVal();
        }

        return falseVal;
    }
};

const root = (...args) => path.join(path.resolve(__dirname, '../'), ...args);

function testDll() {

    if (!fs.existsSync(root('./dll/polyfill.dll.js')) || !fs.existsSync(root('./dll/vendor.dll.js'))) {
        throw "DLL files do not exist, please use 'npm run build:dll' once to generate dll files.";
    }

}

exports.HOST = HOST;
exports.DEV_PORT = DEV_PORT;
exports.PROD_PORT = PROD_PORT;
exports.CONSTANTS = CONSTANTS;
exports.ifConst = ifConst;
exports.root = root;
exports.testDll = testDll;
