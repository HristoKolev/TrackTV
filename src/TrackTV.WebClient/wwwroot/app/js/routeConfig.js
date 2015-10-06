app.config([
    '$routeProvider', 'templateProvider',
    function ($routeProvider, templateProvider) {

        $routeProvider.when('/', {
            templateUrl: templateProvider.view('calendar'),
            controller: 'CalendarController'
        });

        $routeProvider.when('/calendar/:year/:month', {
            templateUrl : templateProvider.view('calendar'),
            controller : 'CalendarController'
        });

        $routeProvider.otherwise({ redirectTo : '/' });
    }
]);