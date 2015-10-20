(function () {
    'use strict';

    ngModules.main.controller('NetworkController', [
        '$scope', 'showsService', '$routeParams', '$location',
        function NetworkController($scope, showsService, $routeParams, $location) {

            $scope.pageSize = 24;
            $scope.currentPage = $routeParams.page || 1;

            $scope.gePtage = function (page) {

                showsService.network($routeParams.network, page)
                    .then(function (response) {

                        var data = response.data;

                        $scope.items = data.shows;
                        $scope.totalItems = data.count;
                        $scope.network = data.networkName;
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