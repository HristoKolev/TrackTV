(function () {
    'use strict';

    ngModules.filters.filter('premieredDate', [
        'moment',
        function premieredDate (moment) {

            function filter (date) {

                if (date instanceof String) {

                    date = new Date(date);
                }

                return moment(date).format('MMM [ \']DD');
            }

            return filter;
        }
    ]);
})();