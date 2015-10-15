(function () {
    'use strict';

    ngModules.filters.filter('doubleDigit', [
        '_s',
        function doubleDigit (_s) {

            function filter(number) {

                return _s(number).lpad(2, '0').value();
            }

            return filter;
        }
    ]); 
})();