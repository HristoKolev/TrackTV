export abstract class PersistentContainer<T> {

    public abstract get(key: string): T;

    public abstract set(key: string, value: T): void;

    public abstract remove(key: string): void;
}

export class PersistentContainerKey<T> {

    constructor(private container: PersistentContainer<T>, private key: string) {
    }

    public get(): T {

        return this.container.get(this.key);
    }

    public set(value: T): void {

        this.container.set(this.key, value);
    }

    public remove(): void {

        this.container.remove(this.key);
    }
}
