import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';

import {AccountModule} from './account/account.module';
import {ShowModule} from './show/show.module';
import {SharedModule} from './shared/shared.module';
import {ShowsModule} from './shows/shows.module';
import {MyShowsModule} from './my-shows/my-shows.module';
import {LayoutModule} from "./layout/layout.module";

@NgModule({
    imports: [
        BrowserModule,
        RouterModule.forRoot([]),

        SharedModule,

        LayoutModule,
        AccountModule,
        ShowModule,
        ShowsModule,
        MyShowsModule,
    ],
    providers: [],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule {
}