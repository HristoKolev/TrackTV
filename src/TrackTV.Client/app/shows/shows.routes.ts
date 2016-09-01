import {ModuleWithProviders}  from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {ShowsComponent} from './shows.component';
import {NetworkShowsComponent} from './network-shows.component';
import {SearchShowsComponent} from './search-shows.component';

const showsRoutes : Routes = [
    
    {path: '', component: ShowsComponent},
    {path: 'shows', component: ShowsComponent},
    {path: 'shows/genre/:genre', component: ShowsComponent},
    {path: 'shows/network/:network', component: NetworkShowsComponent},
    {path: 'shows/search/:query', component: SearchShowsComponent}
];

export const showsRouting : ModuleWithProviders = RouterModule.forChild(showsRoutes);