import {ModuleWithProviders} from '@angular/core';
import {RouterModule} from '@angular/router';
import {AuthGuard} from '../shared/index';
import {MyShowsComponent} from './my-shows.component';
import {MyShowsResolve} from './my-shows-resolve.service';

export const myShowsRouting : ModuleWithProviders = RouterModule.forChild([
    
    {
        path: 'myshows',
        component: MyShowsComponent,
        canActivate: [AuthGuard],
        resolve: {
            model: MyShowsResolve
        }
    },
]);