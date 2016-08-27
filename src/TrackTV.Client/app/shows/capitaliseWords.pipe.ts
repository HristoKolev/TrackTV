import {Pipe, PipeTransform} from  '@angular/core';

@Pipe({name: 'capitaliseWords'})
export class CapitaliseWords implements PipeTransform {

    public transform(value : string, ...args : any[]) : string {

        let lastCharacter : string = '';

        let result : string = '';

        for (let character of value) {

            if (!lastCharacter || !lastCharacter.trim() || lastCharacter === '-') {

                result += character.toUpperCase();
            }
            else {

                result += character;
            }

            lastCharacter = character;
        }

        return result;
    }
}