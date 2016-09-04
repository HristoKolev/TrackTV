import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';

import {Observable} from 'rxjs';

import {ApiPath, Identity} from '../shared/index';

import {CalendarNavigationInfo, MonthRouteInfo, CalendarModel} from './calendar.models';

@Injectable()
export class CalendarService {

    private calendar : (path : string) => string = this.apiPath.service('/calendar');

    constructor(private apiPath : ApiPath,
                private identity : Identity,
                private http : Http) {
    }

    private getMonthRouteInfo(date : Date) : MonthRouteInfo {

        return {
            year: date.getFullYear(),
            month: date.getMonth() + 1
        };
    }

    private getHeaderRoutesInfo(date : Date) : CalendarNavigationInfo {

        date = new Date(date.toString());

        const info : CalendarNavigationInfo = {} as CalendarNavigationInfo;

        info.thisMonth = this.getMonthRouteInfo(date);

        date.setMonth(date.getMonth() - 1);
        info.previosMonth = this.getMonthRouteInfo(date);

        date.setMonth(date.getMonth() + 2);
        info.nextMonth = this.getMonthRouteInfo(date);

        return info;
    }

    private processResponse(response : Response) : CalendarModel {

        const data = response.json();

        for (let week of data.month) {

            for (let day of week) {

                day.date = new Date(day.date.toString());

                if (day.episodes) {

                    for (let episode of day.episodes) {

                        episode.firstAired = new Date(episode.firstAired.toString());
                    }
                }
            }
        }

        data.navigationInfo = this.getHeaderRoutesInfo(new Date(data.date.toString()));
        data.weeks = data.month;

        return data;
    }

    public currentMonth() : Observable<CalendarModel> {

        return this.http.get(this.calendar('/'), this.identity.authenticatedOptions)
            .map((res : Response) => this.processResponse(res));
    }

    public getMonth(year : number, month : number) : Observable<CalendarModel> {

        return this.http.get(this.calendar(`/${year}/${month}`), this.identity.authenticatedOptions)
            .map((res : Response) => this.processResponse(res));
    }
}