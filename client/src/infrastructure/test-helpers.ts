import { ReduxReducer } from './redux-types';

export const testReducer = (reducer: ReduxReducer) => {

    const unknownAction = Object.freeze({type: '@@UNKNOWN_ACTION'});

    it('should return some default value', () => {

        const newState = reducer(undefined, unknownAction);

        expect(newState).toBeTruthy();
    });

    it('should return old state when passed an unknown action', () => {

        const oldState = {};

        const newState = reducer(oldState, unknownAction);

        expect(newState).toBe(oldState);
    });
};

export const testReducerWithAction = (reducer: ReduxReducer, action: any) => {

    it('should preserve previous state when passed an action', () => {

        const oldState = {key: '__VALUE__'};

        const newState = reducer(oldState, action);

        expect(oldState.key).toBe(newState.key);
    });

    it('should return different state from the one that was passed', () => {

        const prevState = {};

        const newState = reducer(prevState, action);

        expect(newState).not.toBe(prevState);
    });
};
