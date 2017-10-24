import { Injectable, NgModule } from '@angular/core';
import { NgRedux } from '@angular-redux/store';

type PersistStrategy = 'localStorage' | 'sessionStorage';

const prefix: string = 'redux_';

export const getPersistedState = (): any => {

    const localStorageItems = Object.keys(localStorage)
        .filter(x => x.startsWith(prefix))
        .reduce((items, key) => ({
            ...items,
            [key.substr(prefix.length)]: JSON.parse(localStorage.getItem(key) || '{}'),
        }), {});

    const sessionStorageItems = Object.keys(sessionStorage)
        .filter(x => x.startsWith(prefix))
        .reduce((items, key) => ({
            ...items,
            [key.substr(prefix.length)]: JSON.parse(sessionStorage.getItem(key) || '{}'),
        }), {});

    return {
        ...sessionStorageItems,
        ...localStorageItems
    };
};

@Injectable()
export class ReduxPersist {

    constructor(private ngRedux: NgRedux<any>) {
    }

    initialize(persistConfig: { [key: string]: PersistStrategy }): void {

        for (let [propertyName, persistStrategy] of Object.entries(persistConfig)) {

            this.ngRedux.select(state => state[propertyName])
                .distinctUntilChanged()
                .subscribe(propertyValue => {
                    this.persist(propertyName, propertyValue, persistStrategy as PersistStrategy);
                });
        }
    }

    private persist(propertyName: string, propertyValue: any, persistStrategy: PersistStrategy) {
        switch (persistStrategy) {
            case 'localStorage': {

                this.saveToLocalStorage(propertyValue, propertyName, persistStrategy);
                break;
            }
            case 'sessionStorage': {

                this.saveToSessionStorage(propertyValue, propertyName, persistStrategy);
                break;
            }
            default: {
                throw new Error(
                    `The PersistStrategy value for property '${propertyName}' is invalid. PersistStrategy: '${persistStrategy}'`);
            }
        }
    }

    private saveToSessionStorage(propertyValue: any, propertyName: string, persistStrategy: PersistStrategy) {
        try {

            const json = JSON.stringify(propertyValue);

            sessionStorage.setItem(prefix + propertyName, json);

        } catch (err) {
            throw new Error(
                `Failed to persist property '${propertyName}' with strategy ${persistStrategy}. Error: ` + err.getStacktrace());
        }
    }

    private saveToLocalStorage(propertyValue: any, propertyName: string, persistStrategy: PersistStrategy) {
        try {

            const json = JSON.stringify(propertyValue);

            localStorage.setItem(prefix + propertyName, json);

        } catch (err) {
            throw new Error(
                `Failed to persist property '${propertyName}' with strategy ${persistStrategy}. Error: ` + err.getStacktrace());
        }
    }
}

@NgModule({
    providers: [ReduxPersist],
})
export class ReduxPersistModule {
}
