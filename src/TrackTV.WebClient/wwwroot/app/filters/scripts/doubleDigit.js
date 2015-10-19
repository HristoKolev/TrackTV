(function () {
    'use strict';

    ngModules.filters.filter('doubleDigit', [
        's',
        function doubleDigit (s) {

            function filter(number) {

                return s(number).lpad(2, '0').value();
            }

            return filter;
        }
    ]); 
})();