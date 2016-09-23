import * as toastr from 'toastr';

export function configureToastr() : void {

    toastr.options.closeButton = true;
    toastr.options.positionClass = 'toast-bottom-left';
    toastr.options.timeOut = 2000;
}