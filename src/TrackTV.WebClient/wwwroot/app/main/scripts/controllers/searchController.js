(function () {
    'use strict';

    ngModules.main.controller('SearchController', [
        '$scope', 'showsService', '$routeParams',
        function SearchController ($scope, showsService, $routeParams) {

            $scope.count = 0;
            $scope.pageSize = 1;
            $scope.shows = [];
            $scope.query = $routeParams.query;

            $scope.pagination = {
                current : $routeParams.page || 1
            };

            $scope.getPage = function (page) {

                showsService.search($routeParams.query, page)
                    .then(function (response) {

                        var data = response.data;

                        $scope.shows = data.shows;
                        $scope.count = data.count;
                    });
            };

            $scope.getPage($scope.pagination.current);
        }
    ]);

})();