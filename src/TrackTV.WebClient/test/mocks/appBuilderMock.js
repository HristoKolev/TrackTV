'use strict';

const path = require('path');

module.exports = function (appPath, basePath) {

    const publicApi = {};

    publicApi.stat = function stat(p) {

        let basePath = p.replace('\\content', '').replace('\\include', '');
        let isGlobal = p === path.join(appPath, 'global_include') || p === path.join(appPath, 'global_content');

        let lastCharAsANumber = parseInt(basePath[basePath.length - 1], 10);

        let isEven = () => lastCharAsANumber % 2 === 0;
        let isOdd = () => lastCharAsANumber % 2 !== 0;

        let isDirectory;

        if (p.endsWith('\\content')) {

            isDirectory = isEven;

        } else if (p.endsWith('\\include')) {

            isDirectory = isOdd;
        }
        else if (isGlobal) {

            isDirectory = () => true;
        }
        else {
            isDirectory = () => false;
        }

        return {
            isDirectory
        };
    };

    publicApi.getModules = function getModules() {

        function format(name) {

            return {
                name,
                fullPath: path.join(basePath, appPath, name)
            };
        }

        return [
            format('module1'),
            format('module2')
        ];
    };

    publicApi.getSubmodules = function getSubmodules(fullPath) {

        function format(name) {

            return {
                name,
                fullPath: path.join(fullPath, name)
            };
        }

        return [
            format('sub1'),
            format('sub1/sub2')
        ];
    };

    return publicApi;
};