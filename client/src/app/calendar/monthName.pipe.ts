import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'monthName'})
export class MonthName implements PipeTransform {

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

    public transform(num: number, ...args: any[]): string {

        return this.monthNames[num - 1];
    }
}
