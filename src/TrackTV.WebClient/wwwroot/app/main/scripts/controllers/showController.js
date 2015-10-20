(function () {
    'use strict';

    ngModules.main.controller('ShowController', [
        '$scope', '$routeParams', 'showService', 'identity', 'subscriptionService',
        function ShowController ($scope, $routeParams, showService, identity, subscriptionService) {

            $scope.user = identity.getCurrentUser();

            showService.show($routeParams.show)
                .then(function (response) {

                    $scope.show = response.data;
                });

            function subscribe (id) {

                subscriptionService.subscribe(id)
                    .then(function (response) {

                        $scope.show.isUserSubscribed = true;
                    });
            }

            function unsubscribe (id) {

                subscriptionService.unsubscribe(id)
                    .then(function (response) {

                        $scope.show.isUserSubscribed = false;
                    });
            }

            $scope.subscribe = subscribe;
            $scope.unsubscribe = unsubscribe;
        }
    ]);
})();