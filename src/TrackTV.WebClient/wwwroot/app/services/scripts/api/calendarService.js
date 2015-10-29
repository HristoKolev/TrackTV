(function () {
    'use strict';

    window.ngModules.services.factory('calendarService', [
        'apiPath', '$http', 'identity',
        function calendarService(apiPath, $http, identity) {

            var calendar = apiPath.service('calendar');

            function getConfig() {

                return {
                    headers: identity.getCurrentUser().addAuthorizationHeader()
                };
            }

            function getMonthModel(date) {

                return {
                    year: date.getFullYear(),
                    month: date.getMonth() + 1
                };
            }

            function setMonthLinks(date) {

                var info = {};

                info.thisMonth = getMonthModel(date);

                date.setMonth(date.getMonth() - 1);
                info.previosMonth = getMonthModel(date);

                date.setMonth(date.getMonth() + 2);
                info.nextMonth = getMonthModel(date);

                return info;
            }

            function processResponse(response) {

                var data = response.data;

                data.date = new Date(data.date);

                for (var i = 0; i < data.month.length; i += 1) {

                    for (var j = 0; j < data.month[i].length; j += 1) {

                        var day = data.month[i][j];
                        day.date = new Date(day.date);
                        data.month[i][j] = day;
                    }
                }

                data.info = setMonthLinks(data.date);

                return response;
            }

            // public

            function currentMonth() {

                return $http.get(calendar('/'), getConfig()).then(processResponse);
            }

            function getMonth(year, month) {

                return $http.get(calendar('/' + year + '/' + month), getConfig()).then(processResponse);
            }

            return {
                currentMonth: currentMonth,
                month: getMonth
            };
        }
    ]);

}());