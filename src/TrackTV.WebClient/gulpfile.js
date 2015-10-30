'use strict';

var gulp = require('gulp'),
    del = require('del'),
    concat = require('gulp-concat'),
    less = require('gulp-less'),
    smoosher = require('gulp-smoosher'),
    replace = require('gulp-replace'),
    uglify = require('gulp-uglify'),
    minifyCss = require('gulp-minify-css'),
    insert = require('gulp-insert'),
    runSequence = require('run-sequence'),
    minifyHtml = require('gulp-minify-html'),
    file = require('gulp-file'),
    fs = require('fs'),
    saveFile = require('gulp-savefile'),
    jslint = require('gulp-jslint-simple'),
    jshint = require('gulp-jshint'),
    stylish = require('jshint-stylish'),
    browserify = require('browserify'),
    source = require('vinyl-source-stream'),
    buffer = require('vinyl-buffer');

// config files
var appConfig = require('./config/appConfig.json'),
    pathConfig = require('./config/path.json'),
    templateConfig = require('./' + appConfig.appRoot + '/templateConfig.json');

// custom modules
var embedMedia = require('./modules/gulp-embed-media'),
    pathResolve = require('./modules/pathResolver').instance(pathConfig),
    appBuilder = require('./modules/appBuilder').instance(pathResolve, appConfig.appRoot),
    fixGulp = require('./modules/fix-gulp'),
    fillContent = require('./modules/fill-content'),
    jsonExpose = require('./modules/json-expose'),
    bowerComponents = require('./modules/bowerComponents').instance(pathResolve, require('./config/bowerIncludes.json'));

// source files

var appScripts = appBuilder.scripts(),
    appLessFiles = appBuilder.lessFiles(),
    appTemplates = appBuilder.templates(),
    npmModuleFiles = appBuilder.npmModuleFiles(),
    moduleHeaders = appBuilder.moduleHeaders();

// constants

var thirdPartyJs = 'third-party.js',
    thirdPartyCss = 'third-party.css',
    mergedStyles = 'merged-styles.css',
    mergedScripts = 'merged-scripts.js',
    browserifiedScripts = 'browserified.js',
    headerScripts = 'module-headers.js';

// third party configs
var buildPath = pathResolve.publicPath('/build'),
    contentPath = buildPath + '/content',
    tempBuild = buildPath + '/temp',
    buildHtml = buildPath + '/index.html',
    tempMerged = tempBuild + '/merged',

    libPath = pathResolve.publicPath('/lib'),
    mergedPath = pathResolve.publicPath('/merged');

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

var cssMinifyOptions = {
    processImportFrom: ['local'],
    keepSpecialComments: 0
};

var htmlMinifyOptions = {
    empty: true,
};

var embedMediaOptions = {
    baseDir: pathResolve.publicPath(),
    resourcePattern: [
        '/include/*'
    ],
    verbose: true
};

var browserifyOptions = {
    debug: true,
    entries: npmModuleFiles.filter(function (file) {

        return fileExists(file);
    })
};

function createFile(name, contents) {

    return file(name, contents, { src: true });
}

fixGulp(gulp);

 

var buildSystem = require('./modules/buildSystem')
    .instance(appBuilder, bowerComponents, pathResolve);

// dev tasks

var devBuildSystem = require('./modules/devBuildSystem')
    .instance(appBuilder, bowerComponents, pathResolve);

devBuildSystem.registerTasks();

gulp.task('default', function () {

    runSequence(
        'dev-clean',
        'dev-styles',
        //'dev-fonts',
        'dev-scripts',
        'dev-browserify',
        'dev-templates',
        'dev-less',
        'dev-module-headers',
        'dev-merge'
    );
});
// build tasks

gulp.task('build-clean', function (callback) {

    del.sync(buildPath);

    callback();
});

gulp.task('build-index', function () {

    return gulp.src(pathResolve.publicPath('/index.html'))
        .pipe(gulp.dest(buildPath));
});

gulp.task('build-scripts', function () {

    return buildSystem.libScriptsStream()
        .pipe(uglify())
        .pipe(gulp.dest(tempBuild + '/lib'));
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
        .pipe(gulp.dest(tempBuild + '/lib/css'));
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
        .pipe(gulp.dest(tempBuild + '/lib'));
});

gulp.task('build-copy-content', function () {

    return gulp.src(pathResolve.publicPath('/content/*'))
        .pipe(gulp.dest(contentPath));
});

gulp.task('build-templates', function () {

    function wrapTemplate(name, contents) {

        return '<script type="text/ng-template" id="' + name + '">\n' + contents + '\n</script>';
    }

    return buildSystem.allTemplatesStream()
        .pipe(embedMedia(embedMediaOptions))
        .pipe(insert.transform(function (contents, file) {

            return wrapTemplate(file.path.split('\\').pop(), contents);
        }))
        .pipe(concat('templates'))
        .pipe(fillContent(buildHtml, 'templates'));
});

gulp.task('build-settings', function () {

    return createFile('settings.html', jsonExpose('settings', appConfig.appRoot + '/*.json'))
        .pipe(fillContent(buildHtml, 'settings'));
});

gulp.task('build-merge', function () {

    return gulp.src(buildHtml)
        .pipe(embedMedia({
            baseDir: pathResolve.publicPath(),
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

gulp.task('build', function () {

    runSequence(
        'build-clean',
        'build-index',
        'build-scripts',
        'build-browserify',
        'build-source',
        'build-module-headers',
        'build-styles',
        //'build-fonts',
        'build-less',
        'build-copy-content',
        'build-templates',
        'build-settings',
        'build-merge',
        'build-clear'
    );
});