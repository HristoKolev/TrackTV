(function () {
    'use strict';

    ngModules.services.factory('showsService', [
        'apiPath', '$http',
        function showsService (apiPath, $http) {

            //variables

            var shows = apiPath.service('shows');
            var baseUrl = apiPath.path();

            //public

            function top () {

                var request = $http.get(shows('/top'));

                request.then(function (response) {

                    var data = response.data;

                    addBaseUrl(data.running, 'poster');
                    addBaseUrl(data.ended, 'poster');
                });

                return request;
            }

            function genre (name) {

                var request = $http.get(shows('/genre/' + name));

                request.then(function (response) {

                    var data = response.data;

                    addBaseUrl(data.running, 'poster');
                    addBaseUrl(data.ended, 'poster');
                });

                return request;
            }

            function search (query, page) {
                page = page || 1;

                var request = $http.get(shows('/search/' + query + '/' + page));

                request.then(function (response) {

                    var data = response.data;

                    addBaseUrl(data.shows, 'poster');
                });

                return request;
            }

            // private

            function addBaseUrl (elements, property) {

                for (var index in elements) {

                    var element = elements[index];

                    element[property] = baseUrl + element[property];

                    elements[index] = element;
                }

                return elements;
            }

            return {
                top : top,
                genre : genre,
                search
            };
        }
    ]);

})();