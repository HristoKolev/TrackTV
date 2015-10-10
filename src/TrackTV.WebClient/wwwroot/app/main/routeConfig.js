(function () {
    'use strict';

    ngModules.main.config([
        '$routeProvider', 'templateProvider',
        function ($routeProvider, templateProvider) {

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
                controller : 'ShowsController'
            });

            $routeProvider.when('/shows/genre/:genre', {
                templateUrl : templateProvider.view('shows-by-genre'),
                controller : 'ShowsController'
            });

            $routeProvider.otherwise({ redirectTo : '/' });
        }
    ]);
})();