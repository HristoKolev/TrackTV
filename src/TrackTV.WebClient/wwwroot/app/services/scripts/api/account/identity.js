(function () {
    'use strict';

    window.ngModules.services.factory('identity', [
        '$cookieStore',
        function ($cookieStore) {

            var userKey = 'currentUser';

            var localUser = {};

            // private
            function updateLocalUser(user) {

                user = user || getCookieUser() || {};

                localUser.isAuthenticated = !!isAuthenticated();
                localUser.isAdmin = !!isAdmin();
                localUser.isGuest = !user.access_token;
                localUser.username = user.userName;
                localUser.addAuthorizationHeader = addAuthorizationHeader;

                if (localUser.isGuest) {
                    localUser.username = 'Guest';
                }

                localUser.full = user;
            }

            function addAuthorizationHeader(headers) {

                headers = headers || {};

                // !!! capital A
                headers.Authorization = 'Bearer ' + localUser.full.access_token;

                return headers;
            }

            function clearLocalUser() {

                updateLocalUser({});
            }

            function getCookieUser() {

                return $cookieStore.get(userKey);
            }

            // public
            function getCurrentUser() {

                updateLocalUser();
                return localUser;
            }

            function setCurrentUser(user) {

                if (!user) {
                    throw Error('The provided user is empty.');
                }

                $cookieStore.put(userKey, user);

                updateLocalUser(user);
            }

            function removeCurrentUser() {

                if (!isAuthenticated()) {

                    throw Error('There currently is no authorized user.');
                }

                $cookieStore.remove(userKey);

                clearLocalUser();
            }

            function isAuthenticated() {

                return !!getCookieUser();
            }

            function isAdmin() {

                if (isAuthenticated()) {

                    var isInAdminRole = getCookieUser().isInAdminRole;

                    switch (isInAdminRole) {
                    case 'True':
                        return true;
                    case 'False':
                        return false;
                    default:
                        throw Error('The property "isInAdminRole" is not present or has no valid value. Value: ' + isInAdminRole);
                    }
                } else {
                    return false;
                }
            }

            clearLocalUser();

            return {
                getCurrentUser : getCurrentUser,
                setCurrentUser : setCurrentUser,
                removeCurrentUser : removeCurrentUser,
                isAuthenticated : isAuthenticated,
                isAdmin : isAdmin
            };
        }
    ]);

}());