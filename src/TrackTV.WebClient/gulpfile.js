/// <binding Clean='default' ProjectOpened='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

'use strict';

var gulp = require('gulp'),
    del = require('del'),
    concat = require('gulp-concat'),
    less = require('gulp-less'),
    fs = require('fs'),
    path = require('path'),

    smoosher = require('gulp-smoosher'),
    replace = require('gulp-replace'),
    uglify = require('gulp-uglify'),
    minifyCss = require('gulp-minify-css'),
    insert = require('gulp-insert'),
    runSequence = require('run-sequence'),
    file = require('gulp-file'),
    minifyHtml = require('gulp-minify-html');

var embedMedia = require('./modules/gulp-embed-media');

var settings = require('./wwwroot/app/settings.json'),
    sourceManager = require('./modules/sourceManager');

sourceManager.setSettings(settings.source);

(function () {
    // Workaround for https://github.com/gulpjs/gulp/issues/71
    var origSrc = gulp.src;
    gulp.src = function () {
        return fixPipe(origSrc.apply(this, arguments));
    };

    function fixPipe (stream) {
        var origPipe = stream.pipe;
        stream.pipe = function (dest) {
            arguments[0] = dest.on('error', function (error) {
                var nextStreams = dest._nextStreams;
                if (nextStreams) {
                    nextStreams.forEach(function (nextStream) {
                        nextStream.emit('error', error);
                    });
                } else if (dest.listeners('error').length === 1) {
                    throw error;
                }
            });
            var nextStream = fixPipe(origPipe.apply(this, arguments));
            (this._nextStreams || (this._nextStreams = [])).push(nextStream);
            return nextStream;
        };
        return stream;
    }
})();

function getFolderNames (dir) {
    return fs.readdirSync(dir).filter(function (file) {
        return fs.statSync(path.join(dir, file)).isDirectory();
    });
}

function getFileNames(dir) {
    return fs.readdirSync(dir).filter(function (file) {
        return fs.statSync(path.join(dir, file)).isFile();
    });
}

var appBuilder = sourceManager.createSourceListBuilder()
    .addPublicFile('/app/init.js')
    .addModule(getFolderNames(settings.source.moduleRootPath))
    .addModuleFile('main', '/routeConfig.js');

var pathResolve = sourceManager.pathResolve;
var modulePath = sourceManager.modulePath;

// source files

var lessFiles = appBuilder.lessFiles();

var bowerScripts = pathResolve.bowerComponent([
    '/jquery/dist/jquery.js',
    '/bootstrap/dist/js/bootstrap.js',
    '/toastr/toastr.js',
    '/angular/angular.js',
    '/angular-route/angular-route.js',
    '/angular-cookies/angular-cookies.js',
    '/angular-gravatar/build/angular-gravatar.js',
    '/angular-utils-pagination/dirPagination.js',
    '/underscore/underscore.js',
    '/moment/moment.js'
]);

var npmScripts = pathResolve.npmComponent([
    '/underscore.string/dist/underscore.string.js'
]);

var styles = pathResolve.bowerComponent([
    '/bootstrap/dist/css/bootstrap.css',
    '/bootswatch/cosmo/bootstrap.css',
    '/toastr/toastr.css'
]);

var fonts = pathResolve.bowerComponent('/bootstrap/dist/fonts/*');

var templates = appBuilder.templates();

var libTemplates = pathResolve.bowerComponent([
    '/angular-utils-pagination/dirPagination.tpl.html'
]);

var appScripts = appBuilder.scripts();

// dev tasks

gulp.task('clean', function (cb) {

    del(pathResolve.publicPath([
        '/lib',
        '/merged-styles.css',
        '/merged-scripts.js'
    ]));

    cb();
});

gulp.task('scripts', function () {

    var libPath = pathResolve.publicPath('/lib');

    return gulp.src(bowerScripts.concat(npmScripts))
        .pipe(concat('third-party.js'))
        .pipe(gulp.dest(libPath));

});

