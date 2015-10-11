(function () {
    'use strict';

    ngModules.main.controller('ShowsController', [
        '$scope', 'showsService', '$routeParams',
        function ShowsController ($scope, showsService, $routeParams) {

            function loadData (response) {
                $scope.shows = response.data;
            }

            if ($routeParams.genre) {

                showsService.genre($routeParams.genre).then(loadData);
            } else {

                showsService.top().then(loadData);
            }
        }
    ]);

})();