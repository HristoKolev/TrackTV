import {ModuleWithProviders} from '@angular/core';
import {RouterModule} from '@angular/router';
import {AuthGuard} from '../shared/index';
import {CalendarComponent} from './calendar.component';
import {CalendarResolve} from './calendar-resolve.service';

export const calendarRouting : ModuleWithProviders = RouterModule.forChild([

    {
        path: 'calendar',
        component: CalendarComponent,
        canActivate: [AuthGuard],
        resolve: {
            model: CalendarResolve
        }
    },
    {
        path: 'calendar/:year/:month',
        component: CalendarComponent,
        canActivate: [AuthGuard],
        resolve: {
            model: CalendarResolve
        }
    },
]);