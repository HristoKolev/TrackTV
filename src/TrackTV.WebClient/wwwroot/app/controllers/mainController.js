'use strict';

app.controller('MainController',
    function MainController ($scope, appOptions, templateProvider) {

        $scope.title = appOptions.title;
    });