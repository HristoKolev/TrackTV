'use strict';

function buildSystem(appBuilder, includes, pathResolver) {

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
        headerScripts = 'module-headers.js',
        browserifiedScripts = 'browserified.js';

    // variables
    var bowerScripts = pathResolver.bowerComponent(includes.bowerScripts),
        bowerStyles = pathResolver.bowerComponent(includes.bowerStyles),
        bowerFonts = pathResolver.bowerComponent(includes.bowerFonts),
        bowerTemplates = pathResolver.bowerComponent(includes.bowerTemplates);

    var browserifyOptions = {
        debug: true,
        entries: appBuilder.npmModuleFiles().filter(function (file) {

            return fileExists(file);
        })
    };

    function fileExists(filePath) {

        try {
            fs.statSync(filePath);
        } catch (err) {

            if (err.code == 'ENOENT') {
                return false;
            } else {
                throw err;
            }
        }

        return true;
    }

    var that = Object.create(null);

    that.libScriptsStream = function () {

        return gulp.src(bowerScripts)
            .pipe(concat(thirdPartyJs));
    };

    that.libStylesStream = function () {
        return gulp.src(bowerStyles)
            .pipe(concat(thirdPartyCss));
    };

    that.libFontsStream = function () {

        return gulp.src(bowerFonts);
    };

    that.libTemplatesStream = function () {

        return gulp.src(bowerTemplates);
    };

    that.allTemplatesStream = function () {

        return gulp.src(bowerTemplates.concat(appBuilder.templates()));
    };

    that.appStylesStream = function () {

        return gulp.src(appBuilder.lessFiles())
            .pipe(concat(mergedStyles))
            .pipe(less());
    };

    that.appScriptsStream = function () {

        return gulp.src(appBuilder.scripts())
            .pipe(concat(mergedScripts));
    };

    that.allSourcesStream = function () {

        return gulp.src(appBuilder.scripts()
            .concat(appBuilder.npmModuleFiles())
            .concat(appBuilder.moduleHeaders()));
    };

    that.moduleHeadersStream = function () {

        return gulp.src(appBuilder.moduleHeaders())
            .pipe(concat(headerScripts));
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
    instance: function (appBuilder, includes, pathResolver) {

        return buildSystem(appBuilder, includes, pathResolver);
    }
};