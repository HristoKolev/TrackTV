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
    path = require('path');

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

var sourceManager = (function () {

    function PathResolver () {

        this.rootPath = 'wwwroot';
        this.bowerRootPath = 'bower_components';
        this.npmRootPath = 'node_modules';

        PathResolver.prototype.publicPath = function (path) {

            if (path instanceof Array) {

                for (var index in path) {
                    path[index] = this.publicPath(path[index]);
                }

                return path;

            } else {
                path = path || '';

                return this.rootPath + '/' + path;
            }
        };

        PathResolver.prototype.bowerComponent = function (path) {

            if (!path) {
                throw Error('You must specify the path of the component.');
            }

            if (path instanceof Array) {

                for (var index in path) {
                    path[index] = this.bowerComponent(path[index]);
                }

                return path;

            } else {

                return this.bowerRootPath + '/' + path;
            }
        };

        PathResolver.prototype.npmComponent = function (path) {

            if (!path) {
                throw Error('You must specify the path of the component.');
            }

            if (path instanceof Array) {

                for (var index in path) {
                    path[index] = this.npmComponent(path[index]);
                }

                return path;

            } else {

                return this.npmRootPath + '/' + path;
            }
        };
    }

    function ModulePathResolver(pathResolver) {

        this._pathResolver = pathResolver;
        this.moduleRootPath = this._pathResolver.publicPath('app');
        this.fetchLevel = 4;

        ModulePathResolver.prototype.modulePath = function (moduleName, path) {

            if (!moduleName) {
                throw Error('You must specifiy the name of the module.');
            }

            if (path instanceof Array) {

                for (var index in path) {

                    path[index] = this.modulePath(moduleName, path[index]);
                }

                return path;

            } else {

                path = path || '';

                return this.moduleRootPath + '/' + moduleName + '/' + path;
            }
        };

        ModulePathResolver.prototype.getSourceFilesPattern = function () {

            return this.moduleRootPath + '/' + Array(this.fetchLevel + 1).join('**/') + '*.js';
        };
    }

    function SourceListBuilder (pathResolver, modulePathResolver) {

        this._pathResolver = pathResolver;
        this._modulePathResolver = modulePathResolver;
        this._files = [];

        SourceListBuilder.prototype.addModule = function (name) {

            if (name instanceof Array) {

                for (var index in name) {

                    this.addModule(name[index]);
                }

            } else {

                this.addFile(this._modulePathResolver.modulePath(name, 'module.js'));
                this.addFile(this._modulePathResolver.modulePath(name, 'constants.js'));
                this.addFile(this._modulePathResolver.modulePath(name, 'libraries.js'));
                this.addFile(this._modulePathResolver.modulePath(name, 'scripts/**/**/*.js'));
            }

            return this;
        };

        SourceListBuilder.prototype.addPublicFile = function (path) {

            return this.addFile(this._pathResolver.publicPath(path));
        };

        SourceListBuilder.prototype.addFile = function (path) {

            if (path instanceof Array) {

                for (var index in path) {

                    this.addFile(path[index]);
                }

            } else {

                this._files.push(path);
            }

            return this;

        };

        SourceListBuilder.prototype.addModuleFile = function (moduleName, path) {

            this.addFile(this._modulePathResolver.modulePath(moduleName, path));

            return this;
        };

        SourceListBuilder.prototype.src = function () {

            return this._files;
        };

        SourceListBuilder.prototype.clear = function () {

            this._files = [];
        };

    }

    var pathResolver = new PathResolver();
    var modulePath = new ModulePathResolver(pathResolver);

    var module = {
        pathResolve : pathResolver,
        modulePath : modulePath,
        createSourceListBuilder : function () {
            return new SourceListBuilder(pathResolver, modulePath);
        }
    };

    return module;
}());

function getFolders (dir) {
    return fs.readdirSync(dir)
        .filter(function (file) {
            return fs.statSync(path.join(dir, file)).isDirectory();
        });
}

var pathResolve = sourceManager.pathResolve;
var modulePath = sourceManager.modulePath;

var lessFiles = modulePath.modulePath('main', 'styles/*.less');

gulp.task('clean', function () {

    del.sync(pathResolve.publicPath([
        'lib',
        'styles.css',
        'merged-app.js'
    ]));
});

gulp.task('scripts', function () {

    var bowerScripts = pathResolve.bowerComponent([
        'jquery/dist/jquery.js',
        'bootstrap/dist/js/bootstrap.js',
        'toastr/toastr.js',
        'angular/angular.js',
        'angular-route/angular-route.js',
        'angular-cookies/angular-cookies.js',
        'angular-gravatar/build/angular-gravatar.js',
        'angular-utils-pagination/dirPagination.js',
        'underscore/underscore.js',
        'moment/moment.js'
    ]);

    var npmScripts = pathResolve.npmComponent([
        'underscore.string/dist/underscore.string.js'
    ]);

    var libPath = pathResolve.publicPath('lib');

    gulp.src(bowerScripts.concat(npmScripts))
        .pipe(concat('third-party.js'))
        .pipe(gulp.dest(libPath));

    gulp.src(pathResolve.bowerComponent('angular-utils-pagination/dirPagination.tpl.html'))
        .pipe(gulp.dest(pathResolve.publicPath('lib/templates')));
});

gulp.task('styles', function () {

    var styles = pathResolve.bowerComponent([
        'bootstrap/dist/css/bootstrap.css',
        'bootswatch/cosmo/bootstrap.css',
        'toastr/toastr.css'
    ]);

    gulp.src(styles)
        .pipe(concat('third-party.css'))
        .pipe(gulp.dest(pathResolve.publicPath('lib/css')));
});

gulp.task('fonts', function () {

    var fontsPath = pathResolve.publicPath('lib/fonts');

    gulp.src([pathResolve.bowerComponent('bootstrap/dist/fonts/*')])
        .pipe(gulp.dest(fontsPath));
});

gulp.task('less', function () {

    gulp.src([lessFiles])
        .pipe(less())
        .pipe(gulp.dest(pathResolve.publicPath()))
        .on('error', console.error);
});

gulp.task('merge', function () {

    var builder = sourceManager.createSourceListBuilder();

    builder.addPublicFile('app/init.js')
        .addModule(getFolders(modulePath.moduleRootPath))
        .addModuleFile('main', 'routeConfig.js');

    gulp.src(builder.src())
        .pipe(concat('merged-app.js'))
        .pipe(gulp.dest(pathResolve.publicPath()));;
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

gulp.task('default', ['clean', 'scripts', 'styles', 'fonts', 'less', 'merge']);