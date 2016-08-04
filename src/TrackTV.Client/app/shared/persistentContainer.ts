export abstract class PersistentContainer<T> {

    abstract get(key : string) : T;

    abstract set(key : string, value : T) : void;

    abstract remove(key : string) : void;
}

export class PersistentContainerKey<T> {

    constructor(private container : PersistentContainer<T>, private key : string) {
    }

    get() : T {

        return this.container.get(this.key);
    }

    set(value : T) : void {

        this.container.set(this.key, value);
    }

    remove() : void {

        this.container.remove(this.key);
    }
}