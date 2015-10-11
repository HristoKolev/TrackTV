(function () {
    'use strict';

    ngModules.directives.directive('ttGenrePanel', [
        'templateProvider',
        function ttGenrePanel(templateProvider) {

            return {
                restrict: 'A',
                templateUrl: templateProvider.directive('genre-panel'),
                scope: {
                    genres: '='
                },
            };
        }
    ]);

})();