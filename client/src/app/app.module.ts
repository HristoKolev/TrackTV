import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { IdlePreload, IdlePreloadModule } from '@angularclass/idle-preload';

import { AppComponent } from './app.component';
import { NotFound404Component } from './not-found404.component';

import { ReactiveFormsModule } from '@angular/forms';
import { store } from './store';
import { NgRedux, NgReduxModule } from '@angular-redux/store';
import { NgReduxRouter } from './ng-router-cats/router';
import { NgReduxRouterModule } from './ng-router-cats/index';
import { DashboardComponent } from './dashboard/dashboard.component';

export const routes: Routes = [
    {path: '', component: DashboardComponent, pathMatch: 'full'},
    {path: 'lazy', loadChildren: './lazy/lazy.module#LazyModule'},
    {path: '**', component: NotFound404Component},
];

@NgModule({
    declarations: [
        AppComponent,
        DashboardComponent,
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
        NgReduxRouterModule,
    ],
    bootstrap: [AppComponent],
    exports: [AppComponent],
    providers: [],
})
export class AppModule {

    constructor(ngRedux: NgRedux<any>, ngReduxRouter: NgReduxRouter) {

        ngRedux.provideStore(store);
        ngReduxRouter.initialize();
    }
}

