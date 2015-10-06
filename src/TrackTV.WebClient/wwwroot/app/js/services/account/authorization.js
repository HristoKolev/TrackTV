app.factory('authorization', [
    'identity',
    function (identity) {

        function getAuthorizationHeader () {
            return {
                'Authorization' : 'Bearer ' + identity.getCurrentUser()['access_token']
            };
        }

        return {
            getAuthorizationHeader : getAuthorizationHeader
        };
    }
]);