import { ModuleWithProviders } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CalendarComponent } from './calendar.component';
import { CalendarResolve } from './calendar-resolve.service';
import { AuthGuard } from '../identity/auth-guard.service';

export const calendarRouting: ModuleWithProviders = RouterModule.forChild([

    {
        path: 'calendar',
        component: CalendarComponent,
        canActivate: [AuthGuard],
        resolve: {
            model: CalendarResolve,
        },
    },
    {
        path: 'calendar/:year/:month',
        component: CalendarComponent,
        canActivate: [AuthGuard],
        resolve: {
            model: CalendarResolve,
        },
    },
]);
