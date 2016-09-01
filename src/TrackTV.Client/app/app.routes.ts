import {RouterConfig} from '@angular/router';

import {ShowsComponent} from './shows/shows.component';
import {NetworkShowsComponent} from  './shows/network-shows.component';
import {SearchShowsComponent} from  './shows/search-shows.component';
import {MyShowsComponent} from './my-shows/my-shows.component';
import {AuthGuard} from './shared/index';

export const routes : RouterConfig = [

    {path: '', component: ShowsComponent},

    {path: 'shows', component: ShowsComponent},
    {path: 'shows/genre/:genre', component: ShowsComponent},
    {path: 'shows/network/:network', component: NetworkShowsComponent},
    {path: 'shows/search/:query', component: SearchShowsComponent},    
    {path: 'myshows', component: MyShowsComponent, canActivate: [AuthGuard]},
];