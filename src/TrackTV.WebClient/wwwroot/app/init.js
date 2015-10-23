(function () {
    'use strict';

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

    window.settings = window.settings || getSettings();

    window.ngModules = {};

})();