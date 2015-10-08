(function () {
    'use strict';

    ngModules.services.constant('templateProvider',
        function templateProvider () {

            var templateExtension = '.html';

            function view (name) {

                var viewPath = 'app/main/templates/';

                return viewPath + name + templateExtension;
            }

            function directive (name) {

                var directiveViewPath = 'app/directives/templates/';

                return directiveViewPath + name + templateExtension;
            }

            return {
                view : view,
                directive : directive
            };
        }());

})();