import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import { createEpicMiddleware } from 'redux-observable';
import { routerReducer } from './redux-router';
import { rootEpic } from './redux-epics';

declare const window: any;

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

let allReducers: any = {router: routerReducer};

const createReducer = (newReducers: any = {}): any => {

    allReducers = {...allReducers, ...newReducers};

    return combineReducers(allReducers);
};

export const store = createStore<any>(createReducer(), composeEnhancers(applyMiddleware(createEpicMiddleware(rootEpic))));

export const addReducers = (newReducers: any = {}): void => store.replaceReducer(createReducer(newReducers));

