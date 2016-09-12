import {ModuleWithProviders} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {ShowComponent} from './show.component';
import {ShowResolve} from './show-resolve.service';

const showRoutes : Routes = [

    {path: 'show/:show', component: ShowComponent, resolve: {show: ShowResolve}},
];

export const showRouting : ModuleWithProviders = RouterModule.forChild(showRoutes);