(function () {
    'use strict';

    ngModules.directives.directive('ttSiteHeader', [
        'templateProvider', 'authentication', 'toastr', '$location',
        function ttSiteHeader (templateProvider, authentication, toastr, $location) {

            function notify () {
                toastr.success('Successful Logout!');
            }

            function logout () {
                authentication.logout().then(notify, notify);
            }

            function search(query) {
                $location.path('/shows/search/' + query);
            }

            return {
                restrict : 'A',
                templateUrl : templateProvider.directive('site-header'),
                scope : {
                    user : '='
                },
                link : function link (scope, element, attr) {
                    scope.logout = logout;
                    scope.search = search;
                }
            };
        }
    ]);

})();