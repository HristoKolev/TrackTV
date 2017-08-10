export const actionTypes = (actionPrefix: string) => ({
    ofType: <T>() => new Proxy({}, {
        get: (target: any, name: string) => actionPrefix + '/' + name,
    }) as T,
});
