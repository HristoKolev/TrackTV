import {Component, OnInit, OnDestroy} from '@angular/core';
import {ActivatedRoute} from '@angular/router';

import {Subscription} from 'rxjs';

import {CalendarService} from './calendar.service';
import {CalendarModel, CalendarDay, CalendarNavigationInfo} from './calendar.models';

@Component({
    moduleId: module.id,
    selector: 'calendar-component',
    templateUrl: 'calendar.component.html',
    styleUrls: ['calendar.component.css']
})
export class CalendarComponent implements OnInit, OnDestroy {

    private routeSubscription : Subscription;

    private weeks : CalendarDay[][];

    private navigationInfo : CalendarNavigationInfo;

    private daysOfWeek : string[] = [
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
        'Saturday',
        'Sunday'
    ];

    constructor(private activatedRoute : ActivatedRoute,
                private calendarService : CalendarService) {
    }

    private isToday(date : Date) {

        return date.toDateString() === new Date().toDateString();
    }

    public ngOnInit() : void {

        this.routeSubscription = this.activatedRoute.params.subscribe(params => {

            const year : number = params['year'];
            const month : number = params['month'];

            if (year && month) {

                this.calendarService.getMonth(year, month)
                    .subscribe((calendarModel : CalendarModel) => {

                        this.weeks = calendarModel.weeks;
                        this.navigationInfo = calendarModel.navigationInfo;
                    });
            }
            else {

                this.calendarService.currentMonth()
                    .subscribe((calendarModel : CalendarModel) => {

                        this.weeks = calendarModel.weeks;
                        this.navigationInfo = calendarModel.navigationInfo;
                    });
            }
        });
    }

    public ngOnDestroy() : void {

        this.routeSubscription.unsubscribe();
    }
}