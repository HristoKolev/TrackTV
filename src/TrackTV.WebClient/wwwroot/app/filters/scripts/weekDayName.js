(function () {
    'use strict';

    window.ngModules.filters.filter('weekDayName', [
        's',
        function weekDayName(s) {

            var daysOfWeek = [
                'Monday',
                'Tuesday',
                'Wednesday',
                'Thursday',
                'Friday',
                'Saturday',
                'Sunday'
            ];

            function filter(number) {

                return daysOfWeek[number - 1];
            }

            return filter;
        }
    ]);
}());