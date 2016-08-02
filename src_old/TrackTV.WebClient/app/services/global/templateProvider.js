(function () {
    'use strict';

    function createTemplateProvider(moduleName) {

        function view(name) {

        }

        function directive(name) {

        }

        function lib(name) {

        }

        return {
            view: view,
            directive: directive,
            lib: lib
        };
    }

    for (var moduleName in window.ngModules) {

        window.ngModules[moduleName].constant('templateProvider', createTemplateProvider(moduleName));
    }

}());