app.directive('siteHeader', [
    'templateProvider', 'identity', 'authentication',
    function siteHeader(templateProvider, identity, authentication) {

        function link (scope, element, attr) {

            scope.isLoggedIn = identity.isAuthenticated;
            scope.isAdmin = identity.isAdmin;
            scope.user = identity.getCurrentUser;
            scope.logout = authentication.logout;
        }

        return {
            restrict : 'A',
            templateUrl : templateProvider.directive('site-header'),
            scope : true,
            link : link
        };
    }
]);