import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import * as toastr from 'toastr';
import { Authentication } from '../../identity/authentication.service';
import { LoginError, LoginUser } from '../../identity/authentication.models';

@Component({
    moduleId: module.id,
    templateUrl: 'login.component.html',
})
export class LoginComponent implements OnInit {

    public loginForm: FormGroup;

    constructor(private router: Router,
                private authentication: Authentication) {
    }

    public login(): void {

        this.authentication.login(this.loginForm.getRawValue() as LoginUser)
            .subscribe(() => this.onSuccessfulLogin(), (error: LoginError) => this.notifyLoginError(error));
    }

    public ngOnInit(): void {

        this.createForm();
    }

    private createForm(): void {

        this.loginForm = new FormGroup({

            username: new FormControl(null, [Validators.required, Validators.minLength(6)]),
            password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
        });
    }

    private notifyLoginError(error: LoginError): void {

        let message: string;

        if (error === LoginError.InvalidCredentials) {

            message = 'Invalid username or password!';
        } else {

            message = 'Server error! Try again in a few minutes.';
        }

        toastr.error(message);
    }

    private onSuccessfulLogin(): void {

        this.loginForm.reset();

        toastr.success('Successful login!');

        this.router.navigate(['/shows']);
    }
}
