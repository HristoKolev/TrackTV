(function () {
    'use strict';

    ngModules.main.controller('MyShowsController', [
        '$scope', 'myShowsService', '$routeParams', 'subscriptionService', 'toastr',
        function MyShowsController ($scope, myShowsService, $routeParams, subscriptionService) {

            $scope.pageSize = 10;

            function subscribe (show) {

                subscriptionService.subscribe(show.id)
                    .then(function (response) {

                        show.unsubscribed = false;
                    });
            }

            function unsubscribe (show) {

                subscriptionService.unsubscribe(show.id)
                    .then(function (response) {

                        show.unsubscribed = true;
                        toastr.success('You have successfully unsubscribed from ' + show.name);
                    });
            }

            function getContinuing (page) {

                myShowsService.continuing(page)
                    .then(function (response) {

                        $scope.continuing = response.data;
                    });
            }

            function getEnded (page) {

                myShowsService.ended(page)
                    .then(function (response) {

                        $scope.ended = response.data;
                    });
            }

            $scope.unsubscribe = unsubscribe;
            $scope.subscribe = subscribe;
            $scope.getContinuing = getContinuing;
            $scope.getEnded = getEnded;
        }
    ]);
})();