(function () {
    'use strict';

    window.ngModules.main.controller('MyShowsController', [
        '$scope', 'myShowsService', '$routeParams', 'subscriptionService', 'toastr', 'templateLoader',
        function MyShowsController($scope, myShowsService, $routeParams, subscriptionService, toastr, templateLoader) {

            // scope

            $scope.pageSize = 10;

            function subscribe(show) {

                subscriptionService.subscribe(show.id)
                    .then(function (response) {

                        show.unsubscribed = false;
                    });
            }

            $scope.subscribe = subscribe;

            function unsubscribe(show) {

                subscriptionService.unsubscribe(show.id)
                    .then(function (response) {

                        show.unsubscribed = true;
                        toastr.success('You have successfully unsubscribed from ' + show.name);
                    });
            }

            $scope.unsubscribe = unsubscribe;

            function getContinuing(page) {

                myShowsService.continuing(page)
                    .then(function (response) {

                        $scope.continuing = response.data;
                        templateLoader.ready();
                    });
            }

            $scope.getContinuing = getContinuing;

            function getEnded(page) {

                myShowsService.ended(page)
                    .then(function (response) {

                        $scope.ended = response.data;
                    });
            }

            $scope.getEnded = getEnded;
        }
    ]);
}());