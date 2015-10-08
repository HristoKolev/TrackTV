(function () {
    'use strict';

    ngModules.services.constant('appOptions',
        function appOptions () {

            return {
                title : 'hello world',
                baseServiceUrl : 'http://localhost:5050'
            };
        }());

})();