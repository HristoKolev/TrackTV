(function () {
    'use strict';

    ngModules.main.controller('CalendarController', [
        '$scope', '$routeParams', 'calendarService', '_s',
        function CalendarController ($scope, $routeParams, calendarService, _s) {

            $scope.daysOfWeek = [
                'Monday',
                'Tuesday',
                'Wednesday',
                'Thursday',
                'Friday',
                'Saturday',
                'Sunday'
            ];

            var monthNames = [
                'January',
                'February',
                'March',
                'April',
                'May',
                'June',
                'July',
                'August',
                'September',
                'October',
                'November',
                'December'
            ];

            // public

            function todayClass (date) {

                if (date.toDateString() === new Date().toDateString()) {
                    return 'calendar-today';
                }

                return '';
            }

            $scope.todayClass = todayClass;

            // private

            function getMonthModel (date) {

                return {
                    year : date.getFullYear(),
                    month : date.getMonth() + 1,
                    name : monthNames[date.getMonth()]
                };
            }

            function setMonthLinks (date) {

                $scope.thisMonth = getMonthModel(date);

                date.setMonth(date.getMonth() - 1);
                $scope.previosMonth = getMonthModel(date);

                date.setMonth(date.getMonth() + 2);
                $scope.nextMonth = getMonthModel(date);
            }

            function processResponse (response) {

                var data = response.data;

                setMonthLinks(data.date);

                $scope.weeks = data.month;
            }

            if ($routeParams.year && $routeParams.month) {

                calendarService.month($routeParams.year, $routeParams.month).then(processResponse);
            } else {

                calendarService.currentMonth().then(processResponse);
            }
        }
    ]);
})();