app.directive('siteHeader', [
    'templateProvider', 'identity', 'auth',
    function siteHeader (templateProvider, identity, auth) {

        function link (scope, element, attr) {

            scope.isLoggedIn = identity.isAuthenticated;
            scope.isAdmin = identity.isAdmin;
            scope.user = identity.getCurrentUser;
            scope.logout = auth.logout;
        }

        return {
            restrict : 'A',
            templateUrl : templateProvider.directive('site-header'),
            scope : true,
            link : link
        };
    }
]);