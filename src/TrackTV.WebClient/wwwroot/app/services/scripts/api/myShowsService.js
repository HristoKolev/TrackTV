(function () {
    'use strict';

    window.ngModules.services.factory('myShowsService', [
        'apiPath', '$http', 'identity',
        function myShowsService(apiPath, $http, identity) {

            var myShows = apiPath.service('myshows');

            var config = { headers : identity.getCurrentUser().addAuthorizationHeader() };

            function continuing(page) {

                page = page || 1;

                return $http.get(myShows('/continuing/' + page), config);
            }

            function ended(page) {

                page = page || 1;

                return $http.get(myShows('/ended/' + page), config);
            }

            return {
                continuing : continuing,
                ended : ended,
            };
        }
    ]);

}());