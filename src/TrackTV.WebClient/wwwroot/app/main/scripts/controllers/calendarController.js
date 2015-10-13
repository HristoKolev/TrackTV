(function () {
    'use strict';

    ngModules.main.controller('CalendarController', [
        '$scope', '$routeParams', 'calendarService',
        function CalendarController ($scope, $routeParams, calendarService) {

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

            function shortDate (date) {

                var monthName = monthNames[date.getMonth()].substr(0, 3);

                var formattedDate = monthName + '. ' + padLeft(date.getDate(), 2, '0');

                return formattedDate;
            }

            function padLeft (str, length, char) {

                str = str.toString();

                return Array((length - str.length) + 1).join(char) + str;
            }

            function todayClass (date) {

                if (date.toDateString() === new Date().toDateString()) {
                    return 'calendar-today';
                }

                return '';
            }

            $scope.shortDate = shortDate;
            $scope.padLeft = padLeft;
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
                console.log(data);

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