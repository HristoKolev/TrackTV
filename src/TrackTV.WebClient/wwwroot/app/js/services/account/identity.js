app.factory('identity', [
    '$cookieStore',
    function ($cookieStore) {

        var key = 'currentUser';

        function getCurrentUser() {

            var user = $cookieStore.get(key)

            if (!user) {
                throw Error('The user is not authenticated.');
            }

            return user;
        }

        function setCurrentUser(user) {

            if (!user) {
                throw Error('The provided user is empty.');
            }

            $cookieStore.put(key, user);
        }

        function removeCurrentUser() {

            if (!$cookieStore.get(key)) {

                throw Error('There currently is no authorized user.');
            }

            $cookieStore.remove(key);
        }

        function isAuthenticated() {
            
            return !!$cookieStore.get(key);
        }

        function isAdmin() {

            if (isAuthenticated()) {

                var isInAdminRole = getCurrentUser().isInAdminRole;

                switch (isInAdminRole) {
                    case 'True':
                        return true;
                    case 'False':
                        return false;
                    default:
                        throw Error('The property "isInAdminRole" is not present or has no valid value. Value: ' + isInAdminRole)
                }
            } else {
                return false;
            }
        }

        return {
            getCurrentUser: getCurrentUser,
            setCurrentUser: setCurrentUser,
            removeCurrentUser: removeCurrentUser,
            isAuthenticated: isAuthenticated,
            isAdmin: isAdmin
        };
    }
]);