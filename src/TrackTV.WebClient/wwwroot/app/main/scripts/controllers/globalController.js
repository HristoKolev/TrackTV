(function () {
    'use strict';

    ngModules.main.controller('GlobalController', [
        '$scope', 'identity',
        function GlobalController ($scope, identity) {

            $scope.user = identity.getCurrentUser();
            $scope.title = 'TrackTV';
        }
    ]);

})();