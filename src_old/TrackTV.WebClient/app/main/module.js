(function () {
    'use strict';

    var name = 'main',
        dependencies = [
            'ngRoute',
            'ngCookies',
            'ngAnimate',
            'ui.gravatar',
            'tt.services',
            'tt.directives',
            'tt.filters',
            'angularUtils.directives.dirPagination'
        ],
        module = angular.module(name, dependencies);

    window.toastr.options = {
        closeButton: true,
        positionClass: 'toast-top-center',
        timeOut: 2000
    };

    module.value('toastr', window.toastr);

    window.ngModules.main = module;
}());