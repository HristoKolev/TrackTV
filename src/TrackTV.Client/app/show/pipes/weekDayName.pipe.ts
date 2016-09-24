import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'weekDayName'})
export class WeekDayName implements PipeTransform {

    private readonly daysOfWeek = [
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
        'Saturday',
        'Sunday'
    ];

    public transform(number : number, ...args : any[]) : string {

        return this.daysOfWeek[number - 1];
    }
}