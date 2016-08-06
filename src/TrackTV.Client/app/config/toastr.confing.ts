import * as toastr from 'toastr';

export function configureToastr() {

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-top-center',
        timeOut: 2000
    };
}