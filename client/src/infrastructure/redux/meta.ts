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

export type SubscriptionStrategy = 'NotEmpty' | 'Normal';
