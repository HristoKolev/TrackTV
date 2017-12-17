import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {reduxStore} from '../redux-store';

@Injectable()
export class ReduxStoreService {

  select<T = any>(selector: (state: any) => any = f => f): Observable<T> {
    return reduxStore.select(selector);
  }

  dispatch(...args: any[]): void {
    reduxStore.dispatch(...args);
  }

  getState(): any {
    return reduxStore.getState();
  }
}
