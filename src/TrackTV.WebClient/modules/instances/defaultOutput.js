'use strict';

const output = require('../../modules/pathChain');

const outputConfig = require('../../config/outputConfig.json');

module.exports = output.instance(outputConfig.devPath);