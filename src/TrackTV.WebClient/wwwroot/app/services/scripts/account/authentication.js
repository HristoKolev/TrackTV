(function () {
    'use strict';

    ngModules.services.factory('authentication', [
        '$http', '$q', 'identity', 'baseServiceUrl',
        function authentication($http, $q, identity, baseServiceUrl) {

            var apiUrl = baseServiceUrl + '/api/account';

            var urls = {
                registerUrl : apiUrl + '/register',
                loginUrl : baseServiceUrl + '/token',
                loguotUrl : apiUrl + '/logout'
            };

            function signup (user) {

                var deferred = $q.defer();

                $http.post(urls.registerUrl, user)
                    .success(function (response) {
                        deferred.resolve(response);
                    }, function (response) {
                        deferred.reject(response);
                    });

                return deferred.promise;
            }

            function login (user) {

                var deferred = $q.defer();

                user.grant_type = 'password';

                var data = 'username=' + user.username + '&password=' + user.password + '&grant_type=password';

                var config = {
                    headers : { 'Content-Type' : 'application/x-www-form-urlencoded' }
                };

                $http.post(urls.loginUrl, data, config)
                    .success(function (response) {
                        if (response.access_token) {

                            identity.setCurrentUser(response);

                            deferred.resolve(true);
                        } else {

                            deferred.resolve(response);
                        }
                    });

                return deferred.promise;
            }

            function logout () {

                var deferred = $q.defer();

                var headers = {
                    'Authorization' : 'Bearer ' + identity.getCurrentUser().full.access_token
                };

                $http.post(urls.loguotUrl, {}, { headers : headers })
                    .then(function success (response) {

                        identity.removeCurrentUser();
                        deferred.resolve(response);

                    }, function error (response) {

                        // removing the cookie, despite the server being unavailable
                        identity.removeCurrentUser();

                        console.log(response);
                        deferred.reject(response);
                    });

                return deferred.promise;
            }

            return {
                signup : signup,
                login : login,
                logout : logout
            };
        }
    ]);

})();