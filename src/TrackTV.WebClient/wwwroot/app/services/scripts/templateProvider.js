﻿(function () {
    'use strict';

    ngModules.services.constant('templateProvider',
        function templateProvider () {

            var templates = settings.templates;

            function getTemplate (name, prefix) {

                var isCached = templates.cached;

                var result = name + templates.extension;

                if (!isCached) {
                    result = prefix + result;
                }

                return result;
            }

            function view (name) {

                var path = templates.viewPath + '/';

                return getTemplate(name, path);
            }

            function directive (name) {

                var path = templates.directivePath + '/';

                return getTemplate(name, path);
            }

            function lib (name) {

                var path = templates.libPath + '/';

                return getTemplate(name, path);
            }

            return {
                view : view,
                directive : directive,
                lib : lib
            };
        }());
})();