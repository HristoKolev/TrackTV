'use strict';

var path = require('path'),
    fs = require('fs');

function includer(output, appBuilder) {

    var that = Object.create(null);

    var outputIndex = output('index.html'),
        includeLog = output('includes.json');

    // custom modules
    var fillContent = require('./fill-content').external,
        listScripts = require('./list-resources'),
        copyFiles = require('./copyFiles');

    // logic

    var scriptFormatter = function (resourcePath) {

        return '<script src="' + resourcePath + '"></script>';
    };

    var styleFormatter = function (resourcePath) {

        return '<link rel="stylesheet" href="' + resourcePath + '">';
    };

    that.formatters = {
        scriptFormatter: 'script',
        styleFormatter: 'style'
    };

    function removeBaseDir(files, baseDir) {

        files = files.slice();

        for (var i = 0; i < files.length; i += 1) {

            if (files[i].indexOf(baseDir) !== 0) {

                throw new Error('This file does not start with the provided base directory. baseDir: ' + baseDir + '; fileName: ' + files[i]);
            }

            files[i] = files[i].slice(baseDir.length);
        }

        return files;
    }

    function renameModuleFile(fileName) {

        var ext = path.extname(fileName);
        var base = path.basename(fileName, ext);

        var moduleName = path.basename(path.dirname(fileName));

        return path.join(moduleName + '-' + base + ext);
    }

    function separateModuleFile(fileName) {

        var ext = path.extname(fileName);
        var base = path.basename(fileName, ext);

        var moduleName = path.basename(path.dirname(fileName));

        return path.join(moduleName, base + ext);
    }

    function getFormatter(name) {

        var formattersByName = {};

        formattersByName[that.formatters.scriptFormatter] = scriptFormatter;
        formattersByName[that.formatters.styleFormatter] = styleFormatter;

        var formatter = formattersByName[name];

        if (!formatter) {

            throw new Error('Unknown formatter name: ' + name);
        }

        return formatter;

    }

    function readIncludes() {

        var obj = JSON.parse(fs.readFileSync(includeLog.value()));

        Object.keys(obj).forEach(function (index) {

            obj[index].formatter = getFormatter(obj[index].formatter);
        });

        return obj;
    }

    that.createIncludeLog = function () {

        fs.writeFileSync(includeLog.value(), '{}');
    };

    that.logIncludes = function (name, files, formatter) {

        var obj = JSON.parse(fs.readFileSync(includeLog.value()));

        obj[name] = {
            files: files,
            formatter: formatter
        };

        fs.writeFileSync(includeLog.value(), JSON.stringify(obj, null, '\t'));
    };

    function injectApplicationFiles(placeholder, files, formatter) {

        files = removeBaseDir(files, output.value());

        that.logIncludes(placeholder, files, formatter);
    }

    that.copyIndex = function () {

        copyFiles.copy(appBuilder.indexFile, output.value());
    };

    that.updateIncludes = function () {

        that.copyIndex();

        var includes = readIncludes();

        Object.keys(includes).forEach(function (index) {

            var include = includes[index];

            fillContent(outputIndex.value(), index, listScripts(include.files, include.formatter));
        });
    };

    that.includeDirectory = function (name, list, baseDir, formatter) {

        var newList = copyFiles.copyStructure(list, output(name).value(), baseDir);

        injectApplicationFiles(name, newList, formatter);
    };

    that.includeFile = function (placeholder, file, baseDir, formatter) {

        var newList = copyFiles.copyStructure([file], output.value(), baseDir);

        injectApplicationFiles(placeholder, newList, formatter);
    };

    that.includeModuleFiles = function (name, list, formatter) {

        var newList = copyFiles.copy(list, output(name).value(), renameModuleFile);

        injectApplicationFiles(name, newList, formatter);
    };

    that.includeSeparatedModuleFiles = function (name, list, baseDir, formatter) {

        var newList = copyFiles.copyStructure(list, output(name).value(), baseDir, separateModuleFile);

        injectApplicationFiles(name, newList, formatter);
    };

    return that;
}

module.exports = {
    instance: includer
};