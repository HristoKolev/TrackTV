(function () {
    'use strict';

    ngModules.main.controller('RegisterController', [
        '$scope', '$location', 'authentication', 'toastr',
        function RegisterController ($scope, $location, authentication, toastr) {

            function register (user) {

                authentication.signup(user)
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

})();