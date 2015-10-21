(function () {
    'use strict';

    ngModules.main.controller('LoginController', [
        '$scope', '$location', 'toastr', 'identity', 'authentication',
        function LoginController ($scope, $location, toastr, identity, authentication) {

            $scope.identity = identity;

            function login (user, loginForm) {

                authentication.login(user).then(function (success) {

                    if (success) {

                        toastr.success('Successful login!');
                        $location.path('/');
                    } else {

                        toastr.error('Username/Password combination is not valid!');
                    }
                });
            }

            $scope.login = login;

            function logout () {

                authentication.logout().then(function () {

                    toastr.success('Successful logout!');

                    if ($scope.user) {

                        $scope.user.email = '';
                        $scope.user.username = '';
                        $scope.user.password = '';
                    }

                    $scope.loginForm.$setPristine();

                    $location.path('/');
                });
            }
        
            $scope.logout = logout;
        }
    ]);

})();