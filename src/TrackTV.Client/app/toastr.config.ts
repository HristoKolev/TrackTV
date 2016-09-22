import * as toastr from 'toastr';

export function configureToastr() {

    toastr.options = {
        closeButton: true,
        positionClass: 'toast-bottom-left',
        timeOut: 2000
    };
}