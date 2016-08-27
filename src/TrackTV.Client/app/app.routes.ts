import {RouterConfig} from '@angular/router';

import {LoginComponent, RegisterComponent} from  './account/index';
import {ShowsComponent, NetworkShowsComponent, SearchShowsComponent} from  './shows/index';
import {ShowComponent} from  './show/show.component';
import {MyShowsComponent} from  './my-shows/my-shows.component';

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