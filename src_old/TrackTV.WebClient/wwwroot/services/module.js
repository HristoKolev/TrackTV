(function () {
    'use strict';

    var name = 'tt.services',
        dependencies = [],
        module = angular.module(name, dependencies);

    module.constant('baseServiceUrl', 'http://localhost:5050');

    window.ngModules.services = module;
}());