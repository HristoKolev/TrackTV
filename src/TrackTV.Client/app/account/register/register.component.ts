import {Component} from  '@angular/core';
import {Router} from  '@angular/router';
import {FormGroup, FormControl, Validators} from  '@angular/forms';

import * as toastr from 'toastr';

import {Authentication, RegisterUser, RegisterError} from "../../services/index";

@Component({
    moduleId: module.id,
    selector: 'register-component',
    templateUrl: 'register.component.html',
})
export class RegisterComponent {

    registerForm : FormGroup;

    formActive : boolean = true;

    constructor(private router : Router,
                private authentication : Authentication) {

        this.createForm();
    }

    private createForm() : void {

        this.registerForm = new FormGroup({
            email: new FormControl(null, [Validators.required, Validators.minLength(6)]),
            password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
            confirmPassword: new FormControl(null, [Validators.required, Validators.minLength(6)]),
        });
    }

    private resetForm() : void {

        this.createForm();

        this.formActive = false;
        setTimeout(() => this.formActive = true, 0);
    }

    private notifyLoginSuccess() : void {

        toastr.success('Registration successful!');

        this.resetForm();

        this.router.navigate(['/login']);
    }

    private notifyRegisterError(error : RegisterError) : void {

        let message : string;

        if (error === RegisterError.InvalidPassword) {

            message = 'Invalid password.';
        } else {

            message = 'Server error! Try again in a few minutes.';
        }

        toastr.error(message);
    }

    register() {

        let user = new RegisterUser();
        user.email = this.registerForm.get('email').value;
        user.password = this.registerForm.get('password').value;
        user.confirmPassword = this.registerForm.get('confirmPassword').value;

        this.authentication.signup(user)
            .subscribe(this.notifyLoginSuccess.bind(this), this.notifyRegisterError.bind(this));
    }

}