(function () {
    'use strict';

    window.ngModules.filters.filter('weekDayName', [
        function weekDayName() {

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