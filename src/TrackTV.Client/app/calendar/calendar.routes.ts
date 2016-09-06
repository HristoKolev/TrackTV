import {ModuleWithProviders}  from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {CalendarComponent} from './calendar.component';
import {CalendarResolve} from './calendar-resolve.service';

const calendarRoutes : Routes = [

    {path: 'calendar', component: CalendarComponent, resolve: {model: CalendarResolve}},
    {path: 'calendar/:year/:month', component: CalendarComponent, resolve: {model: CalendarResolve}},
];

export const calendarRouting : ModuleWithProviders = RouterModule.forChild(calendarRoutes);