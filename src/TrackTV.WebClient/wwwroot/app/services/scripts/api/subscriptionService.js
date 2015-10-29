(function () {
    'use strict';

    window.ngModules.services.factory('subscriptionService', [
        'apiPath', '$http', 'identity',
        function subscriptionService(apiPath, $http, identity) {

            var subscription = apiPath.service('subscription');

            function getConfig() {

                return {
                    headers: identity.getCurrentUser().addAuthorizationHeader()
                };
            }

            function subscribe(id) {

                return $http.post(subscription('/subscribe/' + id), {}, getConfig());
            }

            function unsubscribe(id) {
                return $http.post(subscription('/unsubscribe/' + id), {}, getConfig());
            }

            return {
                subscribe: subscribe,
                unsubscribe: unsubscribe
            };
        }
    ]);

}());