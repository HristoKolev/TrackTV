(function () {
    'use strict';

    window.ngModules.main.controller('ShowController', [
        '$scope', '$routeParams', 'showService', 'identity', 'subscriptionService', 'templateLoader',
        function ShowController($scope, $routeParams, showService, identity, subscriptionService, templateLoader) {

            showService.show($routeParams.show)
                .then(function (response) {

                    $scope.show = response.data;

                    templateLoader.ready();
                });

            // scope

            $scope.user = identity.getCurrentUser();

            function subscribe(id) {

                subscriptionService.subscribe(id)
                    .then(function (response) {

                        $scope.show.isUserSubscribed = true;
                        $scope.show.subscriberCount += 1;

                    });
            }

            $scope.subscribe = subscribe;

            function unsubscribe(id) {

                subscriptionService.unsubscribe(id)
                    .then(function (response) {

                        $scope.show.isUserSubscribed = false;
                        $scope.show.subscriberCount += 1;
                    });
            }

            $scope.unsubscribe = unsubscribe;
        }
    ]);
}());