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

            function fixBaseUrl (request) {

                request.then(function (response) {

                    var shows = response.data;

                    addBaseUrl(shows.running, 'poster');
                    addBaseUrl(shows.ended, 'poster');
                });

                return request;
            }

            function top () {

                return fixBaseUrl($http.get(shows('/top')));
            }

            function genre (name) {

                return fixBaseUrl($http.get(shows('/genre/' + name)));

            }

            return {
                top : top,
                genre : genre
            };
        }
    ]);

})();