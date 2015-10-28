(function () {
    'use strict';

    window.ngModules.directives.directive('ttPagedMyShowsList', [
        'templateProvider',
        function ttPagedMyShowsList(templateProvider) {

            /*jslint unparam:true */
            function link(scope, element, attrs) {

                scope.pageChanged(scope.currentPage);
            }

            function compile(element, attrs) {

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

            return {
                restrict : 'A',
                templateUrl : templateProvider.directive('paged-my-shows-list'),
                scope : {
                    totalItems : '=',
                    pageSize : '=',
                    items : '=',
                    currentPage : '=',
                    pageChanged : '=',
                    unsubscribe : '=',
                    subscribe : '=',
                    showNextEpisode : '=',
                    paginationId : '@'
                },
                compile : compile
            };
        }
    ]);

}());