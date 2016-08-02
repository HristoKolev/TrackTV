'use strict';

const bowerComponents = require('../../modules/bowerComponents');

const includes = require('../../config/bowerIncludes.json'),
    pathConfig = require('../../config/path.json');

module.exports = bowerComponents.instance(includes, pathConfig.bowerRootPath);