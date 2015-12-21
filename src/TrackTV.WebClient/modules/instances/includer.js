'use strict';

const includer = require('../../modules/includer');

const appBuilder = require('./appBuilder'),
    defaultOutput = require('./defaultOutput');

module.exports = includer.instance(appBuilder.indexFile, defaultOutput.value());