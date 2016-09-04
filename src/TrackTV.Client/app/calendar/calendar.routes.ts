import {ModuleWithProviders}  from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {CalendarComponent} from './calendar.component';

const calendarRoutes : Routes = [

    {path: 'calendar', component: CalendarComponent},
    {path: 'calendar/:year/:month', component: CalendarComponent},
];

export const calendarRouting : ModuleWithProviders = RouterModule.forChild(calendarRoutes);