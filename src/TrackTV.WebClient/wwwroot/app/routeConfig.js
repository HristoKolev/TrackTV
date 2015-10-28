(function () {
    'use strict';

    window.ngModules.main.config([
        '$routeProvider', 'templateProvider', 'paginationTemplateProvider', '$locationProvider',
        function ($routeProvider, templateProvider, paginationTemplateProvider, $locationProvider) {

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
                controller : 'ShowsController',
            });

            $routeProvider.when('/shows/genre/:genre', {
                templateUrl : templateProvider.view('shows-by-genre'),
                controller : 'ShowsController',
            });

            $routeProvider.when('/shows/search/:query', {
                templateUrl : templateProvider.view('search'),
                controller : 'SearchController',
                reloadOnSearch : false
            });

            $routeProvider.when('/shows/network/:network', {
                templateUrl : templateProvider.view('network'),
                controller : 'NetworkController',
                reloadOnSearch : false
            });

            $routeProvider.when('/myshows', {
                templateUrl : templateProvider.view('my-shows'),
                controller : 'MyShowsController',
                reloadOnSearch : false
            });

            $routeProvider.when('/show/:show', {
                templateUrl : templateProvider.view('show'),
                controller : 'ShowController'
            });

            $routeProvider.otherwise({ redirectTo : '/' });

            $locationProvider.html5Mode({
                enabled : true,
                requireBase : false,
                rewriteLinks : true
            });

            paginationTemplateProvider.setPath(templateProvider.lib('dirPagination.tpl'));

        }
    ]);
}());