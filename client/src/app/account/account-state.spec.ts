import { accountActions, accountReducer } from './account-state';
import { ReduxReducer } from '../../infrastructure/redux-types';

const testReducer = (reducer: ReduxReducer) => {

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

const testReducerWithAction = (reducer: ReduxReducer, action: any) => {

    it('should preserve previous state when passed an action', () => {

        const oldState = {key: '__VALUE__'};

        const newState = reducer(oldState, action);

        expect(oldState.key).toBe(newState.key);
    });

    it('should return different state from the one that was passed', () => {

        const prevState = {};

        const newState = accountReducer(prevState, action);

        expect(newState).not.toBe(prevState);
    });
};

describe('accountReducer', () => {

    testReducer(accountReducer);

    describe('Action: ' + accountActions.LOGIN_REQUEST_SUCCESS, () => {

        const accessToken = '__ACCESS_TOKEN__';

        const action = {
            type: accountActions.LOGIN_REQUEST_SUCCESS,
            response: {
                body: {
                    access_token: accessToken,
                },
            },
        };

        testReducerWithAction(accountReducer, action);

        it('adds the access token correctly', () => {

            const prevState = {};

            const newState = accountReducer(prevState, action) as any;

            expect(newState).toEqual({
                session: {
                    access_token: accessToken,
                },
                errorMessages: [],
            });
        });
    });

    describe('Action: ' + accountActions.LOGIN_REQUEST_FAILED, () => {

        const defaultAction = {
            type: accountActions.LOGIN_REQUEST_FAILED,
            response: {
                networkError: true,
            },
        };

        testReducerWithAction(accountReducer, defaultAction);

        it('adds correct error messages when the network is down', () => {

            const prevState = {};

            const action = {
                type: accountActions.LOGIN_REQUEST_FAILED,
                response: {
                    networkError: true,
                },
            };

            const newState = accountReducer(prevState, action) as any;

            expect(newState).toEqual({
                errorMessages: ['Server is down. Please, try again later.'],
            });
        });

        it('adds correct error messages when the server returns an error', () => {

            const prevState = {};

            const errorMessage = 'Error message';

            const action = {
                type: accountActions.LOGIN_REQUEST_FAILED,
                response: {
                    body: {
                        error_description: errorMessage,
                    },
                },
            };

            const newState = accountReducer(prevState, action) as any;

            expect(newState).toEqual({
                errorMessages: [errorMessage],
            });
        });
    });

    describe('Action: ' + accountActions.PROFILE_REQUEST_SUCCESS, () => {

        const user = {
            key: '__VALUE__',
        };

        const defaultAction = {
            type: accountActions.PROFILE_REQUEST_SUCCESS,
            data: user,
        };

        testReducerWithAction(accountReducer, defaultAction);

        it('passes the correct profile data', () => {

            const prevState = {};

            const newState = accountReducer(prevState, defaultAction) as any;

            expect(newState).toEqual({
                user,
                errorMessages: [],
            });
        });

    });

    describe('Action: ' + accountActions.PROFILE_REQUEST_FAILED, () => {

        const errorMessages = ['err1', 'err2'];

        const defaultAction = {
            type: accountActions.PROFILE_REQUEST_FAILED,
            errorMessages,
        };

        testReducerWithAction(accountReducer, defaultAction);

        it('add the correct error messages', () => {

            const prevState = {};

            const newState = accountReducer(prevState, defaultAction) as any;

            expect(newState).toEqual({

                errorMessages,
            });
        });
    });
});
