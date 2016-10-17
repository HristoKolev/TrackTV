import {ModuleWithProviders} from '@angular/core';
import {RouterModule} from '@angular/router';
import {ShowComponent} from './show.component';
import {ShowResolve} from './show-resolve.service';

export const showRouting : ModuleWithProviders = RouterModule.forChild([

    {
        path: 'show/:show',
        component: ShowComponent,
        resolve: {
            model: ShowResolve
        }
    },
]);