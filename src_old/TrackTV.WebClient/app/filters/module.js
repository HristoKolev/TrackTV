(function () {
    'use strict';

    var name = 'tt.filters',
        dependencies = [],
        module = angular.module(name, dependencies);

    module.value('moment', window.moment);

    window.ngModules.filters = module;
}());