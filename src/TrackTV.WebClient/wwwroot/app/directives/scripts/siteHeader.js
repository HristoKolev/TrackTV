(function () {
    'use strict';

    ngModules.directives.directive('ttSiteHeader', [
        'templateProvider', 'authentication', 'toastr',
        function ttSiteHeader (templateProvider, authentication, toastr) {

            function notify () {
                toastr.success('Successful Logout!');
            }

            function logout () {
                authentication.logout().then(notify, notify);
            }

            return {
                restrict : 'A',
                templateUrl : templateProvider.directive('site-header'),
                scope : {
                    user : '='
                },
                link : function link (scope, element, attr) {
                    scope.logout = logout;
                }
            };
        }
    ]);

})();