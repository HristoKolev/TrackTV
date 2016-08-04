import {PersistentContainer} from "./persistentContainer";

export class LocalStorageContainer<T> implements PersistentContainer<T> {

    get(key : string) : T {

        if (localStorage[key] === undefined) {

            return undefined;
        }

        return <T>JSON.parse(localStorage[key]);
    }

    set(key : string, value : T) : void {

        localStorage[key] = JSON.stringify(value);
    }

    remove(key : string) : void {

        this.set(key, undefined);
    }
}