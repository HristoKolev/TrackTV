import { accountActions, accountReducer } from './account-state';

describe('accountReducer', () => {

    it('should add two numbers', () => {

        const prevState = {key: '__VALUE__'};

        const accessToken = '__ACCESS_TOKEN__';

        const action = {
            type: accountActions.LOGIN_REQUEST_SUCCESS,
            response: {
                body: {
                    access_token: accessToken,
                },
            },
        };

        const newState = accountReducer(prevState, action) as any;

        expect(newState.key).toBe(prevState.key);
        expect(newState).not.toBe(prevState);
    });
});
