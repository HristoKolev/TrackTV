(function () {
    'use strict';

    window.ngModules.services.factory('showService', [
        'apiPath', '$http', 'identity',
        function showService(apiPath, $http, identity) {

            //variables

            var show = apiPath.service('show');
            var baseUrl = apiPath.path();
            var config = { headers : identity.getCurrentUser().addAuthorizationHeader() };

            //public

            function getShow(name) {
                return $http.get(show('/' + name), config).then(processResponse);
            }

            // private 

            function processResponse(response) {

                var data = response.data;

                if (data.banner) {

                    data.banner = baseUrl + data.banner;
                }

                if (data.firstAired) {

                    data.firstAired = new Date(data.firstAired);
                }

                return response;
            }

            return {
                show : getShow,
            };
        }
    ]);

}());