import {ModuleWithProviders}  from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {MyShowsComponent} from './my-shows.component';
import {AuthGuard} from '../shared/index';
import {MyShowsResolve} from './my-shows-resolve.service';

const myShowsRoutes : Routes = [

    {path: 'myshows', component: MyShowsComponent, canActivate: [AuthGuard], resolve: {shows: MyShowsResolve}},
];

export const myShowsRouting : ModuleWithProviders = RouterModule.forChild(myShowsRoutes);