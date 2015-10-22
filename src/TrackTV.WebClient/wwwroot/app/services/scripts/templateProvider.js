(function () {
    'use strict';

    ngModules.services.constant('templateProvider',
        function templateProvider () {

            function getSettings () {

                var settings;

                $.ajax({
                    type : 'GET',
                    url : 'app/settings.json',
                    dataType : 'json',
                    success : function (data) {
                        settings = data;

                    },

                    async : false
                });

                return settings;
            }

            var settings = window.settings || getSettings();
            var templates = settings.templates;

            function view (name) {

                return templates.viewPath + '/' + name + templates.extension;
            }

            function directive (name) {

                return templates.directivePath + '/' + name + templates.extension;
            }

            function lib (name) {

                return templates.libPath + '/' + name + templates.extension;
            }

            return {
                view : view,
                directive : directive,
                lib : lib
            };
        }());

})();