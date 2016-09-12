import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {CalendarModel, CalendarDay, CalendarNavigationInfo} from './calendar.models';

@Component({
    moduleId: module.id,
    selector: 'calendar-component',
    templateUrl: 'calendar.component.html',
    styleUrls: ['calendar.component.css']
})
export class CalendarComponent implements OnInit {

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

    constructor(private route : ActivatedRoute) {
    }

    private isToday(date : Date) {

        return date.toDateString() === new Date().toDateString();
    }

    public ngOnInit() : void {

        this.route.data.forEach((data : {model : CalendarModel}) => {

            this.weeks = data.model.weeks;
            this.navigationInfo = data.model.navigationInfo;
        });
    }
}