(function () {
    'use strict';

    ngModules.main.controller('RegisterController', [
        '$scope', '$location', 'authentication', 'toastr', 'templateLoader',
        function RegisterController($scope, $location, authentication, toastr, templateLoader) {

            function register (user) {

                authentication.signup(user)
                    .then(function (response) {

                        toastr.success('Registration successful!');

                        $location.path('/');

                    }, function (response) {

                        toastr.error('Registration failed!');
                    });
            }

            $scope.register = register;

            templateLoader.ready();
        }
    ]);

})();