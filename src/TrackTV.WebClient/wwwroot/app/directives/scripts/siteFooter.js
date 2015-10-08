(function () {
    'use strict';

    ngModules.directives.directive('ttSiteFooter', [
        'templateProvider',
        function ttSiteFooter (templateProvider) {

            return {
                restrict : 'A',
                templateUrl : templateProvider.directive('site-footer'),
                scope : {}
            };
        }
    ]);

})();