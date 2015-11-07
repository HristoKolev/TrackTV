'use strict';

var fs = require('fs');

function commentPlaceholder(placeholder) {

    return new RegExp('<!--\\s*?' + placeholder + '\\s*?-->', 'g');
}

function replaceContent(destinationFile, placeholder, replacement) {

    if (!destinationFile) {

        throw new Error('The destination path is invalid. :' + destinationFile);
    }

    if (!placeholder) {

        throw new Error('The placeholder is invalid. :' + placeholder);
    }

    if (!replacement) {

        throw new Error('The replacement is invalid. :' + replacement);
    }

    var content = fs.readFileSync(destinationFile).toString();

    var regex = commentPlaceholder(placeholder);

    content = content.replace(regex, replacement);

    fs.writeFileSync(destinationFile, content);
}

module.exports = replaceContent;