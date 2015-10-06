app.config(function ($routeProvider, templateProvider) {

    $routeProvider.when('/', {
        templateUrl: templateProvider.view('calendar')
    });

    $routeProvider.when('/calendar/:year/:month', {
        templateUrl: templateProvider.view('calendar'),
        controller: 'CalendarController'
    });

    $routeProvider.otherwise({ redirectTo: '/' });
});