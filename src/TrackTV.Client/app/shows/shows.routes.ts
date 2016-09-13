import {ModuleWithProviders} from '@angular/core';
import {RouterModule} from '@angular/router';
import {ShowsComponent} from './shows.component';
import {ShowsResolve} from './shows-resolve.service';
import {ShowsByGenreComponent} from './shows-by-genre.component';
import {ShowsByGenreResolve} from './shows-by-genre-resolve.service';
import {ShowsByNetworkComponent} from './shows-by-network.component';
import {ShowsByNetworkResolve} from './shows-by-network-resolve.service';
import {ShowsByNameComponent} from './shows-by-name.component';
import {ShowsByNameResolve} from './shows-by-name-resolve.service';

export const showsRouting : ModuleWithProviders = RouterModule.forChild([

    {
        path: 'shows',
        component: ShowsComponent,
        resolve: {
            model: ShowsResolve
        }
    },
    {
        path: 'shows/genre/:genre',
        component: ShowsByGenreComponent,
        resolve: {
            model: ShowsByGenreResolve
        }
    },
    {
        path: 'shows/network/:network',
        component: ShowsByNetworkComponent,
        resolve: {
            model: ShowsByNetworkResolve
        }
    },
    {
        path: 'shows/search/:query',
        component: ShowsByNameComponent,
        resolve: {
            model: ShowsByNameResolve
        }
    }
]);