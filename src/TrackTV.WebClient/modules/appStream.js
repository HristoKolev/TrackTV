'use strict';

function appStream(appBuilder, includes) {

    var that = Object.create(null);

    // modules
    var gulp = require('gulp'),
        concat = require('gulp-concat'),
        less = require('gulp-less'),
        fs = require('fs'),
        path = require('path'),
        browserify = require('browserify'),
        source = require('vinyl-source-stream'),
        buffer = require('vinyl-buffer'),
        glob = require('glob');

    // constants
    var constants = {
        thirdPartyJs: 'third-party.js',
        thirdPartyCss: 'third-party.css',
        mergedStyles: 'merged-styles.css',
        mergedScripts: 'merged-scripts.js',
        moduleHeaders: 'module-headers.js',
        moduleLibraries: 'module-libraries.js',
        moduleConstants: 'module-constants.js',
        browserifiedScripts: 'browserified.js'
    };

    // option object
    var browserifyOptions = {
        debug: true,
        entries: glob.sync(appBuilder.npmModuleFiles)
    };

    // methods

    ///////////////////////////////////////////////////////////////

    that.indexFileStream = function () {

        return gulp.src(appBuilder.indexFile);
    };

    that.thirdPartyScriptsStream = function () {

        return gulp.src(includes.scripts);
    };

    ///////////////////////////////////////////////////////////////

    that.libStylesStream = function () {

        return gulp.src(includes.styles)
            .pipe(concat(constants.thirdPartyCss));
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
            .pipe(concat(constants.mergedStyles))
            .pipe(less());
    };

    that.appScriptsStream = function () {

        return gulp.src(appBuilder.scripts)
            .pipe(concat(constants.mergedScripts));
    };

    that.allSourcesStream = function () {

        return gulp.src(appBuilder.sourceFiles.concat(appBuilder.npmModuleFiles));
    };

    that.moduleHeadersStream = function () {

        return gulp.src(appBuilder.moduleHeaders)
            .pipe(concat(constants.moduleHeaders));
    };

    that.moduleLibrariesStream = function () {

        return gulp.src(appBuilder.moduleLibraries)
            .pipe(concat(constants.moduleLibraries));
    };

    that.moduleConstantsStream = function () {

        return gulp.src(appBuilder.moduleConstants)
            .pipe(concat(constants.moduleConstants));
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
            .pipe(source(constants.browserifiedScripts))
            .pipe(buffer());
    };

    that.contentStream = function () {

        return gulp.src(appBuilder.contentPath, {
            base: path.basename(appBuilder.contentPath)

        });
    };

    that.basePath = appBuilder.basePath;
    return that;
}

module.exports = {
    instance: appStream
};