(function () {
    'use strict';

    window.ngModules.directives.directive('ttShowList', [
        'templateProvider',
        function ttShowList(templateProvider) {

            return {
                restrict: 'A',
                templateUrl: templateProvider.directive('show-list'),
                scope: {
                    shows: '='
                }
            };
        }
    ]);

}());