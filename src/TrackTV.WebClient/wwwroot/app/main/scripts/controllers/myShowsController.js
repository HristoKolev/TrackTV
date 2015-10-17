(function () {
    'use strict';

    ngModules.main.controller('MyShowsController', [
        '$scope', 'myShowsService', '$routeParams', 'subscriptionService',
        function MyShowsController ($scope, myShowsService, $routeParams, subscriptionService) {

            $scope.pageSize = 10;

            function unsubscribe(id) {

                subscriptionService.unsubscribe(id).then(function (response) {
                    console.log(response);
                });
            }

            $scope.unsubscribe = unsubscribe;

            function subscribe(id) {

                subscriptionService.subscribe(id).then(function (response) {
                    console.log(response);
                });
            }

            $scope.subscribe = subscribe;

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