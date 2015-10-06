app.config([
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

        $routeProvider.otherwise({ redirectTo : '/' });
    }
]);