gulp.task('templates', function () {

    return gulp.src(libTemplates)
        .pipe(gulp.dest(pathResolve.publicPath('/lib/templates')));
});

gulp.task('styles', function () {

    return gulp.src(styles)
        .pipe(concat('third-party.css'))
        .pipe(gulp.dest(pathResolve.publicPath('/lib/css')));
});

gulp.task('fonts', function () {

    var fontsPath = pathResolve.publicPath('/lib/fonts');

    return gulp.src([fonts])
        .pipe(gulp.dest(fontsPath));
});

gulp.task('less', function () {

    return gulp.src(lessFiles)
        .pipe(concat('merged-styles.css'))
        .pipe(less())
        .pipe(gulp.dest(pathResolve.publicPath()))
        .on('error', console.error);
});

gulp.task('merge', function () {

    return gulp.src(appScripts)
        .pipe(concat('merged-scripts.js'))
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
    var sourceFiles = modulePath.getSourceFilesPattern();
    gulp.watch(sourceFiles, ['merge']);
    console.log('Watching: ' + sourceFiles);
});

var buildPath = pathResolve.publicPath('/build');
var contentPath = buildPath + '/content';
var tempBuild = buildPath + '/temp';

var html = pathResolve.publicPath('/index.html');

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

function commentPlaceholder (id) {

    return new RegExp('<!--\\s*?' + id + '\\s*?-->', 'g');
}

// build tasks

gulp.task('build-clean', function (callback) {

    del(buildPath);

    callback();
});

gulp.task('build-scripts', function () {

    return gulp.src(bowerScripts.concat(npmScripts))
        .pipe(concat('third-party.js'))
        .pipe(uglify())
        .pipe(gulp.dest(tempBuild + '/lib'));
});

gulp.task('build-source', function () {

    return gulp.src(appScripts)
        .pipe(concat('merged-scripts.js'))
        .pipe(uglify())
        .pipe(gulp.dest(tempBuild));;

});

gulp.task('build-styles', function () {

    return gulp.src(styles)
        .pipe(concat('third-party.css'))
        .pipe(minifyCss(cssMinifyOptions))
        .pipe(replace('../fonts/', './content/'))
        .pipe(gulp.dest(tempBuild + '/lib/css'));
});

gulp.task('build-fonts', function () {

    return gulp.src([fonts])
        .pipe(gulp.dest(contentPath));
});

gulp.task('build-less', function () {

    return gulp.src(lessFiles)
        .pipe(concat('merged-styles.js'))
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

    return gulp.src(templates.concat(libTemplates))
        .pipe(embedMedia(embedMediaOptions))
        .pipe(insert.transform(function (contents, file) {

            return wrapTemplate(file.path.split('\\').pop(), contents);
        }))
        .pipe(concat('templates.html'))
        .pipe(gulp.dest(tempBuild));
});

gulp.task('build-settings', function () {

    settings.templates.cached = true;
    var content = '<script> window.settings = ' + JSON.stringify(settings) + '; </script>';

    return createFile('settings.html', content)
        .pipe(gulp.dest(tempBuild));
});

gulp.task('build-merge', function () {

    var templateRegex = commentPlaceholder('templates');
    var settingsRegex = commentPlaceholder('settings');

    var templateContent = fs.readFileSync(tempBuild + '/' + 'templates.html');
    var settingsContent = fs.readFileSync(tempBuild + '/' + 'settings.html');

    return gulp.src(html)
        .pipe(embedMedia({
            baseDir : pathResolve.publicPath(),
            verbose : true,
            selectors : 'head link[rel="icon"]',
            attributes : 'href'
        }))
        .pipe(replace(templateRegex, templateContent))
        .pipe(replace(settingsRegex, settingsContent))
        .pipe(smoosher({
            base : tempBuild
        }))
        .pipe(minifyHtml(htmlMinifyOptions))
        .pipe(gulp.dest(buildPath));
});

gulp.task('build-clear', function (callback) {

    del(tempBuild);

    callback();
});

gulp.task('build', function () {

    runSequence(
        'build-clean',
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