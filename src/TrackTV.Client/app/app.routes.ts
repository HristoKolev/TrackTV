import {provideRouter, RouterConfig} from '@angular/router';

import {LoginComponent, RegisterComponent} from  './account/index';
import {ShowsComponent} from  './shows/shows.component';

const routes : RouterConfig = [

    {path: '', component: LoginComponent},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'shows', component: ShowsComponent},
    {path: 'shows/genre/:genre', component: ShowsComponent},
];

export const routerProviders = [
    provideRouter(routes)
];