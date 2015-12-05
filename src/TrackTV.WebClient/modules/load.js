'use strict';

const gulp = require('gulp');

const tasks = require('./tasks'),
    runner = require('./runner'),
    fixGulp = require('./fix-gulp');

fixGulp(gulp);
tasks.load(runner);

const buildSystem = require('./instances/buildSystem');

buildSystem.registerTasks();