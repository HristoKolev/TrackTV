(function () {
    'use strict';

    ngModules.services.factory('subscriptionService', [
        'apiPath', '$http', 'identity',
        function subscriptionService (apiPath, $http, identity) {

            var subscription = apiPath.service('subscription');

            var config = { headers : identity.getCurrentUser().addAuthorizationHeader() };

            function subscribe(id) {

                return $http.post(subscription('/subscribe/' + id), {}, config);
            }

            function unsubscribe (id) {
                return $http.post(subscription('/unsubscribe/' + id), {}, config);
            }

            return {
                subscribe : subscribe,
                unsubscribe : unsubscribe
            };
        }
    ]);

})();