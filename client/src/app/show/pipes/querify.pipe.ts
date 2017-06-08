import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'querify'})
export class Querify implements PipeTransform {

    public transform(value : string, ...args : any[]) : string {

        if (value) {
            return value.replace(/ /g, '+');
        }

        return null;
    }
}