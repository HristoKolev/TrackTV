(function () {
    'use strict';

    window.ngModules.filters.filter('monthName', [
        function monthName() {

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
}());