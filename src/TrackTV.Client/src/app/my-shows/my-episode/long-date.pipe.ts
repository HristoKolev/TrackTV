import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'longDate'})
export class LongDate implements PipeTransform {

    public transform(date : string | Date, ...args : any[]) : string {

        if (date instanceof String) {

            date = new Date(date);
        }

        if (!(date instanceof Date)) {

            throw new Error(`Invalid value passed to pipe: ${date}`);
        }

        const options = {weekday: 'long', year: 'numeric', month: 'long', day: '2-digit'};

        return date.toLocaleDateString('en-US', options);
    }
}