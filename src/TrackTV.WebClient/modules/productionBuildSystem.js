'use strict';

function productionBuildSystem(appBuilder, buildSystem, pathResolver) {

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
        saveFile = require('gulp-savefile');

    // custom modules 
    var embedMedia = require('./gulp-embed-media'),
        fillContent = require('./fill-content'),
        jsonExpose = require('./json-expose');

    // option object
    var cssMinifyOptions = {
        processImportFrom: ['local'],
        keepSpecialComments: 0
    };

    var htmlMinifyOptions = {
        empty: true,
    };

    var embedMediaOptions = {
        baseDir: pathResolver.publicPath(),
        resourcePattern: [
            '/include/*'
        ],
        verbose: true
    };

    // paths
    var buildPath = pathResolver.publicPath('/build'),
        contentPath = buildPath + '/content',
        buildHtml = buildPath + '/index.html',
        tempBuild = buildPath + '/temp',
        tempMerged = tempBuild + '/merged',
        tempLib = tempBuild + '/lib',
        tempLibCss = tempLib + '/styles',
        libScriptsPath = tempLib + '/scripts';

    var sourceIndex = pathResolver.publicPath('/index.html'),
        sourceContent = pathResolver.publicPath('/content/*');

    function createFile(name, contents) {

        return file(name, contents, { src: true });
    }

    that.registerTasks = function () {

        gulp.task('build-clean', function (callback) {

            del.sync(buildPath);

            callback();
        });

        gulp.task('build-index', function () {

            return gulp.src(sourceIndex)
                .pipe(gulp.dest(buildPath));
        });

        gulp.task('build-scripts', function () {

            return buildSystem.libScriptsStream()
                .pipe(uglify())
                .pipe(gulp.dest(libScriptsPath));
        });

        gulp.task('build-module-headers', function () {

            return buildSystem.moduleHeadersStream()
                .pipe(uglify())
                .pipe(gulp.dest(tempMerged));
        });

        gulp.task('build-source', function () {

            return buildSystem.appScriptsStream()
                .pipe(uglify())
                .pipe(gulp.dest(tempMerged));;
        });

        gulp.task('build-styles', function () {

            return buildSystem.libStylesStream()
                .pipe(minifyCss(cssMinifyOptions))
                .pipe(replace('../fonts/', './content/'))
                .pipe(gulp.dest(tempLibCss));
        });

        gulp.task('build-fonts', function () {

            return buildSystem.libFontsStream()
                .pipe(gulp.dest(contentPath));
        });

        gulp.task('build-less', function () {

            return buildSystem.appStylesStream()
                .pipe(minifyCss(cssMinifyOptions))
                .pipe(gulp.dest(tempMerged));
        });

        gulp.task('build-browserify', function () {

            return buildSystem.browserifyStream()
                .pipe(uglify())
                .pipe(gulp.dest(libScriptsPath));
        });

        gulp.task('build-copy-content', function () {

            return gulp.src(sourceContent)
                .pipe(gulp.dest(contentPath));
        });

        gulp.task('build-templates', function () {

            function wrapTemplate(name, contents) {

                return '<script type="text/ng-template" id="' + name + '">\n' + contents + '\n</script>';
            }

            var templatesName = 'templates';

            return buildSystem.allTemplatesStream()
                .pipe(embedMedia(embedMediaOptions))
                .pipe(insert.transform(function (contents, file) {

                    return wrapTemplate(file.path.split('\\').pop(), contents);
                }))
                .pipe(concat(templatesName))
                .pipe(fillContent(buildHtml, templatesName));
        });

        gulp.task('build-settings', function () {

            var globalPropertyName = 'settings';
            var commentPlaceholder = 'settings';

            var configFiles = appBuilder.appPath('/*.json');

            var content = jsonExpose(globalPropertyName, configFiles);

            return createFile('settings', content)
                .pipe(fillContent(buildHtml, commentPlaceholder));
        });

        gulp.task('build-merge', function () {

            return gulp.src(buildHtml)
                .pipe(embedMedia({
                    baseDir: pathResolver.publicPath(),
                    verbose: true,
                    selectors: 'head link[rel="icon"]',
                    attributes: 'href'
                }))
                .pipe(smoosher({
                    base: tempBuild
                }))
                .pipe(minifyHtml(htmlMinifyOptions))
                .pipe(saveFile());
        });

        gulp.task('build-clear', function (callback) {

            del.sync(tempBuild);

            callback();
        });
    };

    return that;
}

module.exports = {
    instance: productionBuildSystem
};