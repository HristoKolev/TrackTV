(function () {
    'use strict';

    ngModules.services.factory('calendarService', [
        'apiPath', '$http', 'identity',
        function calendarService (apiPath, $http, identity) {

            var calendar = apiPath.service('calendar');
            var user = identity.getCurrentUser();

            function getConfig () {
                return {
                    headers : user.addAuthorizationHeader()
                };
            }

            function getMonthModel (date) {

                return {
                    year : date.getFullYear(),
                    month : date.getMonth() + 1
                };
            }

            function setMonthLinks (date) {

                var info = {};

                info.thisMonth = getMonthModel(date);

                date.setMonth(date.getMonth() - 1);
                info.previosMonth = getMonthModel(date);

                date.setMonth(date.getMonth() + 2);
                info.nextMonth = getMonthModel(date);

                return info;
            }

            function processResponse (response) {

                var data = response.data;

                data.date = new Date(data.date);

                for (var i in data.month) {
                    for (var j in data.month[i]) {
                        var day = data.month[i][j];
                        day.date = new Date(day.date);
                    }
                }

                data.info = setMonthLinks(data.date);

                return response;
            }

            // public

            function currentMonth () {
                return $http.get(calendar('/'), getConfig()).then(processResponse);
            }

            function month (year, month) {
                return $http.get(calendar('/' + year + '/' + month), getConfig()).then(processResponse);
            }

            return {
                currentMonth : currentMonth,
                month : month
            };
        }
    ]);

})();