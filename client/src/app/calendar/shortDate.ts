import { Pipe, PipeTransform } from '@angular/core';
import * as s from 'underscore.string';

@Pipe({name: 'shortDate'})
export class ShortDate implements PipeTransform {

    private readonly monthNames: string[] = [
        'January',
        'February',
        'March',
        'April',
        'May',
        'June',
        'July',
        'August',
        'September',
        'October',
        'November',
        'December',
    ];

    public transform(date: Date, ...args: any[]): string {

        const monthName = this.monthNames[date.getMonth()].substr(0, 3);

        return monthName + '. ' + s.lpad(date.getDate().toString(), 2, '0');
    }
}
