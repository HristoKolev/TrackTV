import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import { routerReducer } from '../external/angular-redux-router/reducer';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { createEpicMiddleware } from 'redux-observable';
import { Observable } from 'rxjs/Observable';
import 'rxjs';

declare const window: any;

const emptyEpic = (o$: any) => o$.filter(() => false);

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

let allReducers: any = {router: routerReducer};

const createReducer = (newReducers: any = {}): any => {

    allReducers = {...allReducers, ...newReducers};

    return combineReducers(allReducers);
};

const epics$ = new BehaviorSubject<any>(emptyEpic);

const rootEpic = (action$: any, store: any): Observable<any> => epics$.mergeMap(epic => epic(action$, store));

export const store = createStore<any>(createReducer(), composeEnhancers(applyMiddleware(createEpicMiddleware(rootEpic))));

export const addReducers = (newReducers: any = {}): void => store.replaceReducer(createReducer(newReducers));

export const addEpics = (epics: any[]): void => {
    for (let epic of epics) {
        epics$.next(epic);
    }
};

