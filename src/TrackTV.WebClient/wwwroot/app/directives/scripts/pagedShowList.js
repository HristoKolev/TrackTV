(function () {
    'use strict';

    ngModules.directives.directive('ttPagedShowList', [
        'templateProvider',
        function ttPagedShowList (templateProvider) {

            function compile (element, attrs) {

                if (!attrs.totalItems) {

                    attrs.totalItems = 0;
                }

                if (!attrs.pageSize) {
                    attrs.pageSize = 1;
                }
                if (!attrs.items) {
                    attrs.items = [];
                }

                if (!attrs.currentPage) {
                    attrs.currentPage = 1;
                }

                return link;
            }

            function link (scope, element, attrs) {

                scope.pageChanged(scope.currentPage);

            }

            return {
                restrict : 'A',
                templateUrl : templateProvider.directive('paged-show-list'),
                scope : {
                    totalItems : '=',
                    pageSize : '=',
                    items : '=',
                    currentPage : '=',
                    pageChanged : '='
                },
                compile : compile
            };
        }
    ]);

})();