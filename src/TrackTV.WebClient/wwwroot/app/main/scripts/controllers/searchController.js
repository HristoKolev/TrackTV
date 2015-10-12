(function () {
    'use strict';

    ngModules.main.controller('SearchController', [
        '$scope', 'showsService', '$routeParams', '$location',
        function SearchController($scope, showsService, $routeParams, $location) {

            $scope.pageSize = 24;
            $scope.query = $routeParams.query;
            $scope.currentPage = $routeParams.page || 1;

            $scope.gePtage = function (page) {

                showsService.search($routeParams.query, page)
                    .then(function (response) {

                        var data = response.data;

                        $scope.items = data.shows;
                        $scope.totalItems = data.count;
                    });

                if (page === 1) {
                    $location.search('page', null);
                } else {
                    $location.search('page', page);
                }
            };
        }
    ]);

})();