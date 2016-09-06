'use strict';

const gulp = require('gulp'),
    gulpTypescript = require('gulp-typescript'),
    tsProject = gulpTypescript.createProject('tsconfig.json'),
    less = require('gulp-less'),
    watch = require('gulp-watch'),
    path = require('path');

const projectBase = {base: './'},
    tsFiles = './app/**/*.ts',
    lessFiles = './app/**/*.less';

gulp.task('dev:ts', function () {

    return gulp.src(tsFiles, projectBase)
        .pipe(gulpTypescript(tsProject))
        .pipe(gulp.dest('.'));
});

gulp.task('dev:less', function () {

    return gulp.src(lessFiles, projectBase)
        .pipe(less())
        .pipe(gulp.dest('.'));
});

gulp.task('watch', function () {

    gulp.watch([tsFiles, lessFiles], ['dev:ts', 'dev:less']);
});

gulp.task('default', ['dev:ts', 'dev:less']);