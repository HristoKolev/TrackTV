import {RouteDefinition} from 'angular2/router';

import {LoginComponent, RegisterComponent} from  './account/index';
import {ShowsComponent} from  './shows/shows.component';

export const RouteNames = {
    Login: 'Login',
    Register: 'Register',
    Shows: 'Shows',
    ShowsByGenre: 'ShowsByGenre',
};

export const routeDefinitions : RouteDefinition[] = [

    {name: RouteNames.Login, path: '/login', component: LoginComponent},
    {name: RouteNames.Register, path: '/register', component: RegisterComponent},
    {name: RouteNames.Shows, path: '/shows', component: ShowsComponent},
    {name: RouteNames.ShowsByGenre, path: '/shows/genre/:genre', component: ShowsComponent},
];