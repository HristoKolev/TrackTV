import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {ReduxReducerMap, SubscriptionStrategy} from './meta';
import {reduxStore} from './redux-store';
import {IReduxState} from './redux-state';

@Injectable()
export class ReduxStoreService {

  select<T>(selector: (state: IReduxState) => T, subscriptionStrategy: SubscriptionStrategy = 'NotEmpty'): Observable<T> {
    return reduxStore.select(selector, subscriptionStrategy);
  }

  dispatch(action): void {
    reduxStore.dispatch(action);
  }

  getState(): IReduxState {
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



