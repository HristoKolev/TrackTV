'use strict';

function buildSystem(appBuilder, includes) {

    var that = Object.create(null);

    // modules
    var gulp = require('gulp'),
        concat = require('gulp-concat'),
        less = require('gulp-less'),
        fs = require('fs'),
        browserify = require('browserify'),
        source = require('vinyl-source-stream'),
        buffer = require('vinyl-buffer');

    // constants
    var thirdPartyJs = 'third-party.js',
        thirdPartyCss = 'third-party.css',
        mergedStyles = 'merged-styles.css',
        mergedScripts = 'merged-scripts.js',
        moduleHeaders = 'module-headers.js',
        moduleLibraries = 'module-libraries.js',
        moduleConstants = 'module-constants.js',
        browserifiedScripts = 'browserified.js';

    // option object
    var browserifyOptions = {
        debug: true,
        entries: appBuilder.existingNpmModueFiles
    };

    // methods

    that.libScriptsStream = function () {

        return gulp.src(includes.scripts)
            .pipe(concat(thirdPartyJs));
    };

    that.libStylesStream = function () {

        return gulp.src(includes.styles)
            .pipe(concat(thirdPartyCss));
    };

    that.libFontsStream = function () {

        return gulp.src(includes.fonts);
    };

    that.libTemplatesStream = function () {

        return gulp.src(includes.templates);
    };

    that.allTemplatesStream = function () {

        return gulp.src(includes.templates.concat(appBuilder.templates));
    };

    that.appStylesStream = function () {

        return gulp.src(appBuilder.lessFiles)
            .pipe(concat(mergedStyles))
            .pipe(less());
    };

    that.appScriptsStream = function () {

        return gulp.src(appBuilder.scripts)
            .pipe(concat(mergedScripts));
    };

    that.allSourcesStream = function () {

        return gulp.src(appBuilder.sourceFiles.concat(appBuilder.npmModuleFiles));
    };

    that.moduleHeadersStream = function () {

        return gulp.src(appBuilder.moduleHeaders)
            .pipe(concat(moduleHeaders));
    };

    that.moduleLibrariesStream = function () {

        return gulp.src(appBuilder.moduleLibraries)
            .pipe(concat(moduleLibraries));
    };

    that.moduleConstantsStream = function () {

        return gulp.src(appBuilder.moduleConstants)
            .pipe(concat(moduleConstants));
    };

    that.initFileStream = function () {

        return gulp.src(appBuilder.initFile);
    };

    that.routeConfigStream = function () {

        return gulp.src(appBuilder.routeConfig);
    };

    that.browserifyStream = function () {

        return browserify(browserifyOptions)
            .bundle()
            .pipe(source(browserifiedScripts))
            .pipe(buffer());
    };

    return that;
}

module.exports = {
    instance: buildSystem
};