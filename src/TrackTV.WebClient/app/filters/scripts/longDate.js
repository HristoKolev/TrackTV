(function () {
    'use strict';

    window.ngModules.filters.filter('longDate', [
        function longDate() {

            function filter(date) {

                if (!(date instanceof Date)) {

                    date = new Date(date);
                }

                var options = { weekday: 'long', year: 'numeric', month: 'long', day: '2-digit' };

                return date.toLocaleDateString('en-US', options);
            }

            return filter;
        }
    ]);
}());