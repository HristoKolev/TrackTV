import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CalendarDay, CalendarModel, CalendarNavigationInfo } from './calendar.models';
import { ResolveData } from '../shared/router.models';

@Component({
    moduleId: module.id,
    templateUrl: 'calendar.component.html',
    styleUrls: ['calendar.component.css'],
})
export class CalendarComponent implements OnInit {

    //noinspection JSMismatchedCollectionQueryUpdate
    public weeks: CalendarDay[][];

    private navigationInfo: CalendarNavigationInfo;

    //noinspection JSMismatchedCollectionQueryUpdate
    private readonly daysOfWeek: string[] = [
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
        'Saturday',
        'Sunday',
    ];

    constructor(private route: ActivatedRoute) {
    }

    public ngOnInit(): void {

        this.route.data.forEach((data: ResolveData<CalendarModel>) => {

            this.weeks = data.model.weeks;
            this.navigationInfo = data.model.navigationInfo;
        });
    }

    private isToday(date: Date): boolean {

        return date.toDateString() === new Date().toDateString();
    }
}
