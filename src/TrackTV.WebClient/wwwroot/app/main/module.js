(function () {
    'use strict';

    var name = 'main';

    var dependencies = [
        'ngRoute',
        'ngCookies',
        'ui.gravatar',
        'ff.services',
        'ff.directives'
    ];

    window.ngModules.main = angular.module(name, dependencies);

})();