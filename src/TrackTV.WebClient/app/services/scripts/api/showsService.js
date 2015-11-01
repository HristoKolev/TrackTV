(function () {
    'use strict';

    window.ngModules.services.factory('showsService', [
        'apiPath', '$http',
        function showsService(apiPath, $http) {

            //variables

            var shows = apiPath.service('shows');
            var baseUrl = apiPath.path();

            //public

            function top() {

                var request = $http.get(shows('/top'));

                request.then(function (response) {

                    var data = response.data;

                    addBaseUrl(data.running, 'poster');
                    addBaseUrl(data.ended, 'poster');
                });

                return request;
            }

            function genre(name) {

                var request = $http.get(shows('/genre/' + name));

                request.then(function (response) {

                    var data = response.data;

                    addBaseUrl(data.running, 'poster');
                    addBaseUrl(data.ended, 'poster');
                });

                return request;
            }

            function search(query, page) {
                page = page || 1;

                var request = $http.get(shows('/search/' + query + '/' + page));

                request.then(function (response) {

                    var data = response.data;

                    addBaseUrl(data.shows, 'poster');
                });

                return request;
            }

            function network(networkName, page) {
                page = page || 1;

                var request = $http.get(shows('/network/' + networkName + '/' + page));

                request.then(function (response) {

                    var data = response.data;

                    addBaseUrl(data.shows, 'poster');
                });

                return request;
            }

            // private

            function addBaseUrl(elements, property) {

                for (var i = 0; i < elements.length; i += 1) {

                    var element = elements[i];

                    element[property] = baseUrl + element[property];

                    elements[i] = element;
                }

                return elements;
            }

            return {
                top: top,
                genre: genre,
                search: search,
                network: network
            };
        }
    ]);

}());