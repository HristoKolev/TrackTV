(function () {
    'use strict';

    ngModules.directives.directive('ttMyEpisode', [
        'templateProvider',
        function ttMyEpisode(templateProvider) {

            return {
                restrict: 'A',
                templateUrl: templateProvider.directive('my-episode'),
                scope: {
                    episode: '='
                },
            };
        }
    ]);

})();