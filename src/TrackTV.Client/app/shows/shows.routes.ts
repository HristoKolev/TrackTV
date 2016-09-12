import {ModuleWithProviders} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {ShowsComponent} from './shows.component';
import {ShowsByNetworkComponent} from './shows-by-network.component';
import {ShowsByNameComponent} from './shows-by-name.component';
import {ShowsResolve} from './shows-resolve.service';
import {ShowsByGenreResolve} from './shows-by-genre-resolve.service';
import {ShowsByNetworkResolve} from './shows-by-network-resolve.service';
import {ShowsByNameResolve} from './shows-by-name-resolve.service';

const showsRoutes : Routes = [

    {
        path: '',
        component: ShowsComponent,
        resolve: {
            showsModel: ShowsResolve
        }
    },
    {
        path: 'shows',
        component: ShowsComponent,
        resolve: {
            showsModel: ShowsResolve
        }
    },
    {
        path: 'shows/genre/:genre',
        component: ShowsComponent,
        resolve: {
            showsModel: ShowsByGenreResolve
        }
    },
    {
        path: 'shows/network/:network',
        component: ShowsByNetworkComponent,
        resolve: {
            showsModel: ShowsByNetworkResolve
        }
    },
    {
        path: 'shows/search/:query',
        component: ShowsByNameComponent,
        resolve: {
            showsModel: ShowsByNameResolve
        }
    }
];

export const showsRouting : ModuleWithProviders = RouterModule.forChild(showsRoutes);