'use strict';

const path = require('path');

function getReport(paths, validations) {

    let report = [];

    function evaluate(name, func) {

        if (!name) {

            throw new Error('The evaluation name is invalid');
        }

        if (!func) {

            throw new Error('The evaluation function is invalid');
        }

        let message = func(paths.slice());

        if (message) {

            report.push({
                name,
                message
            });
        }
    }

    validations(evaluate);

    return report;
}

function validate(paths, appRootPath, validations) {

    if (!paths) {

        throw new Error('The paths are invalid.');
    }

    if (!Array.isArray(paths)) {

        throw new Error('The paths argument is not an array');
    }

    if (paths.length === 0) {

        throw new Error('The paths array is empty.');
    }

    if (!appRootPath) {

        throw new Error('The app root path is invalid.');
    }

    for (let element of paths) {

        if (!element.startsWith(appRootPath)) {

            throw new Error('The path is not in the app directory. File: ' + element + '; App path: ' + appRootPath);
        }
    }

    if (!validations) {

        throw new Error('The validations are invalid.');
    }

    paths = paths.slice();

    for (var i = 0; i < paths.length; i += 1) {

        paths[i] = path.relative(appRootPath, paths[i]);
    }

    return getReport(paths, validations);
}

module.exports = {
    validate
};

