import { ChangeDetectionStrategy, Component, Injectable, NgModule, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
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
        <pre>{{ data | json}}</pre>
    `,
})
export class CalendarComponent implements OnInit {

    data: any;

    constructor(private calendarService: CalendarService) {
    }

    ngOnInit(): void {

        reduxStore.select(state => state.calendar)
            .subscribe((calendar) => {
                this.data = calendar;
            });

        this.calendarService.fetchCalendar();
    }
}

const routes: Routes = [
    {path: '', component: CalendarComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
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
