import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { IdlePreload, IdlePreloadModule } from '@angularclass/idle-preload';

import { AppComponent, NotFound404Component } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DevToolsExtension, NgRedux, NgReduxModule } from '@angular-redux/store';
import { explicitRouterEpic, ReduxRouter, ReduxRouterModule } from '../infrastructure/redux-router';

import { settingsReducer } from './global.state';
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


