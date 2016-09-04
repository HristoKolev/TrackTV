import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {SharedModule} from "../shared/shared.module";

import {CalendarService} from './calendar.service';

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
        MonthName,
        ShortDate
    ],
    providers: [
        CalendarService
    ]
})
export class CalendarModule {
}