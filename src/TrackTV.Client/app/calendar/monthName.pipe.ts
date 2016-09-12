import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'monthName'})
export class MonthName implements PipeTransform {

    private monthNames : string[] = [
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
        'December'
    ];

    transform(number : number, ...args : any[]) : string {

        return this.monthNames[number - 1];
    }
}