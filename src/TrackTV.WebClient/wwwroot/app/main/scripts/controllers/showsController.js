(function () {
    'use strict';

    ngModules.main.controller('ShowsController', [
        '$scope', 'showsService', '$routeParams', 'templateLoader',
        function ShowsController($scope, showsService, $routeParams, templateLoader) {

            function processResponse (response) {
                $scope.shows = response.data;

                templateLoader.ready();
            }

            if ($routeParams.genre) {

                showsService.genre($routeParams.genre).then(processResponse);
            } else {

                showsService.top().then(processResponse);
            }
        }
    ]);

})();