(function () {
    'use strict';

    ngModules.main.controller('GlobalController', [
        '$scope', 'appOptions', 'templateProvider', 'identity',
        function GlobalController ($scope, appOptions, templateProvider, identity) {

            $scope.user = identity.getCurrentUser();
            $scope.title = appOptions.title;
        }
    ]);

})();