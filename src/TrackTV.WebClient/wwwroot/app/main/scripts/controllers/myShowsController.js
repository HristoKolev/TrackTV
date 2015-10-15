(function () {
    'use strict';

    ngModules.main.controller('MyShowsController', [
        '$scope', 'myShowsService', '$routeParams',
        function MyShowsController ($scope, myShowsService, $routeParams) {

            window.unsub = myShowsService.unsubscribe;

            $scope.pageSize = 10;

            function unsubscribe (id) {

            }

            $scope.unsubscribe = unsubscribe;

            function getContinuing (page) {

                myShowsService.continuing(page).then(function (response) {
                    $scope.continuing = response.data;
                });
            }

            $scope.getContinuing = getContinuing;

            function getEnded (page) {

                myShowsService.ended(page).then(function (response) {
                    $scope.ended = response.data;
                });
            }

            $scope.getEnded = getEnded;
        }
    ]);
})();