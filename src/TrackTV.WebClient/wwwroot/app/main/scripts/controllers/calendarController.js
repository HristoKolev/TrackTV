(function () {
    'use strict';

    ngModules.main.controller('CalendarController', [
        '$scope', '$routeParams', 'calendarService', 'templateLoader',
    function CalendarController ($scope, $routeParams, calendarService, templateLoader) {

            function processResponse (response) {

                var data = response.data;

                $scope.info = data.info;
                $scope.weeks = data.month;

                templateLoader.ready();
            }

            if ($routeParams.year && $routeParams.month) {

                calendarService.month($routeParams.year, $routeParams.month).then(processResponse);
            } else {

                calendarService.currentMonth().then(processResponse);
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

            function todayClass (date) {

                if (date.toDateString() === new Date().toDateString()) {
                    return 'calendar-today';
                }

                return '';
            }

            $scope.todayClass = todayClass;
        }
    ]);
})();