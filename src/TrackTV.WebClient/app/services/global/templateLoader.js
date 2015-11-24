(function () {
    'use strict';

    window.ngModules.services.factory('templateLoader', [
        function templateLoader() {

            var states = {
                ready: 'LOAD_STATE_READY',
                loading: 'LOAD_STATE_LOADING',
            };

            var globalScope;

            function setState(state) {

                globalScope.loadState = state;
            }

            // public

            function setScope(scope) {

                scope.$on('$locationChangeStart', function (event) {

                    setState(states.loading);
                });

                scope.states = states;

                globalScope = scope;
            }

            function ready() {
                setState(states.ready);
            }

            return {
                setScope: setScope,
                ready: ready
            };
        }
    ]);

}());