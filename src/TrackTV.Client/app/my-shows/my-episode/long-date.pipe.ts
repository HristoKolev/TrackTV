import {Pipe, PipeTransform} from  '@angular/core';

@Pipe({name: 'longDate'})
export class LongDate implements PipeTransform {

    transform(date : Date, ...args : any[]) : any {

        if (!(date instanceof Date)) {

            date = new Date(date.toString());
        }

        var options = {weekday: 'long', year: 'numeric', month: 'long', day: '2-digit'};

        return date.toLocaleDateString('en-US', options);
    }
}