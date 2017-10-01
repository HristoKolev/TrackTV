export const actionTypes = (actionPrefix: string) => ({
    ofType: <T>() => new Proxy({}, {
        get: (target: any, name: string) => actionPrefix + '/' + name,
    }) as T,
});

export type ReduxReducer<TState = any> = (state: TState, action: any) => TState;
export type ReduxReducerMap = { [key: string]: ReduxReducer<any> };

export type ReduxMetaReducer<TState> = (reducer: ReduxReducer<TState>) => ReduxReducer<TState>;
export type ReduxMetaReducerMap = { [key: string]: ReduxMetaReducer<any> };

