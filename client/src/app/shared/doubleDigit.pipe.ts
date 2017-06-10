import { Pipe, PipeTransform } from '@angular/core';
import * as s from 'underscore.string';

@Pipe({name: 'doubleDigit'})
export class DoubleDigit implements PipeTransform {

    public transform(value: number, ...args: any[]): string {

        return s.lpad(value.toString(), 2, '0');
    }
}
