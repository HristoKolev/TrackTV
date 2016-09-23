import {ModuleWithProviders} from '@angular/core';
import {RouterModule} from '@angular/router';

import {MyShowsComponent} from './my-shows.component';
import {MyShowsResolve} from './my-shows-resolve.service';
import {AuthGuard} from '../identity/auth-guard.service';

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