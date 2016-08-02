(function () {
    'use strict';

    window.ngModules.main.controller('SearchController', [
        '$scope', 'showsService', '$routeParams', '$location', 'templateLoader',
        function SearchController($scope, showsService, $routeParams, $location, templateLoader) {

            // scope

            $scope.pageSize = 24;
            $scope.query = $routeParams.query;
            $scope.currentPage = $routeParams.page || 1;

            function gePtage(page) {

                showsService.search($routeParams.query, page)
                    .then(function (response) {

                        var data = response.data;

                        $scope.items = data.shows;
                        $scope.totalItems = data.count;

                        templateLoader.ready();
                    });

                if (page === 1) {
                    $location.search('page', null);
                } else {
                    $location.search('page', page);
                }
            }

            $scope.gePtage = gePtage;
        }
    ]);

}());