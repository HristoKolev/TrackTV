import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppComponent} from './app.component';
import {appRoutes} from './app.routes';
import {AccountModule} from './account/account.module';
import {ShowModule} from './show/show.module';
import {SharedModule} from './shared/shared.module';
import {ShowsModule} from './shows/shows.module';
import {MyShowsModule} from './my-shows/my-shows.module';
import {LayoutModule} from './layout/layout.module';
import {CalendarModule} from './calendar/calendar.module';

@NgModule({
    imports: [
        BrowserModule,

        appRoutes,

        SharedModule,

        LayoutModule,
        AccountModule,
        ShowModule,
        ShowsModule,
        MyShowsModule,
        CalendarModule
    ],
    providers: [],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule {
}