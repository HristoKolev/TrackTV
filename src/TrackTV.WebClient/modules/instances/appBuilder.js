'use strict';

const appBuilder = require('../appBuilder');

const appConfig = require('../../config/appConfig.json');

module.exports = appBuilder.instance(appConfig.appPath);