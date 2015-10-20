(function () {
    'use strict';

    toastr.options = {
        closeButton : true,
        positionClass : 'toast-top-center',
        timeOut : 2000,
    };

    ngModules.main
        .value('toastr', window.toastr)
        .value('s', window.s);

})();