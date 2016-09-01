import {ModuleWithProviders}  from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {MyShowsComponent} from './my-shows.component';
import {AuthGuard} from '../shared/index';

const myShowsRoutes : Routes = [

    {path: 'myshows', component: MyShowsComponent, canActivate: [AuthGuard]},
];

export const myShowsRouting : ModuleWithProviders = RouterModule.forChild(myShowsRoutes);