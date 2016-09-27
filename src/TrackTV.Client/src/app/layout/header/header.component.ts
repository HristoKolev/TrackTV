import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {Identity} from '../../identity/identity.service';
import {Authentication} from '../../identity/authentication.service';
import * as toastr from 'toastr';

@Component({
    moduleId: module.id,
    selector: 'header-component',
    templateUrl: 'header.component.html',
    styleUrls: ['header.component.css']
})
export class HeaderComponent {

    constructor(public identity : Identity,
                private authentication : Authentication,
                private router : Router) {
    }

    public query : string;

    public logout($event : Event) {

        $event.preventDefault();

        this.authentication.logout()
            .subscribe(() => this.afterLogout(), () => this.afterLogout());
    }

    private afterLogout() {

        toastr.success('Successful Logout!');

        this.router.navigate(['/shows']);
    }

}