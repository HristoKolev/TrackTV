import {Component} from  'angular2/core';
import {Identity, Authentication} from  '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'header-component',
    templateUrl: 'header.component.html',
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