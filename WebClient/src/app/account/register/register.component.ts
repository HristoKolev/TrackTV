import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import * as toastr from 'toastr';
import {Authentication} from '../../identity/authentication.service';
import {RegisterError, RegisterUser} from '../../identity/authentication.models';

@Component({
    moduleId: module.id,
    templateUrl: 'register.component.html',
})
export class RegisterComponent implements OnInit {

    public registerForm : FormGroup;

    constructor(private router : Router,
                private authentication : Authentication) {
    }

    private createForm() : void {

        this.registerForm = new FormGroup({

            email: new FormControl(null, [Validators.required, Validators.minLength(6)]),
            password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
            confirmPassword: new FormControl(null, [Validators.required, Validators.minLength(6)]),
        });
    }

    private notifyRegisterSuccess() : void {

        toastr.success('Registration successful!');

        this.registerForm.reset();

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

    public register() {

        this.authentication.signup(this.registerForm.getRawValue() as RegisterUser)
            .subscribe(
                () => this.notifyRegisterSuccess(),
                (error : RegisterError) => this.notifyRegisterError(error)
            );
    }

    public ngOnInit() : void {

        this.createForm();
    }
}