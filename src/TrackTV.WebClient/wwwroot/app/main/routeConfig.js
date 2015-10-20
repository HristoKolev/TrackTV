(function () {
    'use strict';

    ngModules.main.config([
        '$routeProvider', 'templateProvider', 'paginationTemplateProvider',
        function ($routeProvider, templateProvider, paginationTemplateProvider) {

            $routeProvider.when('/', {
                templateUrl : templateProvider.view('calendar'),
                controller : 'CalendarController'
            });

            $routeProvider.when('/calendar/:year/:month', {
                templateUrl : templateProvider.view('calendar'),
                controller : 'CalendarController'
            });

            $routeProvider.when('/login', {
                templateUrl : templateProvider.view('login'),
                controller : 'LoginController'
            });

            $routeProvider.when('/register', {
                templateUrl : templateProvider.view('register'),
                controller : 'RegisterController'
            });

            $routeProvider.when('/shows', {
                templateUrl : templateProvider.view('shows'),
                controller: 'ShowsController',
            });

            $routeProvider.when('/shows/genre/:genre', {
                templateUrl : templateProvider.view('shows-by-genre'),
                controller : 'ShowsController',
            });

            $routeProvider.when('/shows/search/:query', {
                templateUrl: templateProvider.view('search'),
                controller: 'SearchController',
                reloadOnSearch: false
            });

            $routeProvider.when('/shows/network/:network', {
                templateUrl: templateProvider.view('network'),
                controller: 'NetworkController',
                reloadOnSearch: false
            });

            $routeProvider.when('/myshows', {
                templateUrl: templateProvider.view('my-shows'),
                controller: 'MyShowsController',
                reloadOnSearch: false
            });

            $routeProvider.when('/show/:show', {
                templateUrl: templateProvider.view('show'),
                controller: 'ShowController'
            });

            paginationTemplateProvider.setPath(templateProvider.lib('dirPagination.tpl'));

            $routeProvider.otherwise({ redirectTo : '/' });
        }
    ]);
})();