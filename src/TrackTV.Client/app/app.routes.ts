import {RouteDefinition} from 'angular2/router';

import {LoginComponent, RegisterComponent} from  './account/index';

export const RouteNames = {
    Login: 'Login',
    Register: 'Register'
};

export const routeDefinitions : RouteDefinition[] = [

    {name: RouteNames.Login, path: '/login', component: LoginComponent},
    {name: RouteNames.Register, path: '/register', component: RegisterComponent}
];