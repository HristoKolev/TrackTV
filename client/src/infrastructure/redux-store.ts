import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import { freezeMiddleware } from './freeze-middleware';
import createSagaMiddleware from 'redux-saga';
import { put, takeEvery } from 'redux-saga/effects';
import { globalActions } from '../app/global.state';
import { Observable } from 'rxjs/Observable';
import { ApplicationRef, Injectable, NgModule, NgZone } from '@angular/core';
import { NavigationCancel, NavigationError, Router, RoutesRecognized } from '@angular/router';

export const actionTypes = (actionPrefix: string) => ({
    ofType: <T>() => new Proxy({}, {get: (target: any, name: string) => actionPrefix + '/' + name}) as T,
});

export type ReduxReducer<TState = any> = (state: TState, action: any) => TState;
export type ReduxReducerMap = { [key: string]: ReduxReducer<any> };
export type RouterState = { location?: string; };
export type RouterAction = { type: string, location: string };

export const routerReducer: ReduxReducer<RouterState> = (state = {}, action: RouterAction) => {
    switch (action.type) {
        case routerActions.ROUTER_NAVIGATION:
        case routerActions.ROUTER_ERROR: {
            return {
                location: action.location,
            };
        }
        default: {
            return state;
        }
    }
};

class StoreWrapper {

    private sagaMiddleware: any;

    private store: any;

    private allReducers: ReduxReducerMap;

    constructor() {
        this.allReducers = {router: routerReducer};
        this.sagaMiddleware = createSagaMiddleware();
    }

    initStore(enhancers: any[] = []) {

        const middleware = [
            this.sagaMiddleware,
            freezeMiddleware,
        ];

        const enhancer = compose(
            applyMiddleware(...middleware),
            ...enhancers
        );

        const reducer = this.createReducer();

        this.store = createStore<any>(reducer, enhancer);

        return this.store;
    }

    addSagas(sagas: any = {}) {

        const _this = this;

        for (let [sagaName, saga] of Object.entries(sagas)) {

            console.log(`Adding saga: '${sagaName}'`);

            this.sagaMiddleware.run(function* () {

                yield takeEvery(saga.type, function* (...args: any[]) {

                    if (saga.inTransition) {

                        yield put({type: globalActions.START_TRANSITION});
                    }

                    yield saga.saga(...args, _this.getState());

                    if (saga.inTransition) {

                        yield put({type: globalActions.END_TRANSITION});
                    }
                });
            });
        }
    }

    addReducers(reducers: ReduxReducerMap = {}): void {

        for (let [reducerName, reducer] of Object.entries(reducers)) {

            console.log(`Adding reducer: '${reducerName}'`);

            if (!reducer) {
                throw new Error(`Reducer '${reducerName}' is falsy.`);
            }
        }

        return this.store.replaceReducer(this.createReducer(reducers));
    }

    getState() {

        return this.store.getState();
    }

    dispatch(...args: any[]) {
        this.store.dispatch(...args);
    }

    select<T = any>(selector: (state: any) => any = f => f): Observable<T> {

        return Observable.create((observer: any) => {

            observer.next(this.store.getState());

            this.store.subscribe(() => observer.next(this.store.getState()));
        })
            .map(selector)
            .distinctUntilChanged();
    }

    private createReducer(reducers: ReduxReducerMap = {}): any {

        this.allReducers = {...this.allReducers, ...reducers};

        return combineReducers(this.allReducers);
    }
}

export const reduxStore = new StoreWrapper();

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
export class ReduxPersistService {

    initialize(persistConfig: { [key: string]: PersistStrategy }): void {

        for (let [propertyName, persistStrategy] of Object.entries(persistConfig)) {

            reduxStore.select(state => state[propertyName])
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

export const routerActions = actionTypes('router').ofType<{
    ROUTER_NAVIGATION: string;
    ROUTER_NAVIGATION_EXPLICIT: string;
    ROUTER_CANCEL: string;
    ROUTER_ERROR: string;
}>();

let angularRouter: Router;

export const explicitRouterSaga = {
    type: routerActions.ROUTER_NAVIGATION_EXPLICIT,
    saga: function* (action: any) {

        const newAction = {
            type: routerActions.ROUTER_NAVIGATION,
            location: angularRouter.createUrlTree.apply(angularRouter, action.payload).toString(),
        };

        yield put(newAction);
    },
};

@Injectable()
export class ReduxRouterService {

    public init(routerStateSelector: (state: any) => RouterState) {

        if (!routerStateSelector) {
            throw new Error('The router state selector is falsy.');
        }

        let dispatchTriggeredByNavigation = false;
        let navigationTriggeredByDispatch = false;

        reduxStore.select(routerStateSelector).subscribe(state => {

            if (state.location && this.router.url !== state.location) {

                if (dispatchTriggeredByNavigation) {
                    dispatchTriggeredByNavigation = false;
                    return;
                }

                navigationTriggeredByDispatch = true;
                this.router.navigateByUrl(state.location);
            }
        });

        let location: any;

        this.router.events.subscribe(event => {

            if (event instanceof RoutesRecognized) {

                location = event.state.url;

                if (navigationTriggeredByDispatch) {
                    navigationTriggeredByDispatch = false;
                    return;
                }

                dispatchTriggeredByNavigation = true;

                reduxStore.dispatch({type: routerActions.ROUTER_NAVIGATION, location});
            } else if (event instanceof NavigationCancel) {

                reduxStore.dispatch({type: routerActions.ROUTER_CANCEL, location});
            } else if (event instanceof NavigationError) {

                reduxStore.dispatch({type: routerActions.ROUTER_ERROR, location});
            }
        });
    }

    constructor(private router: Router) {

        angularRouter = router;
    }
}

export const wrapDevToolsExtension = (devToolsExtension: any, appRef: ApplicationRef) => {

    return (options?: Object) => {
        let subscription: any;

        // Make sure changes from dev tools update angular's view.
        devToolsExtension.listen(({type}: any) => {

            if (type === 'START') {

                subscription = reduxStore.select(s => s)
                    .subscribe(() => {
                        if (!NgZone.isInAngularZone()) {
                            appRef.tick();
                        }
                    });
            } else if (type === 'STOP') {
                subscription();
            }
        });

        return devToolsExtension(options);
    };
};

@NgModule({
    providers: [ReduxRouterService, ReduxPersistService],
})
export class ReduxHelperModule {
}
