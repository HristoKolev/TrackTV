import {ModuleWithProviders}  from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {ShowsComponent} from './shows.component';
import {NetworkShowsComponent} from './network-shows.component';
import {SearchShowsComponent} from './search-shows.component';
import {ShowsResolve} from './shows-resolve.service';
import {ShowsByGenreResolve} from './shows-by-genre-resolve.service';

const showsRoutes : Routes = [

    {path: '', component: ShowsComponent, resolve: {showsModel: ShowsResolve}},
    {path: 'shows', component: ShowsComponent, resolve: {showsModel: ShowsResolve}},
    {path: 'shows/genre/:genre', component: ShowsComponent, resolve: {showsModel: ShowsByGenreResolve}},
    {path: 'shows/network/:network', component: NetworkShowsComponent},
    {path: 'shows/search/:query', component: SearchShowsComponent}
];

export const showsRouting : ModuleWithProviders = RouterModule.forChild(showsRoutes);