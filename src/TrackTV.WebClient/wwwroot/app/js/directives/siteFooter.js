app.directive('siteFooter', [
    'templateProvider',
    function siteFooter(templateProvider) {

        return {
            restrict : 'A',
            templateUrl : templateProvider.directive('site-footer'),
            scope : {}
        };
    }
]);