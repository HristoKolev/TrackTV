import {Component} from  '@angular/core';
import {FormGroup, FormControl, Validators} from  '@angular/forms';

import * as toastr from 'toastr';

import {Authentication, LoginUser, LoginError} from "../../services/index";

@Component({
    moduleId: module.id,
    selector: 'login-component',
    templateUrl: 'login.component.html',
})
export class LoginComponent {

    private loginForm : FormGroup;

    private formActive : boolean = true;

    constructor(private authentication : Authentication) {

        this.createForm();
    }

    private createForm() : void {

        this.loginForm = new FormGroup({

            username: new FormControl(null, [Validators.required, Validators.minLength(6)]),
            password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
        });
    }

    private notifyLoginError(error : LoginError) : void {

        let message : string;

        if (error === LoginError.InvalidCredentials) {

            message = 'Invalid username or password!';
        }
        else {

            message = 'Server error! Try again in a few minutes.';
        }

        toastr.error(message);
    }

    private notifyLoginSuccess() : void {

        toastr.success('Successful login!');

        this.resetForm();
    }

    private resetForm() : void {

        this.createForm();

        this.formActive = false;
        setTimeout(() => this.formActive = true, 0);
    }

    private login() {

        let user = new LoginUser();
        user.username = this.loginForm.get('username').value;
        user.password = this.loginForm.get('password').value;

        this.authentication.login(user)
            .subscribe(this.notifyLoginSuccess.bind(this), this.notifyLoginError.bind(this));
    }
}