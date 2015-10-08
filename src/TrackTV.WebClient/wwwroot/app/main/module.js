(function () {
    'use strict';

    var name = 'main';

    var dependencies = [
        'ngRoute',
        'ngCookies',
        'ui.gravatar',
        'tt.services',
        'tt.directives'
    ];

    window.ngModules.main = angular.module(name, dependencies);

})();