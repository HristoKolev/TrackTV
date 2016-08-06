import {PersistentContainer} from './persistentContainer';

export class LocalStorageContainer<T> implements PersistentContainer<T> {

    get(key : string) : T {

        const value = localStorage.getItem(key);

        if (value === undefined) {

            return undefined;
        }

        return <T>JSON.parse(value);
    }

    set(key : string, value : T) : void {

        localStorage.setItem(key, JSON.stringify(value));
    }

    remove(key : string) : void {

        localStorage.removeItem(key);
    }
}