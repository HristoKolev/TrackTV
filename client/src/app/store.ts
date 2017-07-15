import { combineReducers, compose, createStore, GenericStoreEnhancer } from 'redux';
import { routerReducer } from '../external/angular-redux-router/reducer';

declare const window: any;

const getDevExtension = (): GenericStoreEnhancer => {

    if (window.devToolsExtension) {

        return window.devToolsExtension() as GenericStoreEnhancer;
    }

    return (f: any) => f;
};

let allReducers: any = {
    router: routerReducer,
};

const createReducer = (newReducers: any = {}): any => {

    allReducers = {
        ...allReducers,
        ...newReducers
    };

    return combineReducers(allReducers);
};

export const store = createStore<any>(createReducer(), compose(getDevExtension()));

export const addReducers = (newReducers: any = {}): void => store.replaceReducer(createReducer(newReducers));
