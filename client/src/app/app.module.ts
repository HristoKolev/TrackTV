import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { IdlePreload, IdlePreloadModule } from '@angularclass/idle-preload';

import { AppComponent, NotFound404Component } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DevToolsExtension, NgRedux, NgReduxModule } from '@angular-redux/store';
import { explicitRouterSaga, ReduxRouter, ReduxRouterModule } from '../infrastructure/redux-router';

import { globalErrorReducer, settingsReducer, userSessionReducer } from './global.state';
import { reduxStore } from '../infrastructure/redux-store';
import { HeaderComponent } from './layout/header-component';
import { LoadingComponent } from './layout/loading-component';
import { apiClient } from './shared/api-client';
import { ReduxPersist, ReduxPersistModule } from '../infrastructure/redux-persist';

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
        NgReduxModule,
        ReduxRouterModule,
        ReduxPersistModule,
    ],
    bootstrap: [AppComponent],
    exports: [AppComponent],
    providers: [],
})
export class AppModule {

    constructor(ngRedux: NgRedux<any>, reduxRouter: ReduxRouter, devTools: DevToolsExtension, reduxPersist: ReduxPersist) {

        reduxRouter.init(state => state.router);

        const enhancers = devTools.isEnabled() ? [devTools.enhancer()] : [];

        ngRedux.provideStore(reduxStore.initStore(enhancers));

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


