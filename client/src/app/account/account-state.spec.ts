import { accountActions, accountEpics, accountReducer } from './account-state';
import { ActionsObservable } from 'redux-observable';
import { Observable } from 'rxjs';
import { testReducer, testReducerWithAction } from '../../infrastructure/test-helpers';

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
            payload: user,
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

describe('accountEpics', () => {

    describe('loginEpic', () => {

        it('returns correct action when the request is successful', (done) => {

            const response = {body: {}};

            const httpClient = {
                post: jest.fn(() => (Observable.from([response]))),
            };

            const {loginEpic} = accountEpics(httpClient, {});

            const inputAction = {type: accountActions.LOGIN_REQUEST_START, user: {username: 'cat', password: 'dog'}};

            const errorCb = jest.fn();

            let action;

            loginEpic(ActionsObservable.from([inputAction]), {}).subscribe((a: any) => action = a, errorCb, done);

            const outputAction = {type: accountActions.LOGIN_REQUEST_SUCCESS, response};

            expect(action).not.toBeUndefined();

            expect(action).toEqual(outputAction);

            expect(errorCb).not.toBeCalled();

            expect(httpClient.post).toBeCalledWith(
                '/connect/token',
                `username=cat&password=dog&grant_type=password`,
                {'Content-Type': 'application/x-www-form-urlencoded'},
            );
        });

        it('returns correct action when the network is down', (done) => {

            const response = {networkError: true};

            const httpClient = {
                post: jest.fn(() => (Observable.from([response]))),
            };

            const {loginEpic} = accountEpics(httpClient, {});

            const inputAction = {type: accountActions.LOGIN_REQUEST_START, user: {username: 'cat', password: 'dog'}};

            const errorCb = jest.fn();

            let action;

            loginEpic(ActionsObservable.from([inputAction]), {}).subscribe((a: any) => action = a, errorCb, done);

            const outputAction = {type: accountActions.LOGIN_REQUEST_FAILED, response};

            expect(action).not.toBeUndefined();

            expect(action).toEqual(outputAction);

            expect(errorCb).not.toBeCalled();

            expect(httpClient.post).toBeCalledWith(
                '/connect/token',
                `username=cat&password=dog&grant_type=password`,
                {'Content-Type': 'application/x-www-form-urlencoded'},
            );
        });

        it('returns correct action when the server returns an error', (done) => {

            const response = {body: {error: {}}};

            const httpClient = {
                post: jest.fn(() => (Observable.from([response]))),
            };

            const {loginEpic} = accountEpics(httpClient, {});

            const inputAction = {type: accountActions.LOGIN_REQUEST_START, user: {username: 'cat', password: 'dog'}};

            const errorCb = jest.fn();

            let action;

            loginEpic(ActionsObservable.from([inputAction]), {}).subscribe((a: any) => action = a, errorCb, done);

            const outputAction = {type: accountActions.LOGIN_REQUEST_FAILED, response};

            expect(action).not.toBeUndefined();

            expect(action).toEqual(outputAction);

            expect(errorCb).not.toBeCalled();

            expect(httpClient.post).toBeCalledWith(
                '/connect/token',
                `username=cat&password=dog&grant_type=password`,
                {'Content-Type': 'application/x-www-form-urlencoded'},
            );
        });
    });

    describe('profileEpic', () => {

        it('returns correct action when the network is down', (doneCb) => {

            const apiClient = {
                profile: jest.fn(() => (Observable.from([{
                    networkError: true,
                }]))),
            };

            const {profileEpic} = accountEpics({}, apiClient);

            const inputAction = {type: accountActions.LOGIN_REQUEST_SUCCESS};

            const errorCb = jest.fn();

            let resultAction;

            profileEpic(ActionsObservable.from([inputAction]), {})
                .subscribe((a: any) => resultAction = a, errorCb, doneCb);

            expect(resultAction).not.toBeUndefined();

            expect(resultAction).toEqual({
                type: accountActions.PROFILE_REQUEST_FAILED,
                errorMessages: ['Server is down. Please, try again later.'],
            });

            expect(errorCb).not.toBeCalled();

            expect(apiClient.profile).toBeCalled();
        });

        it('returns correct action when the server returns an error', (doneCb) => {

            const errorMessages = ['Error message'];

            const apiClient = {
                profile: jest.fn(() => (Observable.from([{
                    body: {errorMessages},
                }]))),
            };

            const {profileEpic} = accountEpics({}, apiClient);

            const inputAction = {type: accountActions.LOGIN_REQUEST_SUCCESS};

            const errorCb = jest.fn();

            let resultAction;

            profileEpic(ActionsObservable.from([inputAction]), {})
                .subscribe((a: any) => resultAction = a, errorCb, doneCb);

            expect(resultAction).not.toBeUndefined();

            expect(resultAction).toEqual({
                type: accountActions.PROFILE_REQUEST_FAILED,
                errorMessages,
            });

            expect(errorCb).not.toBeCalled();

            expect(apiClient.profile).toBeCalled();
        });

        it('returns correct action when the request is successful', (doneCb) => {

            const payload = {key: '__VALUE__'};

            const apiClient = {
                profile: jest.fn(() => (Observable.from([{
                    body: {payload, success: true},
                }]))),
            };

            const {profileEpic} = accountEpics({}, apiClient);

            const inputAction = {type: accountActions.LOGIN_REQUEST_SUCCESS};

            const errorCb = jest.fn();

            let resultAction;

            profileEpic(ActionsObservable.from([inputAction]), {})
                .subscribe((a: any) => resultAction = a, errorCb, doneCb);

            expect(resultAction).not.toBeUndefined();

            expect(resultAction).toEqual({
                type: accountActions.PROFILE_REQUEST_SUCCESS,
                payload,
            });

            expect(errorCb).not.toBeCalled();

            expect(apiClient.profile).toBeCalled();
        });
    });
});
