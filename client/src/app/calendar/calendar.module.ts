import { ChangeDetectionStrategy, Component, Injectable, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { reduxStore } from '../../infrastructure/redux-store';
import { calendarActions, calendarReducer, calendarSagas } from './calendar.state';
import { apiClient } from '../shared/api-client';

@Injectable()
export class CalendarService {

    fetchCalendar() {
        reduxStore.dispatch({
            type: calendarActions.FETCH_CALENDAR_REQUEST_START,
        });
    }
}

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <pre *ngIf="state | async as data">
            {{ data | json}}
        </pre>
    `,
})
export class CalendarComponent implements OnInit {

    state: any = reduxStore.select(state => state.calendar);

    constructor(private calendarService: CalendarService) {
    }

    ngOnInit(): void {

        this.calendarService.fetchCalendar();
    }
}

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild([
            {path: '', component: CalendarComponent},
        ]),
        FormsModule,
        SharedModule,
    ],
    declarations: [
        CalendarComponent,
    ],
    providers: [CalendarService],
})
export class CalendarModule {

    constructor() {
        reduxStore.addReducers({
            calendar: calendarReducer,
        });

        reduxStore.addSagas(calendarSagas(apiClient));
    }
}
