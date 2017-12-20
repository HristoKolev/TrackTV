import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {ReduxReducerMap} from './meta';
import {reduxStore} from './redux-store';

@Injectable()
export class ReduxStoreService {

  select<T = any>(selector: (state: any) => any = f => f): Observable<T> {
    return reduxStore.select(selector);
  }

  dispatch(action): void {
    reduxStore.dispatch(action);
  }

  getState(): any {
    return reduxStore.getState();
  }

  initStore(enhancers: any[] = [], initialReducers?: ReduxReducerMap): void {
    return reduxStore.initStore(enhancers, initialReducers);
  }

  addSagas(sagas: any = {}): void {
    return reduxStore.addSagas(sagas);
  }

  addReducers(reducers: ReduxReducerMap = {}): void {
    return reduxStore.addReducers(reducers);
  }
}
