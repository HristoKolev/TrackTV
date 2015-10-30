var fs = require('fs'),
    path = require('path'),
    glob = require('glob');

function appBuilder(pathResolver, rootPath, fetchLevel) {

    fetchLevel = fetchLevel || 4;

    var that = Object.create(null);

    function getFolderNames(dir) {

        return fs.readdirSync(dir).filter(function (file) {

            return fs.statSync(path.join(dir, file)).isDirectory();
        });
    }

    that.appPath = function (path) {

        if (path instanceof Array) {

            for (var i = 0; i < path.length; i += 1) {

                path[i] = that.appPath(path[i]);
            }

            return path;

        } else {

            path = path || '';

            return rootPath + path;
        }
    };

    var appPatterns = {
        initFile: '/init.js',
        moduleHeaders: '/**/module.js',
        npmModuleFiles: '/**/npmModules.js',
        moduleConstants: '/**/constants.js',
        moduleLibraries: '/**/libraries.js',
        scripts: '/**/scripts/' + Array(fetchLevel + 1).join('**/') + '*.js',
        routeConfig: '/routeConfig.js',
        templates: '/**/templates/*.html',
        lessFiles: '/**/styles/*.less',
        configFiles: '/*.json'
    };

    Object.keys(appPatterns).forEach(function (index) {

        that[index] = that.appPath(appPatterns[index]);
    });

    that.sourceFiles = [
        that.initFile,
        that.moduleHeaders,
        that.moduleConstants,
        that.moduleLibraries,
        that.scripts,
        that.routeConfig
    ];

    that.existingNpmModueFiles = glob.sync(that.npmModuleFiles);

    return that;
}

module.exports = {
    instance: appBuilder
};