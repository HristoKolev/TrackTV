app.constant('templateProvider',
    function templateProvider() {

        var templateExtension = '.html';

        function view(name) {

            var viewPath = 'app/templates/views/';

            return viewPath + name + templateExtension;
        }

        function directiveView(name) {

            var directiveViewPath = 'app/templates/directives/';

            return directiveViewPath + name + templateExtension;
        }

        return {
            view : view,
            directiveView : directiveView
        };
    }());