(function () {
    'use strict';

    window.ngModules.main.controller('CalendarController', [
        '$scope', '$routeParams', 'calendarService', 'templateLoader', '$location', 'identity',
        function CalendarController($scope, $routeParams, calendarService, templateLoader, $location, identity) {

            if (!identity.getCurrentUser().isAuthenticated) {

                $location.path('/shows');
            }

            function processSuccess(response) {

                var data = response.data;

                $scope.info = data.info;
                $scope.weeks = data.month;

                templateLoader.ready();
            }

            if ($routeParams.year && $routeParams.month) {

                calendarService.month($routeParams.year, $routeParams.month).then(processSuccess);
            } else {

                calendarService.currentMonth().then(processSuccess);
            }

            // scope

            $scope.daysOfWeek = [
                'Monday',
                'Tuesday',
                'Wednesday',
                'Thursday',
                'Friday',
                'Saturday',
                'Sunday'
            ];

            function todayClass(date) {

                if (date.toDateString() === new Date().toDateString()) {

                    return 'calendar-today';
                }

                return '';
            }

            $scope.todayClass = todayClass;
        }
    ]);
}());