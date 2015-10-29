(function () {
    'use strict';

    window.ngModules.services.factory('authentication', [
        '$http', '$q', 'identity', 'apiPath', 'calendarService',
        function authentication($http, $q, identity, apiPath, calendarService) {

            var account = apiPath.service('account');

            function signup(user) {

                var deferred = $q.defer();

                $http.post(account('/register'), user)
                    .success(function (response) {
                        deferred.resolve(response);
                    }, function (response) {
                        deferred.reject(response);
                    });

                return deferred.promise;
            }

            function login(user) {

                var deferred = $q.defer();

                user.grant_type = 'password';

                var data = 'username=' + user.username + '&password=' + user.password + '&grant_type=password';

                var config = {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                };

                $http.post(apiPath.loginPath(), data, config)
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

            function logout() {

                var deferred = $q.defer();

                var user = identity.getCurrentUser();

                var headers = {};

                user.addAuthorizationHeader(headers);

                $http.post(account('/logout'), {}, { headers: headers })
                    .then(function success(response) {

                        identity.removeCurrentUser();
                        deferred.resolve(response);

                    }, function error(response) {

                        // removing the cookie, despite the server being unavailable
                        identity.removeCurrentUser();

                        deferred.reject(response);
                    });

                return deferred.promise;
            }

            return {
                signup: signup,
                login: login,
                logout: logout
            };
        }
    ]);

}());