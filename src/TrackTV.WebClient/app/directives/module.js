(function () {
    'use strict';

    var name = 'tt.directives',
        dependencies = ['tt.services', 'tt.filters'];

    window.ngModules.directives = angular.module(name, dependencies);

}());