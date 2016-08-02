'use strict';

module.exports = function (functionToCheck) {

    return functionToCheck && {}.toString.call(functionToCheck) === '[object Function]';
};