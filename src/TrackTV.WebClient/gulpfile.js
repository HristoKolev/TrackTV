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
    saveFile = require('gulp-savefile');

var settings = require('./wwwroot/app/settings.json'),
    pathConfig = require('./config/path.json'),
    includes = require('./config/includes.json');

var embedMedia = require('./modules/gulp-embed-media'),
    pathResolve = require('./modules/pathResolver').instance(pathConfig),
    appBuilder = require('./modules/appBuilder').instance(pathResolve, settings.source.moduleRootPath),
    fixGulp = require('./modules/fix-gulp'),
    fillContent = require('./modules/fill-content');

fixGulp(gulp);

// source files

var lessFiles = appBuilder.lessFiles();

var bowerScripts = pathResolve.bowerComponent(includes.bowerScripts);

var npmScripts = pathResolve.npmComponent(includes.npmScripts);

var bowerStyles = pathResolve.bowerComponent(includes.bowerStyles);

var fonts = pathResolve.bowerComponent(includes.bowerFonts);

var templates = appBuilder.templates();

var bowerTemplates = pathResolve.bowerComponent(includes.bowerTemplates);

var appScripts = appBuilder.scripts();

// constants

var thirdPartyJs = 'third-party.js';
var thirdPartyCss = 'third-party.css';

var mergedStylesCss = '/merged-styles.css';
var mergedScriptsJs = '/merged-scripts.js';

// dev tasks

gulp.task('clean', function (callback) {

    del(pathResolve.publicPath([
        '/lib',
        mergedStylesCss,
        mergedScriptsJs
    ]));

    callback();
});

gulp.task('scripts', function () {

    var libPath = pathResolve.publicPath('/lib');

    return gulp.src(bowerScripts.concat(npmScripts))
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

    return gulp.src(fonts)
        .pipe(gulp.dest(fontsPath));
});

gulp.task('less', function () {

    return gulp.src(lessFiles)
        .pipe(concat(mergedStylesCss))
        .pipe(less())
        .pipe(gulp.dest(pathResolve.publicPath()))
        .on('error', console.error);
});

gulp.task('merge', function () {

    return gulp.src(appScripts)
        .pipe(concat(mergedScriptsJs))
        .pipe(gulp.dest(pathResolve.publicPath()));;

});

gulp.task('default', function () {

    runSequence(
        'clean',
        'styles',
        'fonts',
        'scripts',
        'templates',
        'less',
        'merge'
    );
});

gulp.task('watch', function () {

    //less files
    gulp.watch(lessFiles, ['less']);
    console.log('Watching: ' + lessFiles);

    //angular app
    gulp.watch(appScripts, ['merge']);
    console.log('Watching: ' + appScripts);
});

var buildPath = pathResolve.publicPath('/build');
var contentPath = buildPath + '/content';
var tempBuild = buildPath + '/temp';

var html = buildPath + '/index.html';

var cssMinifyOptions = {
    processImportFrom : ['local'],
    keepSpecialComments : 0
};

var htmlMinifyOptions = {
    empty : true,
};

var embedMediaOptions = {
    baseDir : pathResolve.publicPath(),
    verbose : true
};

function createFile (name, contents) {

    return file(name, contents, { src : true });
}

// build tasks

gulp.task('build-clean', function (callback) {

    del(buildPath);

    callback();
});

gulp.task('build-index', function () {

    return gulp.src(pathResolve.publicPath('/index.html'))
        .pipe(gulp.dest(buildPath));
});

gulp.task('build-scripts', function () {

    return gulp.src(bowerScripts.concat(npmScripts))
        .pipe(concat(thirdPartyJs))
        .pipe(uglify())
        .pipe(gulp.dest(tempBuild + '/lib'));
});

gulp.task('build-source', function () {

    return gulp.src(appScripts)
        .pipe(concat(mergedScriptsJs))
        .pipe(uglify())
        .pipe(gulp.dest(tempBuild));;
});

gulp.task('build-styles', function () {

    return gulp.src(bowerStyles)
        .pipe(concat(thirdPartyCss))
        .pipe(minifyCss(cssMinifyOptions))
        .pipe(replace('../fonts/', './content/'))
        .pipe(gulp.dest(tempBuild + '/lib/css'));
});

gulp.task('build-fonts', function () {

    return gulp.src(fonts)
        .pipe(gulp.dest(contentPath));
});

gulp.task('build-less', function () {

    return gulp.src(lessFiles)
        .pipe(concat(mergedStylesCss))
        .pipe(less())
        .pipe(minifyCss(cssMinifyOptions))
        .pipe(gulp.dest(tempBuild));
});

gulp.task('build-copy-content', function () {

    return gulp.src(pathResolve.publicPath('/content/*'))
        .pipe(gulp.dest(contentPath));
});

gulp.task('build-templates', function () {

    function wrapTemplate (name, contents) {

        return '<script type="text/ng-template" id="' + name + '">\n' + contents + '\n</script>';
    }

    return gulp.src(templates.concat(bowerTemplates))
        .pipe(embedMedia(embedMediaOptions))
        .pipe(insert.transform(function (contents, file) {

            return wrapTemplate(file.path.split('\\').pop(), contents);
        }))
        .pipe(concat('templates'))
        .pipe(fillContent(html, 'templates'));
});

gulp.task('build-settings', function () {

    settings.templates.cached = true;
    var content = '<script> window.settings = ' + JSON.stringify(settings) + '; </script>';

    
    return createFile('settings.html', content)
        .pipe(fillContent(html, 'settings'));
});

gulp.task('build-merge', function () {

    return gulp.src(html)
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

    del(tempBuild);

    callback();
});

gulp.task('build', function () {

    runSequence(
        'build-clean',
        'build-index',
        'build-scripts',
        'build-source',
        'build-styles',
        //'build-fonts',
        'build-less',
        'build-copy-content',
        'build-templates',
        'build-settings',
        'build-merge', 'build-clear'
    );
});