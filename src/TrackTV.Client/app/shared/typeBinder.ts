export class TypeBinder {

    public bindings : any[] = [];

    public bind(interfaceObject : any, implementationObject : any) : void {

        if (!interfaceObject) {

            throw new Error('The interface object is falsy.');
        }

        if (!implementationObject) {

            throw new Error('The implementation object is falsy.');
        }

        this.bindings.push({provide: interfaceObject, useClass: implementationObject})
    }

    public bindToSelf(obj : any) : void {

        this.bind(obj, obj)
    }
}