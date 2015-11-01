(function () {
    'use strict';

    window.ngModules.filters.filter('querify', [
        function querify() {

            function filter(str) {

                if (str) {
                    return str.replace(' ', '+');
                }
            }

            return filter;
        }
    ]);
}());