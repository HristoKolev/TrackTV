'use strict';

function productionBuildSystem(output, appBuilder, appStream, pathResolver) {

    var that = Object.create(null);

    var gulp = require('gulp'),
        del = require('del'),
        uglify = require('gulp-uglify'),
        minifyCss = require('gulp-minify-css'),
        insert = require('gulp-insert'),
        concat = require('gulp-concat'),
        replace = require('gulp-replace'),
        file = require('gulp-file'),
        smoosher = require('gulp-smoosher'),
        minifyHtml = require('gulp-minify-html'),
        saveFile = require('gulp-savefile'),
        wrench = require('wrench');

    // custom modules 
    var embedMedia = require('./plugins/gulp-embed-media'),
        fillContent = require('./fill-content'),
        jsonExpose = require('./json-expose');

    // option object
    var cssMinifyOptions = {
        processImportFrom: ['local'],
        keepSpecialComments: 0
    };

    var htmlMinifyOptions = {
        empty: true
    };

    var embedMediaOptions = {
        baseDir: appBuilder.contentPath,
        resourcePattern: [
            '/include/*'
        ],
        verbose: false
    };

    // paths

    var contentPath = output('/content'),
        buildHtml = output('/index.html'),
        tempBuild = output('/temp'),
        tempMerged = tempBuild('/merged'),
        tempLib = tempBuild('/lib'),
        tempLibCss = tempLib('/styles'),
        libScriptsPath = tempLib('/scripts');

    function createFile(name, contents) {

        return file(name, contents, { src: true });
    }

    that.registerTasks = function () {

        gulp.task('build-clean', function (callback) {

            del.sync(output.value());

            callback();
        });

        gulp.task('build-index', function () {

            return appStream.indexFileStream()
                .pipe(output.destStream());
        });

        gulp.task('build-scripts', function () {

            return appStream.libScriptsStream()
                .pipe(uglify())
                .pipe(libScriptsPath.destStream());
        });

        gulp.task('build-module-headers', function () {

            return appStream.moduleHeadersStream()
                .pipe(uglify())
                .pipe(tempMerged.destStream());
        });

        gulp.task('build-module-libraries', function () {

            return appStream.moduleLibrariesStream()
                .pipe(uglify())
                .pipe(tempMerged.destStream());
        });

        gulp.task('build-module-constants', function () {

            return appStream.moduleConstantsStream()
                .pipe(uglify())
                .pipe(tempMerged.destStream());
        });

        gulp.task('build-copy-initFile', function () {

            return appStream.initFileStream()
                .pipe(uglify())
                .pipe(tempMerged.destStream());
        });

        gulp.task('build-copy-routeConfig', function () {

            return appStream.routeConfigStream()
                .pipe(uglify())
                .pipe(tempMerged.destStream());
        });

        gulp.task('build-source', function () {

            return appStream.appScriptsStream()
                .pipe(uglify())
                .pipe(tempMerged.destStream());
        });

        gulp.task('build-styles', function () {

            return appStream.libStylesStream()
                .pipe(minifyCss(cssMinifyOptions))
                .pipe(replace('../fonts/', './content/'))
                .pipe(tempLibCss.destStream());
        });

        gulp.task('build-fonts', function () {

            return appStream.libFontsStream()
                .pipe(contentPath.destStream());
        });

        gulp.task('build-less', function () {

            return appStream.appStylesStream()
                .pipe(minifyCss(cssMinifyOptions))
                .pipe(tempMerged.destStream());
        });

        gulp.task('build-browserify', function () {

            return appStream.browserifyStream()
                .pipe(uglify())
                .pipe(libScriptsPath.destStream());
        });

        gulp.task('build-copy-content', function (callback) {

            wrench.copyDirSyncRecursive(appBuilder.contentPath, contentPath.value());

            return callback();
        });

        gulp.task('build-templates', function () {

            function wrapTemplate(name, contents) {

                return '<script type="text/ng-template" id="' + name + '">\n' + contents + '\n</script>';
            }

            var templatesName = 'templates';

            return appStream.allTemplatesStream()
                .pipe(embedMedia(embedMediaOptions))
                .pipe(insert.transform(function (contents, file) {

                    return wrapTemplate(file.path.split('\\').pop(), contents);
                }))
                .pipe(concat(templatesName))
                .pipe(fillContent(buildHtml.value(), templatesName));
        });

        gulp.task('build-settings', function () {

            var globalPropertyName = 'settings';
            var commentPlaceholder = 'settings';

            var content = jsonExpose(globalPropertyName, appBuilder.configFiles);

            return createFile('settings', content)
                .pipe(fillContent(buildHtml.value(), commentPlaceholder));
        });

        gulp.task('build-merge', function () {

            return gulp.src(buildHtml.value())
                .pipe(embedMedia({
                    baseDir: appBuilder.contentPath,
                    verbose: false,
                    selectors: 'head link[rel="icon"]',
                    attributes: 'href'
                }))
                .pipe(smoosher({
                    base: tempBuild.value()
                }))
                .pipe(minifyHtml(htmlMinifyOptions))
                .pipe(saveFile());
        });

        gulp.task('build-clear', function (callback) {

            del.sync(tempBuild.value());

            callback();
        });
    };

    return that;
}

module.exports = {
    instance: productionBuildSystem
};