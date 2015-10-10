(function () {
    'use strict';

    ngModules.main.controller('ShowsController', [
        '$scope', 'showsService', '$routeParams',
        function ShowsController ($scope, showsService, $routeParams) {

            if ($routeParams.genre) {

                showsService.genre($routeParams.genre)
                    .then(function (response) {

                        $scope.shows = response.data;
                        $scope.ready = true;

                    });

            } else {

                showsService.top()
                    .then(function (response) {

                        $scope.shows = response.data;
                        $scope.ready = true;
                    });
            }

        }
    ]);

})();