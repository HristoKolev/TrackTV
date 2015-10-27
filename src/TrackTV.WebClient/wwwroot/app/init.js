(function () {
    'use strict';

    function getSettings () {

        function syncRequest (url) {

            var result;

            $.ajax({
                type : 'GET',
                url : url,
                dataType : 'json',
                success : function (data) {
                    result = data;
                },

                async : false
            });

            return result;
        }

        var settings = {
            templateConfig : syncRequest('app/templateConfig.json'),
            development : true
        };

        return settings;
    }

    window.settings = window.settings || getSettings();

    window.ngModules = {};
})();