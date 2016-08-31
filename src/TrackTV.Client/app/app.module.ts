import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {HttpModule} from '@angular/http';
import {FormsModule} from  '@angular/forms';

import {AppComponent} from './app.component';

import {typeBindings} from './app.providers';
import {routes} from './app.routes';
import {appDeclarations} from  './app.declarations';

import {AccountModule} from './account/account.module';

@NgModule({
    imports: [
        BrowserModule,
        RouterModule.forRoot(routes),
        FormsModule,
        HttpModule,
        AccountModule
    ],
    providers: [...typeBindings],
    declarations: [AppComponent, ...appDeclarations],
    bootstrap: [AppComponent]
})
export class AppModule {
}