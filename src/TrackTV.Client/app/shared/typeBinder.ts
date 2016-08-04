import {provide} from 'angular2/core';

export class TypeBinder {

    public bindings : any[] = [];

    bind(interfaceObject : any, implementationObject : any) {

        if (!interfaceObject) {

            throw new Error('The interface object is falsy.');
        }

        if (!implementationObject) {

            throw new Error('The implementation object is falsy.');
        }

        this.bindings.push(provide(interfaceObject, {useClass: implementationObject}))
    }

    bindToSelf(obj : any) {

        this.bind(obj, obj)
    }
}