app.controller('GlobalController', [
    '$scope', 'appOptions', 'templateProvider',
    function GlobalController($scope, appOptions, templateProvider) {

        $scope.title = appOptions.title;
    }
]);