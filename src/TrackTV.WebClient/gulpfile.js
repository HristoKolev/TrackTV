/// <binding AfterBuild='default' Clean='default' ProjectOpened='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    del = require('del'),
    concat = require('gulp-concat');

function publicPath (path) {
    return 'wwwroot/' + path;
}

// constants
var libsPath = publicPath('lib');
var fontsPath = publicPath('fonts');
var bowerRootPath = 'bower_components';

// helper functions
function bowerComponent(path) {
    return bowerRootPath + '/' + path;
}

// Contents of the third-party.js
var scripts = [
    bowerComponent('jquery/dist/jquery.js'),
    bowerComponent('bootstrap/dist/js/bootstrap.js'),
    bowerComponent('angular/angular.js'),
];

// Contents of the third-party.css
var styles = [
    bowerComponent('bootstrap/dist/css/bootstrap.css'),
    bowerComponent('bootstrap/dist/css/bootstrap-theme.css'),
    bowerComponent('bootswatch/cosmo/bootstrap.css'),
];

(function RegisterTasks() {

    gulp.task('clean', function () {
        del([libsPath, fontsPath]);
    });

    gulp.task('scripts', function () {
        gulp.src(scripts)
            .pipe(concat('third-party.js'))
            .pipe(gulp.dest(libsPath));
    });

    gulp.task('styles', function () {
        gulp.src(styles)
            .pipe(concat('third-party.css'))
            .pipe(gulp.dest(libsPath));
    });

    gulp.task('fonts', function () {
        gulp.src([bowerComponent('bootstrap/dist/fonts/*')])
            .pipe(gulp.dest(fontsPath));
    });

    gulp.task('default', ['clean', 'scripts', 'styles', 'fonts']);

})();