app.controller('LoginController', [
    '$scope', '$location', 'toastr', 'identity', 'auth',
    function LoginController($scope, $location, toastr, identity, auth) {

        $scope.identity = identity;

        function login(user, loginForm) {

            auth.login(user).then(function (success) {

                if (success) {

                    toastr.success('Successful login!');
                    $location.path('/');
                }
                else {

                    toastr.error('Username/Password combination is not valid!');
                }
            });
        }

        function logout() {

            auth.logout().then(function () {

                toastr.success('Successful logout!');

                if ($scope.user) {

                    $scope.user.email = '';
                    $scope.user.username = '';
                    $scope.user.password = '';
                }

                $scope.loginForm.$setPristine();

                $location.path('/');
            })
        }

        $scope.login = login;
        $scope.logout = logout;
    }
]);