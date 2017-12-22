import {applyMiddleware, combineReducers, compose, createStore} from 'redux';
import {freezeMiddleware} from './freeze-middleware';
import createSagaMiddleware from 'redux-saga';
import {put, takeEvery} from 'redux-saga/effects';
import {Observable} from 'rxjs/Observable';
import {ReduxReducerMap} from './meta';
import {globalActions} from './redux-global-actions';

class StoreWrapper {

  private sagaMiddleware: any;

  private store: any;

  private allReducers: ReduxReducerMap;

  constructor() {
    this.allReducers = {};
    this.sagaMiddleware = createSagaMiddleware();
  }

  initStore(enhancers: any[] = [], initialReducers?: ReduxReducerMap): void {

    if (initialReducers) {

      this.allReducers = initialReducers;
    }

    const middleware = [
      this.sagaMiddleware,
      freezeMiddleware,
    ];

    const enhancer = compose(
      applyMiddleware(...middleware),
      ...enhancers,
    );

    const reducer = this.createReducer();

    this.store = createStore<any>(reducer, enhancer);

    return this.store;
  }

  addSagas(sagas: any = {}): void {

    const _this = this;

    for (const [sagaName, saga] of Object.entries(sagas)) {

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

    for (const [reducerName, reducer] of Object.entries(reducers)) {

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

  dispatch(action): void {
    this.store.dispatch(action);
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

