export const actionTypes = (actionPrefix: string) => ({
  ofType: <T>() => new Proxy({}, {get: (target: any, name: string) => actionPrefix + '/' + name}) as T,
});

export type ReduxReducer<TState = any> = (state: TState, action: any) => TState;

export interface ReduxSaga {
  type: string;
  saga: (action: string) => Iterator<any>;
}

export interface ReduxReducerMap {
  [key: string]: ReduxReducer<any>;
}

export interface ReduxSagaMap {
  [key: string]: ReduxSaga;
}
