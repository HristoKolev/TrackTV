(function () {
    'use strict';

    ngModules.main.controller('ShowsController', [
        '$scope', 'showsService',
        function ShowsController ($scope, showsService) {

            showsService.top()
                .then(function (response) {

                    $scope.shows = response.data;

                });
        }
    ]);

})();