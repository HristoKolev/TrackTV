app.controller('RegisterController', [
    '$scope', '$location', 'auth', 'toastr',
    function RegisterController ($scope, $location, auth, toastr) {

        function register (user) {

            auth.signup(user)
                .then(function (response) {

                    toastr.success('Registration successful!');
                    console.log(response);

                    //$location.path('/');

                }, function (response) {
                    console.log(response);
                    toastr.error('Registration failed!');
                });
        }

        $scope.register = register;
    }
]);