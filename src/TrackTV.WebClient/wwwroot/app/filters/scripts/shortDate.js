(function () {
    'use strict';

    ngModules.filters.filter('shortDate', [
        's',
        function shortDate (s) {

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

            function filter (date) {

                var monthName = monthNames[date.getMonth()].substr(0, 3);

                var formattedDate = monthName + '. ' + s(date.getDate()).lpad(2, '0').value();

                return formattedDate;
            }

            return filter;
        }
    ]);
})();