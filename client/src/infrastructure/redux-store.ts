import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import { createEpicMiddleware } from 'redux-observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { routerActions } from './redux-router';
import { freezeMiddleware } from './freeze-middleware';
import { ReduxEpicMap, ReduxReducer, ReduxReducerMap } from './redux-types';
import { globalActions } from '../app/global.state';

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

    private store: any;

    private allReducers: ReduxReducerMap;

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

    public addEpics(epics: ReduxEpicMap): void {

        for (let [epicName, epic] of Object.entries(epics)) {

            console.log(`Adding epic: '${epicName}'`);

            if (!epic) {
                throw new Error(`Epic '${epicName}' is falsy.`);
            }

            this.epics$.next((...args: any[]) => epic.apply(null, args)
                .map((action: any) => ({...action, dispatchedBy: epicName}))
                .catch((error: any) => {

                    console.error(error);

                    console.warn(`The epic '${epicName}' errored and is being restarted.`);

                    this.dispatch({type: globalActions.END_TRANSITION});

                    this.addEpics({
                        [epicName]: epic,
                    });

                    return [];
                }));
        }
    }

    public addReducers(reducers: ReduxReducerMap = {}): void {

        for (let [reducerName, reducer] of Object.entries(reducers)) {

            console.log(`Adding reducer: '${reducerName}'`);

            if (!reducer) {
                throw new Error(`Reducer '${reducerName}' is falsy.`);
            }
        }

        return this.store.replaceReducer(this.createReducer(reducers));
    }

    public getState() {

        return this.store.getState();
    }

    public dispatch(...args: any[]) {
        return this.store.dispatch(...args);
    }

    private createReducer(reducers: ReduxReducerMap = {}): any {

        this.allReducers = {...this.allReducers, ...reducers};

        return combineReducers(this.allReducers);
    }

}

export const reduxState = new StoreWrapper();
