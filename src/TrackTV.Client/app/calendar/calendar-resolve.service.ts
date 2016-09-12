import {Injectable} from '@angular/core';
import {Resolve, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {CalendarModel} from './calendar.models';
import {CalendarService} from './calendar.service';

@Injectable()
export class CalendarResolve implements Resolve<CalendarModel> {

    constructor(private calendarService : CalendarService) {
    }

    public resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot) : Observable<any>|Promise<any>|any {

        const year : number = route.params['year'];
        const month : number = route.params['month'];

        if (year && month) {

            return this.calendarService.getMonth(year, month);
        }
        else {

            return this.calendarService.currentMonth();
        }

    }
}