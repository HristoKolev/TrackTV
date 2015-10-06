app.controller('MainController', [
    '$scope', 'appOptions', 'templateProvider',
    function MainController ($scope, appOptions, templateProvider) {

        $scope.title = appOptions.title;
    }
]);