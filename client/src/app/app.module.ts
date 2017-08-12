import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { Router, RouterModule, Routes } from '@angular/router';
import { IdlePreload, IdlePreloadModule } from '@angularclass/idle-preload';

import { AppComponent } from './app.component';
import { NotFound404Component } from './not-found404.component';

import { ReactiveFormsModule } from '@angular/forms';
import { DevToolsExtension, NgRedux, NgReduxModule } from '@angular-redux/store';
import { ReduxRouterModule, explicitRouterEpic, ReduxRouter } from '../infrastructure/redux-router';

import { settingsReducer } from './settings.state';
import { reduxState } from '../infrastructure/redux-store';

export const routes: Routes = [
    {path: '', redirectTo: '/lazy', pathMatch: 'full'},
    {path: 'lazy', loadChildren: './lazy/lazy.module#LazyModule'},
    {path: 'account', loadChildren: './account/account.module#AccountModule'},
    {path: '**', component: NotFound404Component},
];

@NgModule({
    declarations: [
        AppComponent,
        NotFound404Component,
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
    ],
    bootstrap: [AppComponent],
    exports: [AppComponent],
    providers: [],
})
export class AppModule {

    constructor(ngRedux: NgRedux<any>, reduxRouter: ReduxRouter, devTools: DevToolsExtension) {

        reduxRouter.init(state => state.router);

        const enhancers = devTools.isEnabled() ? [devTools.enhancer()] : [];

        ngRedux.provideStore(reduxState.initStore(enhancers));

        reduxState.addReducers({
            settings: settingsReducer,
        });

        reduxState.addEpics({
            explicitRouterEpic,
        });
    }
}


