app.constant('templateProvider',
    function templateProvider() {

        var templateExtension = '.html';

        function view(name) {

            var viewPath = 'app/templates/views/';

            return viewPath + name + templateExtension;
        }

        function directive(name) {

            var directiveViewPath = 'app/templates/directives/';

            return directiveViewPath + name + templateExtension;
        }

        return {
            view : view,
            directive: directive
        };
    }());