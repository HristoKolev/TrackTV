import {RouterConfig} from '@angular/router';

import {LoginComponent} from  './account/login/login.component';
import {RegisterComponent} from  './account/register/register.component';

import {ShowsComponent} from './shows/shows.component';
import {NetworkShowsComponent} from  './shows/network-shows.component';
import {SearchShowsComponent} from  './shows/search-shows.component';
import {ShowComponent} from  './show/show.component';
import {MyShowsComponent} from './my-shows/my-shows.component';

export const routes : RouterConfig = [

    {path: '', component: ShowsComponent},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'shows', component: ShowsComponent},
    {path: 'shows/genre/:genre', component: ShowsComponent},
    {path: 'shows/network/:network', component: NetworkShowsComponent},
    {path: 'shows/search/:query', component: SearchShowsComponent},
    {path: 'show/:show', component: ShowComponent},
    {path: 'myshows', component: MyShowsComponent},
];