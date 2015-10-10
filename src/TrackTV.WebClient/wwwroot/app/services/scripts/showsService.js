(function () {
    'use strict';

    ngModules.services.factory('showsService', [
        'apiPath', '$http',
        function showsService (apiPath, $http) {

            var shows = apiPath.service('shows');

            var baseUrl = apiPath.path();

            function addBaseUrl (elements, property) {

                for (var index in elements) {

                    var element = elements[index];

                    element[property] = baseUrl + element[property];

                    elements[index] = element;
                }

                return elements;
            }

            function top () {

                var request = $http.get(shows('/top'));

                request.then(function (response) {

                    var shows = response.data;

                    addBaseUrl(shows.running, 'poster');
                    addBaseUrl(shows.ended, 'poster');
                });

                return request;
            }

            return {
                top : top,
            };
        }
    ]);

})();