'use strict';

const fs = require('fs');

module.exports = {

    directoryExists: function (dirPath) {

        try {

            return fs.statSync(dirPath).isDirectory();
        }
        catch (err) {

            return false;
        }
    },

    fileExists: function (filePath) {

        try {

            return fs.statSync(filePath).isFile();
        }
        catch (err) {

            return false;
        }
    }
};





