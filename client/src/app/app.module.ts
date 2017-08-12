import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { Router, RouterModule, Routes } from '@angular/router';
import { IdlePreload, IdlePreloadModule } from '@angularclass/idle-preload';

import { AppComponent } from './app.component';
import { NotFound404Component } from './not-found404.component';

import { ReactiveFormsModule } from '@angular/forms';
import { DevToolsExtension, NgRedux, NgReduxModule } from '@angular-redux/store';
import { CatsReduxRouter, CatsReduxRouterModule, explicitRouterEpic } from '../infrastructure/redux-router';
import { addReducers, initStore } from '../infrastructure/redux-store';
import { settingsReducer } from '../infrastructure/settings.state';
import { addEpics } from '../infrastructure/redux-epics';

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
        CatsReduxRouterModule,
    ],
    bootstrap: [AppComponent],
    exports: [AppComponent],
    providers: [],
})
export class AppModule {

    constructor(ngRedux: NgRedux<any>, reduxRouter: CatsReduxRouter, devTools: DevToolsExtension, router: Router) {

        const enhancers = devTools.isEnabled() ? [devTools.enhancer()] : [];

        ngRedux.provideStore(initStore(...enhancers));

        addReducers({
            settings: settingsReducer,
        });

        addEpics({
            explicitRouterEpic,
        });
    }
}
