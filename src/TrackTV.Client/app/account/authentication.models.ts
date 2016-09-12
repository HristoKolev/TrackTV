export interface RegisterUser {

    email : string;

    password : string;

    confirmPassword : string;
}

export interface LoginUser {

    username : string;

    password : string;

    grant_type : string;
}

export enum LoginError {

    ServerError = 1,

    InvalidCredentials
}

export enum  RegisterError{

    ServerError = 1,

    InvalidEmail,

    InvalidPassword
}
