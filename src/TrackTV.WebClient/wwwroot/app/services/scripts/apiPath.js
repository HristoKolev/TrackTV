(function () {
    'use strict';

    ngModules.services.factory('apiPath', [
        'baseServiceUrl',
        function apiPath (baseServiceUrl) {

            var instances = [];

            var baseApiPath = baseServiceUrl + '/api';

            function loginPath () {
                return baseServiceUrl + '/token';
            }

            function service (serviceName) {

                return function (path) {
                    return baseApiPath + '/' + serviceName + path;
                };
            }

            function path(name) {
                name = name || '';

                return baseServiceUrl + name;
            }

            function apiPath (name) {
                return baseApiPath + name;
            }

            return {
                loginPath : loginPath,
                service : service,
                path : path,
                apiPath : apiPath
            };
        }
    ]);

})();