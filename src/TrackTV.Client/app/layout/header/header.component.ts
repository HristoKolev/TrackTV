import {Component} from  '@angular/core';
 
import * as toastr from 'toastr';

import {Identity, Authentication} from  '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'header-component',
    templateUrl: 'header.component.html'
})
export class HeaderComponent {

    constructor(public identity : Identity,
                private authentication : Authentication) {
    }

    private notify() {

        toastr.success('Successful Logout!');
    }

    public logout($event : any) {

        $event.preventDefault();

        this.authentication.logout().subscribe(this.notify, this.notify);
    }
}