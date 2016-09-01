import {ModuleWithProviders}  from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {ShowComponent} from './show.component';

const showRoutes : Routes = [

    {path: 'show/:show', component: ShowComponent},
];

export const showRouting : ModuleWithProviders = RouterModule.forChild(showRoutes);