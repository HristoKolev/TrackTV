import {Component} from  '@angular/core';
import {Router} from  '@angular/router';

import * as toastr from 'toastr';

import {Identity, Authentication} from  '../../services/index';

@Component({
    moduleId: module.id,
    selector: 'header-component',
    templateUrl: 'header.component.html'
})
export class HeaderComponent {

    constructor(private identity : Identity,
                private authentication : Authentication,
                private router : Router) {
    }

    private afterLogout() {

        toastr.success('Successful Logout!');

        this.router.navigate(['/shows']);
    }

    private logout($event : any) {

        $event.preventDefault();

        this.authentication.logout().subscribe(this.afterLogout.bind(this), this.afterLogout.bind(this));
    }
}