(function () {
    'use strict';

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-top-center',
        timeOut: 2000,
    };

    ngModules.services.value('toastr', toastr);
})();