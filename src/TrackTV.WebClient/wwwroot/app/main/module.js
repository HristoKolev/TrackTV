(function () {
    'use strict';

    var name = 'main';

    var dependencies = [
        'ngRoute',
        'ngCookies',
        'ui.gravatar',
        'tt.services',
        'tt.directives',
        'angularUtils.directives.dirPagination'
    ];

    window.ngModules.main = angular.module(name, dependencies);

})();