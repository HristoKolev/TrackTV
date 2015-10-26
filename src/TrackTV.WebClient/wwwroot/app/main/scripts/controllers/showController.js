(function () {
    'use strict';

    ngModules.main.controller('ShowController', [
        '$scope', '$routeParams', 'showService', 'identity', 'subscriptionService',
        function ShowController ($scope, $routeParams, showService, identity, subscriptionService) {

            showService.show($routeParams.show)
              .then(function (response) {

                  $scope.show = response.data;
              });


            // scope

            $scope.user = identity.getCurrentUser();

            function subscribe (id) {

                subscriptionService.subscribe(id)
                    .then(function (response) {

                        $scope.show.isUserSubscribed = true;
                        $scope.show.subscriberCount++;

                    });
            }

            $scope.subscribe = subscribe;

            function unsubscribe (id) {

                subscriptionService.unsubscribe(id)
                    .then(function (response) {

                        $scope.show.isUserSubscribed = false;
                        $scope.show.subscriberCount--;
                    });
            }
           
            $scope.unsubscribe = unsubscribe;
        }
    ]);
})();