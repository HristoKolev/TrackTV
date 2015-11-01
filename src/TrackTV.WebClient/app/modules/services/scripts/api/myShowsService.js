(function () {
    'use strict';

    window.ngModules.services.factory('myShowsService', [
        'apiPath', '$http', 'identity',
        function myShowsService(apiPath, $http, identity) {

            var myShows = apiPath.service('myshows');

            function getConfig() {

                return {
                    headers: identity.getCurrentUser().addAuthorizationHeader()
                };
            }

            function continuing(page) {

                page = page || 1;

                return $http.get(myShows('/continuing/' + page), getConfig());
            }

            function ended(page) {

                page = page || 1;

                return $http.get(myShows('/ended/' + page), getConfig());
            }

            return {
                continuing: continuing,
                ended: ended,
            };
        }
    ]);

}());