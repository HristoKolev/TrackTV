'use strict';

const buildSystem = require('../../modules/devBuildSystem'),
    runner = require('../../modules/runner');

const appBuilder = require('./appBuilder'),
    defaultOutput = require('./defaultOutput'),
    includer = require('./includer'),
    bowerComponents = require('./bowerComponents');

module.exports = buildSystem.instance(appBuilder, defaultOutput, includer, bowerComponents, runner);