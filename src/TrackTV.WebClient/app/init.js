(function () {
    'use strict';

    window.ngModules = {};

    function syncRequest(url) {

        var result;

        $.ajax({
            type: 'GET',
            url: url,
            dataType: 'json',
            success: function (data) {
                result = data;
            },

            async: false
        });

        return result;
    }

}());