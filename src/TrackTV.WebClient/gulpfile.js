/// <binding Clean='default' ProjectOpened='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

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
    runSequence = require('run-sequence');
file = require('gulp-file');

var settings = require('./wwwroot/app/settings.json'),
    sourceManager = require('./sourceManager');

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

function getFolders (dir) {
    return fs.readdirSync(dir)
        .filter(function (file) {
            return fs.statSync(path.join(dir, file)).isDirectory();
        });
}

function getAppScripts () {

    var builder = sourceManager.createSourceListBuilder();

    builder.addPublicFile('/app/init.js')
        .addModule(getFolders(settings.source.moduleRootPath))
        .addModuleFile('main', '/routeConfig.js');

    return builder.src();
}

var pathResolve = sourceManager.pathResolve;
var modulePath = sourceManager.modulePath;

// source files

var lessFiles = modulePath.modulePath('main', '/styles/*.less');

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

var templates = pathResolve.bowerComponent([
    '/angular-utils-pagination/dirPagination.tpl.html'
]);

var appScripts = getAppScripts();

// dev tasks

gulp.task('clean', function (cb) {

    del(pathResolve.publicPath([
        '/lib',
        '/styles.css',
        '/merged-app.js'
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

    return gulp.src(templates)
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

    return gulp.src([lessFiles])
        .pipe(less())
        .pipe(gulp.dest(pathResolve.publicPath()))
        .on('error', console.error);
});

gulp.task('merge', function () {

    return gulp.src(appScripts)
        .pipe(concat('merged-app.js'))
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

    var sourceFiles = modulePath.getSourceFilesPattern();

    //angular app
    gulp.watch(sourceFiles, ['merge']);
    console.log('Watching: ' + sourceFiles);
});

var buildPath = pathResolve.publicPath('/build');
var contentPath = buildPath + '/content';
var tempBuild = buildPath + '/temp';

var html = pathResolve.publicPath('/index.html');

var minifyOptions = {
    processImportFrom : ['local'],
    keepSpecialComments : 0
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
        .pipe(concat('merged-app.js'))
        .pipe(uglify())
        .pipe(gulp.dest(tempBuild));;

});

gulp.task('build-styles', function () {

    return gulp.src(styles)
        .pipe(concat('third-party.css'))
        .pipe(minifyCss(minifyOptions))
        .pipe(replace('../fonts/', './content/'))
        .pipe(gulp.dest(tempBuild + '/lib/css'));
});

gulp.task('build-fonts', function () {

    return gulp.src([fonts])
        .pipe(gulp.dest(contentPath));
});

gulp.task('build-less', function () {

    return gulp.src([lessFiles])
        .pipe(less())
        .pipe(minifyCss(minifyOptions))
        .pipe(gulp.dest(tempBuild));
});

gulp.task('build-copy-content', function () {

    return gulp.src(pathResolve.publicPath('/content/*'))
        .pipe(gulp.dest(contentPath));
});

gulp.task('build-templates', function () {

    var tmpSettings = settings.templates;
    var ext = tmpSettings.extension;

    var templates = pathResolve.publicPath([
        '/' + tmpSettings.viewPath + '/*' + ext,
        '/' + tmpSettings.directivePath + '/*' + ext,
        '/' + tmpSettings.libPath + '/*' + ext
    ]);

    function wrapTemplate (name, contents) {

        return '<script type="text/ng-template" id="' + name + '">\n' + contents + '\n</script>';
    }

    return gulp.src(templates)
        .pipe(insert.transform(function (contents, file) {

            return wrapTemplate(file.path.split('\\').pop(), contents);
        }))
        .pipe(concat('templates.html'))
        .pipe(gulp.dest(tempBuild));
});

gulp.task('build-settings', function () {

    var content = '<script> window.settings = ' + JSON.stringify(settings) + '; </script>';

    return createFile('settings.html', content)
        .pipe(gulp.dest(tempBuild));
});

gulp.task('build-merge', function () {

    var templateRegex = commentPlaceholder('templates');
    
    var settingsRegex = commentPlaceholder('settings');
    console.log(settingsRegex);
    
    var templateContent = fs.readFileSync(tempBuild + '/' + 'templates.html');
    var settingsContent = fs.readFileSync(tempBuild + '/' + 'settings.html');

    return gulp.src(html)
        .pipe(replace(templateRegex, templateContent))
        .pipe(replace(settingsRegex, settingsContent))
        .pipe(smoosher({
            base : tempBuild
        }))
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
        'build-fonts',
        'build-less',
        'build-copy-content',
        'build-templates',
        'build-settings',
        'build-merge','build-clear'
    );
});