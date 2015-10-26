(function () {
    'use strict';

    ngModules.main.controller('GlobalController', [
        '$scope', 'identity', 'showService', 'templateLoader',
        function GlobalController($scope, identity, showService, templateLoader) {

            templateLoader.setScope($scope);

            $scope.user = identity.getCurrentUser();
            $scope.title = 'TrackTV';
        }
    ]);

})();