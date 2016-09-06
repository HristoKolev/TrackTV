import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {SharedModule} from "../shared/shared.module";

import {CalendarService} from './calendar.service';
import {CalendarResolve} from './calendar-resolve.service';

import {CalendarComponent} from './calendar.component';
import {MonthName} from './monthName.pipe';
import {ShortDate} from './shortDate';

import {calendarRouting} from './calendar.routes';

@NgModule({
    imports: [
        BrowserModule,
        SharedModule,
        calendarRouting
    ],
    declarations: [
        CalendarComponent,
        MonthName,
        ShortDate
    ],
    providers: [
        CalendarService,
        CalendarResolve
    ]
})
export class CalendarModule {
}