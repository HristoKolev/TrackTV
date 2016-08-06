import {Component} from  'angular2/core';
import {ROUTER_DIRECTIVES} from 'angular2/router';

import * as toastr from 'toastr';

import {Identity, Authentication} from  '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'header-component',
    templateUrl: 'header.component.html',
    directives: [ROUTER_DIRECTIVES]
})
export class HeaderComponent {

    constructor(public identity : Identity,
                private authentication : Authentication) {
    }

    private notify() {

        toastr.success('Successful Logout!');
    }

    public logout() {

        this.authentication.logout().subscribe(this.notify, this.notify);
    }
}