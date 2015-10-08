(function () {
    'use strict';

    ngModules.main.controller('GlobalController', [
        '$scope', 'templateProvider', 'identity',
        function GlobalController ($scope, templateProvider, identity) {

            $scope.user = identity.getCurrentUser();
            $scope.title = 'TrackTV';
        }
    ]);

})();