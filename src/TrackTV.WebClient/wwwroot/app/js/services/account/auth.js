app.factory('auth', [
    '$http', '$q', 'identity', 'authorization', 'appOptions',
    function ($http, $q, identity, authorization, appOptions) {

        var usersApi = appOptions.baseServiceUrl + '/api/account';
        var loginApi = appOptions.baseServiceUrl + '/token';

        function signup (user) {

            var deferred = $q.defer();

            $http.post(usersApi + '/register', user)
                .success(function (response) {

                    deferred.resolve(response);
                }, function (response) {

                    deferred.reject(response);
                });

            return deferred.promise;
        }

        function login (user) {

            var deferred = $q.defer();

            user['grant_type'] = 'password';

            $http.post(loginApi, 'username=' + user.username + '&password=' + user.password + '&grant_type=password', {
                    headers : { 'Content-Type' : 'application/x-www-form-urlencoded' }
                })
                .success(function (response) {

                    if (response['access_token']) {

                        identity.setCurrentUser(response);

                        deferred.resolve(true);
                    } else {

                        deferred.resolve(false);
                    }
                });

            return deferred.promise;
        }

        function logout () {

            var deferred = $q.defer();

            var headers = authorization.getAuthorizationHeader();

            $http.post(usersApi + '/logout', {}, { headers : headers })
                .success(function () {

                    identity.setCurrentUser(undefined);
                    deferred.resolve();
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