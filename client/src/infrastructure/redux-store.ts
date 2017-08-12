import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import { createEpicMiddleware } from 'redux-observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { routerActions } from './redux-router';
import { freezeMiddleware } from './freeze-middleware';
import { NgModule } from '@angular/core';

export interface RouterState {
    location: string;
}

export const routerReducer = (state: RouterState = {} as RouterState, action: any): RouterState => {
    switch (action.type) {
        case routerActions.ROUTER_NAVIGATION:
        case routerActions.ROUTER_ERROR:
        case routerActions.ROUTER_CANCEL: {
            return {
                location: action.location,
            };
        }
        default:
            return state;
    }
};

class StoreWrapper {

    private store: any;

    private allReducers: any;

    private epics$: BehaviorSubject<any>;

    constructor() {

        this.allReducers = {router: routerReducer};

        this.epics$ = new BehaviorSubject<any>((actions$: any) => actions$.filter(() => false));

    }

    public initStore(enhancers: any[] = []) {

        const rootEpic = (action$: any, store: any) => this.epics$.mergeMap(epic => epic(action$, store));

        const middleware = [
            createEpicMiddleware(rootEpic),
            freezeMiddleware,
        ];

        const enhancer = compose(
            applyMiddleware(...middleware),
            ...enhancers
        );

        this.store = createStore<any>(this.createReducer(), enhancer);

        return this.store;
    }

    public addEpics(newEpics: any): void {

        for (let [epicName, epic] of Object.entries(newEpics)) {

            console.log('Adding epic:', epicName);

            if (!epic) {
                console.log(`Epic '${epicName}' is falsy.`);
            }

            this.epics$.next((...args: any[]) => epic(...args)
                .map((action: any) => ({...action, dispatchedBy: epicName}))
                .catch(console.error.bind(console)));
        }
    }

    public addReducers(newReducers: any = {}): void {

        for (let [reducerName, reducer] of Object.entries(newReducers)) {

            console.log('Adding reducer:', reducerName);

            if (!reducer) {
                throw new Error(`Reducer '${reducerName}' is falsy.`);
            }
        }

        return this.store.replaceReducer(this.createReducer(newReducers));
    }

    public getState() {

        return this.store.getState();
    }

    private createReducer(newReducers: any = {}): any {

        this.allReducers = {...this.allReducers, ...newReducers};

        return combineReducers(this.allReducers);
    }
}

export const reduxState = new StoreWrapper();

export const actionTypes = (actionPrefix: string) => ({
    ofType: <T>() => new Proxy({}, {
        get: (target: any, name: string) => actionPrefix + '/' + name,
    }) as T,
});
