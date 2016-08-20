import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {HttpModule} from '@angular/http';
import {FormsModule, ReactiveFormsModule} from  '@angular/forms';

import {AppComponent} from './app.component';

import {typeBindings} from './app.providers';
import {routes} from './app.routes';
import {appDeclarations} from  './app.declarations';

@NgModule({
    imports: [
        BrowserModule,
        RouterModule.forRoot(routes),
        FormsModule,
        ReactiveFormsModule,
        HttpModule
    ],
    providers: [...typeBindings],
    declarations: [AppComponent, ...appDeclarations],
    bootstrap: [AppComponent]
})
export class AppModule {
}