app.factory('templateProvider',
    function templateProvider () {

        var viewPath = 'app/views/';
        var directiveViewPath = 'app/directives/views/';

        var templateExtension = '.html';

        function view (name) {
            return viewPath + name + templateExtension;
        }

        function directiveView (name) {
            return directiveViewPath + name + templateExtension;
        }

        return {
            view : view,
            directiveView : directiveView
        };
    });