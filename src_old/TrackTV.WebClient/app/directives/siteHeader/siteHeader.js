(function () {
    'use strict';

    window.ngModules.directives.directive('ttSiteHeader', [
        'templateProvider', 'authentication', 'toastr', '$location',
        function ttSiteHeader(templateProvider, authentication, toastr, $location) {

            function notify() {

                toastr.success('Successful Logout!');
            }

            function logout() {

                authentication.logout().then(notify, notify);
            }

            function search(query) {

                if (query) {

                    $location.path('/shows/search/' + query);
                }
            }

            /*jslint unparam:true */
            function link(scope, element, attr) {

                scope.logout = logout;
                scope.search = search;
            }

            return {
                restrict: 'A',
                templateUrl: templateProvider.directive('site-header'),
                scope: {
                    user: '='
                },
                link: link
            };
        }
    ]);

}());