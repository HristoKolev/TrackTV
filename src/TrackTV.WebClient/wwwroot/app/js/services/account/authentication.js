app.factory('authentication', [
    '$http', '$q', 'identity', 'appOptions',
    function authentication($http, $q, identity, appOptions) {

        var apiUrl = appOptions.baseServiceUrl + '/api/account';

        var urls = {
            registerUrl : apiUrl + '/register',
            loginUrl: appOptions.baseServiceUrl + '/token',
            loguotUrl: apiUrl + '/logout'
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

            user['grant_type'] = 'password';

            var data = 'username=' + user.username + '&password=' + user.password + '&grant_type=password';

            var config = {
                headers : { 'Content-Type' : 'application/x-www-form-urlencoded' }
            };

            $http.post(urls.loginUrl, data, config)
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

            if (!identity.isAuthenticated()) {

                throw Error('');
            }

            var deferred = $q.defer();

            var headers = {
                'Authorization' : 'Bearer ' + identity.getCurrentUser()['access_token']
            };

            $http.post(urls.loguotUrl, {}, { headers: headers })
                .success(function () {

                    identity.removeCurrentUser();
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