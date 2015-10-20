(function () {
    'use strict';

    ngModules.filters.filter('monthName', [
        's',
        function monthName(s) {

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

            function filter(number) {

                return monthNames[number - 1];
            }

            return filter;
        }
    ]);
})();