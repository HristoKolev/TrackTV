import { PersistentContainer } from './persistentContainer';

export class LocalStorageContainer<T> implements PersistentContainer<T> {

    public get(key: string): T {

        const value = localStorage.getItem(key);

        if (typeof value === 'undefined' || value === null) {

            return null;
        }

        return <T>JSON.parse(value);
    }

    public set(key: string, value: T): void {

        localStorage.setItem(key, JSON.stringify(value));
    }

    public remove(key: string): void {

        localStorage.removeItem(key);
    }
}
