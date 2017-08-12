import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import { createEpicMiddleware } from 'redux-observable';
import { routerReducer } from './redux-router';
import { rootEpic } from './redux-epics';
import * as freeze from 'redux-freeze';

let allReducers: any = {router: routerReducer};

const createReducer = (newReducers: any = {}): any => {

    allReducers = {...allReducers, ...newReducers};

    return combineReducers(allReducers);
};

let store: any;

export const initStore = (...enhancers: any[]) => {

    store = createStore<any>(createReducer(), compose(applyMiddleware(createEpicMiddleware(rootEpic), freeze), ...enhancers));

    return store;
};

export const getStore = () => store;

export const addReducers = (newReducers: any = {}): void => store.replaceReducer(createReducer(newReducers));

