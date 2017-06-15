import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Identity } from '../../identity/identity.service';
import { Authentication } from '../../identity/authentication.service';
import * as toastr from 'toastr';

@Component({
    moduleId: module.id,
    selector: 'site-header',
    templateUrl: 'site-header.component.html',
    styleUrls: ['site-header.component.css'],
})
export class HeaderComponent {

    public query: string;

    constructor(public identity: Identity,
                private authentication: Authentication,
                private router: Router) {
    }

    public logout($event: Event) {

        $event.preventDefault();

        this.authentication.logout();

        toastr.success('Successful Logout!');

    }

}
