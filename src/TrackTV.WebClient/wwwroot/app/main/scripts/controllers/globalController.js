(function () {
    'use strict';

    ngModules.main.controller('GlobalController', [
        '$scope', 'identity', 'showService',
        function GlobalController($scope, identity, showService) {

            $scope.user = identity.getCurrentUser();
            $scope.title = 'TrackTV';
        }
    ]);

})();