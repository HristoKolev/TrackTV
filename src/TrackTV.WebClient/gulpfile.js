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
    includes = require('./config/includes.json'),
    templateConfig = require('./' + appConfig.appRoot + '/templateConfig.json');

// custom modules
var embedMedia = require('./modules/gulp-embed-media'),
    pathResolve = require('./modules/pathResolver').instance(pathConfig),
    appBuilder = require('./modules/appBuilder').instance(pathResolve, appConfig.appRoot),
    fixGulp = require('./modules/fix-gulp'),
    fillContent = require('./modules/fill-content'),
    jsonExpose = require('./modules/json-expose');

// source files

var appScripts = appBuilder.scripts(),
    appLessFiles = appBuilder.lessFiles(),
    appTemplates = appBuilder.templates(),
    bowerScripts = pathResolve.bowerComponent(includes.bowerScripts),
    bowerStyles = pathResolve.bowerComponent(includes.bowerStyles),
    bowerFonts = pathResolve.bowerComponent(includes.bowerFonts),
    bowerTemplates = pathResolve.bowerComponent(includes.bowerTemplates),
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
    processImportFrom : ['local'],
    keepSpecialComments : 0
};

var htmlMinifyOptions = {
    empty : true,
};

var embedMediaOptions = {
    baseDir : pathResolve.publicPath(),
    resourcePattern : [
        '/include/*'
    ],
    verbose : true
};

var browserifyOptions = {
    debug : true,
    entries : npmModuleFiles.filter(function (file) {

        return fileExists(file);
    })
};

function createFile(name, contents) {

    return file(name, contents, { src : true });
}

fixGulp(gulp);

// dev tasks

gulp.task('clean', function (callback) {

    del(pathResolve.publicPath([
        libPath,
        mergedPath
    ]));

    callback();
});

gulp.task('scripts', function () {

    return gulp.src(bowerScripts)
        .pipe(concat(thirdPartyJs))
        .pipe(gulp.dest(libPath));

});

gulp.task('templates', function () {

    return gulp.src(bowerTemplates)
        .pipe(gulp.dest(pathResolve.publicPath('/lib/templates')));
});

gulp.task('styles', function () {

    return gulp.src(bowerStyles)
        .pipe(concat(thirdPartyCss))
        .pipe(gulp.dest(pathResolve.publicPath('/lib/css')));
});

gulp.task('fonts', function () {

    var fontsPath = pathResolve.publicPath('/lib/fonts');

    return gulp.src(bowerFonts)
        .pipe(gulp.dest(fontsPath));
});

gulp.task('less', function () {

    return gulp.src(appLessFiles)
        .pipe(concat(mergedStyles))
        .pipe(less())
        .pipe(gulp.dest(mergedPath))
        .on('error', console.error);
});

gulp.task('merge', function () {

    return gulp.src(appScripts)
        .pipe(concat(mergedScripts))
        .pipe(gulp.dest(mergedPath));
});

gulp.task('lint', function () {

    var filesForLinting = appScripts.concat(npmModuleFiles).concat(moduleHeaders);

    var jsLintFlagComment = '/*global $, angular, window */\n';

    gulp.src(filesForLinting)
        .pipe(insert.transform(function (contents, file) {

            return jsLintFlagComment + contents;
        }))
        .pipe(jslint.run())
        .pipe(jslint.report(stylish))
        .on('error', console.error);

    gulp.src(filesForLinting)
        .pipe(jshint('.jshintrc'))
        .pipe(jshint.reporter(stylish))
        .on('error', console.error);
});

gulp.task('module-headers', function () {

    return gulp.src(moduleHeaders)
        .pipe(concat(headerScripts))
        .pipe(gulp.dest(mergedPath));
});

gulp.task('browserify', function () {

    return browserify(browserifyOptions)
        .bundle()
        .pipe(source(browserifiedScripts))
        .pipe(buffer())
        .pipe(gulp.dest(pathResolve.publicPath('/lib')));

});

gulp.task('default', function () {

    runSequence(
        'clean',
        'styles',
        //'fonts',
        'scripts',
        'browserify',
        'templates',
        'less',
        'module-headers',
        'merge'
    );
});

gulp.task('watch', function () {

    //less files
    gulp.watch(appLessFiles, ['less']);
    console.log('Watching: ' + appLessFiles);

    //angular app
    var sourceFiles = appScripts.concat(moduleHeaders);

    gulp.watch(sourceFiles, ['merge', 'module-headers', 'lint']);
    console.log('Watching: ' + sourceFiles);

    //browserify
    gulp.watch(npmModuleFiles, ['browserify']);
    console.log('Watching: ' + npmModuleFiles);

    //configuration files
    var buildSystemConfigs = './config/*.json';

    gulp.watch(buildSystemConfigs, ['default']);
    console.log('Watching: ' + buildSystemConfigs);

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

    return gulp.src(bowerScripts)
        .pipe(concat(thirdPartyJs))
        .pipe(uglify())
        .pipe(gulp.dest(tempBuild + '/lib'));
});

gulp.task('build-module-headers', function () {

    return gulp.src(moduleHeaders)
        .pipe(concat(headerScripts))
        .pipe(uglify())
        .pipe(gulp.dest(tempMerged));
});

gulp.task('build-source', function () {

    return gulp.src(appScripts)
        .pipe(concat(mergedScripts))
        .pipe(uglify())
        .pipe(gulp.dest(tempMerged));;
});

gulp.task('build-styles', function () {

    return gulp.src(bowerStyles)
        .pipe(concat(thirdPartyCss))
        .pipe(minifyCss(cssMinifyOptions))
        .pipe(replace('../fonts/', './content/'))
        .pipe(gulp.dest(tempBuild + '/lib/css'));
});

gulp.task('build-fonts', function () {

    return gulp.src(bowerFonts)
        .pipe(gulp.dest(contentPath));
});

gulp.task('build-less', function () {

    return gulp.src(appLessFiles)
        .pipe(concat(mergedStyles))
        .pipe(less())
        .pipe(minifyCss(cssMinifyOptions))
        .pipe(gulp.dest(tempMerged));
});

gulp.task('build-browserify', function () {

    return browserify(browserifyOptions)
        .bundle()
        .pipe(source(browserifiedScripts))
        .pipe(buffer())
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

    return gulp.src(appTemplates.concat(bowerTemplates))
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
            baseDir : pathResolve.publicPath(),
            verbose : true,
            selectors : 'head link[rel="icon"]',
            attributes : 'href'
        }))
        .pipe(smoosher({
            base : tempBuild
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