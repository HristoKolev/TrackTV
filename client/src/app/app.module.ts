import { ApplicationRef, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { IdlePreload, IdlePreloadModule } from '@angularclass/idle-preload';

import { AppComponent, NotFound404Component } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';

import { globalErrorReducer, settingsReducer, userSessionReducer } from './global.state';
import {
    explicitRouterSaga,
    ReduxHelperModule,
    ReduxPersistService,
    ReduxRouterService,
    reduxStore,
    wrapDevToolsExtension,
} from '../infrastructure/redux-store';
import { HeaderComponent } from './layout/header-component';
import { LoadingComponent } from './layout/loading-component';

export const routes: Routes = [
    {path: '', redirectTo: '/lazy', pathMatch: 'full'},
    {path: 'lazy', loadChildren: './lazy/lazy.module#LazyModule'},
    {path: 'account', loadChildren: './account/account.module#AccountModule'},
    {path: 'shows', loadChildren: './shows/shows.module#ShowsModule'},
    {path: 'show', loadChildren: './show/show.module#ShowModule'},
    {path: 'my-shows', loadChildren: './my-shows/my-shows.module#MyShowsModule'},
    {path: '**', component: NotFound404Component},
];

@NgModule({
    declarations: [
        AppComponent,
        NotFound404Component,
        HeaderComponent,
        LoadingComponent,
    ],
    entryComponents: [],
    imports: [
        BrowserModule,
        HttpModule,
        ReactiveFormsModule,
        IdlePreloadModule.forRoot(), // forRoot ensures the providers are only created once
        RouterModule.forRoot(routes, {useHash: false, preloadingStrategy: IdlePreload}),
        ReduxHelperModule,

    ],
    bootstrap: [AppComponent],
    exports: [AppComponent],
    providers: [],
})
export class AppModule {

    constructor(reduxRouter: ReduxRouterService, reduxPersist: ReduxPersistService, appRef: ApplicationRef) {

        const devToolsExtension = wrapDevToolsExtension((window as any).devToolsExtension, appRef);

        reduxStore.initStore([devToolsExtension()]);

        reduxRouter.init(state => state.router);

        reduxPersist.initialize({
            session: 'localStorage',
        });

        reduxStore.addReducers({
            settings: settingsReducer,
            global: globalErrorReducer,
            session: userSessionReducer,
        });

        reduxStore.addSagas({
            explicitRouterSaga,
            logSaga: {
                type: '*',
                saga: function* (action: any): any {
                    console.log(action);
                },
            },
        });
    }
}
