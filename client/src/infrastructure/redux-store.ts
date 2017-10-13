import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import { routerActions } from './redux-router';
import { freezeMiddleware } from './freeze-middleware';
import { ReduxReducer, ReduxReducerMap } from './redux-types';
import createSagaMiddleware from 'redux-saga';
import { put, takeEvery } from 'redux-saga/effects';
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

    private sagaMiddleware: any;

    private store: any;

    private allReducers: ReduxReducerMap;

    constructor() {
        this.allReducers = {router: routerReducer};
        this.sagaMiddleware = createSagaMiddleware();
    }

    public initStore(enhancers: any[] = [], initialState?: any) {

        const middleware = [
            this.sagaMiddleware,
            freezeMiddleware,
        ];

        const enhancer = compose(
            applyMiddleware(...middleware),
            ...enhancers
        );

        this.store = createStore<any>(this.createReducer(), initialState, enhancer);

        return this.store;
    }

    public addSagas(sagas: any = {}) {

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

    private createReducer(reducers: ReduxReducerMap = {}): any {

        this.allReducers = {...this.allReducers, ...reducers};

        return combineReducers(this.allReducers);
    }

}

export const reduxState = new StoreWrapper();
