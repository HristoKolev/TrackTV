import {Component} from  'angular2/core';
import {Control, ControlGroup, FormBuilder, Validators} from  'angular2/common';

import * as toastr from 'toastr';

import {Authentication, LoginUser, LoginError} from "../services/index";

@Component({
    moduleId: module.id,
    selector: 'login-component',
    templateUrl: 'login.component.html',
})
export class LoginComponent {

    loginForm : ControlGroup;

    formActive : boolean = true;

    constructor(private formBuilder : FormBuilder, private authentication : Authentication) {

        this.createForm();
    }

    private createForm() : void {

        this.loginForm = this.formBuilder.group({
            username: new Control(null, Validators.compose([Validators.required, Validators.minLength(6)])),
            password: new Control(null, Validators.compose([Validators.required, Validators.minLength(6)])),
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

    login() {

        let user = new LoginUser();
        user.username = this.loginForm.find('username').value;
        user.password = this.loginForm.find('password').value;

        this.authentication.login(user)
            .subscribe(this.notifyLoginSuccess.bind(this), this.notifyLoginError.bind(this));
    }
}