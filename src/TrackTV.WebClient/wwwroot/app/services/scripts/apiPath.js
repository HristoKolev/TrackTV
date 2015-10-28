(function () {
    'use strict';

    window.ngModules.services.factory('apiPath', [
        'baseServiceUrl',
        function apiPath(baseServiceUrl) {

            var baseApiPath = baseServiceUrl + '/api';

            function loginPath() {

                return baseServiceUrl + '/token';
            }

            function service(serviceName) {

                return function (path) {
                    return baseApiPath + '/' + serviceName + path;
                };
            }

            function path(name) {

                name = name || '';

                return baseServiceUrl + name;
            }

            function rawPath(name) {

                return baseApiPath + name;
            }

            return {
                loginPath : loginPath,
                service : service,
                path : path,
                rawPath : rawPath
            };
        }
    ]);

}());