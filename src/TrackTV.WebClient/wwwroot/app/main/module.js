(function () {
    'use strict';

    var name = 'main';

    var dependencies = [
        'ngRoute',
        'ngCookies',
        'ui.gravatar',
        'tt.services',
        'tt.directives',
        'tt.filters',
        'angularUtils.directives.dirPagination'
    ];

    window.ngModules.main = angular.module(name, dependencies);

})();