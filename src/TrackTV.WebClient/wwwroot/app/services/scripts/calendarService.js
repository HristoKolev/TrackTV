(function () {
    'use strict';

    ngModules.services.factory('calendarService', [
        'apiPath', '$http', 'identity',
        function calendarService (apiPath, $http, identity) {

            var calendar = apiPath.service('calendar');
            var user = identity.getCurrentUser();

            var config = {
                headers : user.addAuthorizationHeader()
            };

            // public

            function currentMonth () {
                return $http.get(calendar('/'), config).then(processResponse);
            }

            function month (year, month) {
                return $http.get(calendar('/' + year + '/' + month), config).then(processResponse);
            }

            // private

            function processResponse (response) {
                var data = response.data;

                data.date = new Date(data.date);

                for (var i in data.month) {
                    for (var j in data.month[i]) {
                        var day = data.month[i][j];
                        day.date = new Date(day.date);
                    }
                }

                return response;
            }

            return {
                currentMonth : currentMonth,
                month : month
            };
        }
    ]);

})();