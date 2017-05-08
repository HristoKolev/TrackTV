import {RouterModule} from '@angular/router';

export const appRoutes = RouterModule.forRoot([
    {
        path: '',
        redirectTo: '/calendar',
        pathMatch: 'full'
    }
]);