(function () {
    'use strict';

    ngModules.directives.directive('ttShortShow', [
        'templateProvider',
        function ttShortShow(templateProvider) {

            return {
                restrict : 'A',
                templateUrl : templateProvider.directive('short-show'),
                scope : {
                    show : '='
                },
            };
        }
    ]);

})();