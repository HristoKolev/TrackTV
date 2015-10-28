(function () {
    'use strict';

    window.toastr.options = {
        closeButton : true,
        positionClass : 'toast-top-center',
        timeOut : 2000,
    };

    window.ngModules.main
        .value('toastr', window.toastr)
        .value('s', window.s);

}());