import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {CalendarModel} from './calendar.models';
import {CalendarService} from './calendar.service';

@Injectable()
export class CalendarResolve implements Resolve<CalendarModel> {

    constructor(private calendarService : CalendarService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<CalendarModel> {

        const {year, month} = route.params;

        if (year && month) {

            return this.calendarService.getMonth(year, month);
        }
        else {

            return this.calendarService.currentMonth();
        }
    }
}