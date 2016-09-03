import {Pipe, PipeTransform} from '@angular/core';
import * as moment from 'moment';

@Pipe({name: 'premieredDate'})
export class PremieredDate implements PipeTransform {

    public transform(date : Date, ...args : any[]) : string {

        if (date instanceof String) {

            date = new Date(<any>date);
        }

        return moment(date).format('MMM \'DD, YYYY');
    }
}