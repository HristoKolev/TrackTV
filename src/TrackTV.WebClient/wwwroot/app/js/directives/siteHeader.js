app.directive('siteHeader', [
    'templateProvider',
    function siteHeader(templateProvider) {

        return {
            restrict : 'A',
            templateUrl : templateProvider.directive('site-header'),
            scope : {}
        };
    }
]);