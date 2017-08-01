import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { addEpics, addReducers } from '../store';
import { httpClient, urlEncodeBody, urlEncodedHeader } from '../shared/http-client';
import { apiClient, triggerAction } from '../shared/api-client';

export interface ICurrentSession {
    access_token: string;
}

export interface ICurrentUser {
    username?: string;
}

export interface IAccountState {
    session?: ICurrentSession;
    user?: ICurrentUser;
    errorMessages?: string[];
}

export interface UserLogin {
    username: string;
    password: string;
}

const actionTypes = {
    LOGIN_REQUEST_START: 'account/LOGIN_REQUEST_START',
    LOGIN_REQUEST_SUCCESS: 'account/LOGIN_REQUEST_SUCCESS',
    LOGIN_REQUEST_FAILED: 'account/LOGIN_REQUEST_FAILED',

    PROFILE_REQUEST_SUCCESS: 'account/PROFILE_REQUEST_SUCCESS',
    PROFILE_REQUEST_FAILED: 'account/PROFILE_REQUEST_FAILED',
};

const initialState = {
    errorMessages: [],
};

export const accountReducer = (state: IAccountState = initialState as IAccountState, action: any): IAccountState => {

    switch (action.type) {

        case actionTypes.LOGIN_REQUEST_SUCCESS: {

            console.log('LOGIN_REQUEST_SUCCESS');

            return {
                ...state,
                session: {
                    access_token: action.response.body.access_token,
                },
            };
        }
        case actionTypes.LOGIN_REQUEST_FAILED: {

            return {
                ...state,
                errorMessages: [
                    ...(action.response.networkError ? ['Server is down. Please, try again later.'] : []),
                    ...(action.response.body ? [action.response.body.error_description] : [])
                ],
            };
        }
        case actionTypes.PROFILE_REQUEST_SUCCESS: {

            return {
                ...state,
                user: action.data,
            };
        }
        case actionTypes.PROFILE_REQUEST_FAILED: {

            return {
                ...state,
                errorMessages: action.errorMessages,
            };
        }
        default: {
            return state;
        }
    }
};

export const loginEpic = (action$: any): any => action$.ofType(actionTypes.LOGIN_REQUEST_START)
    .switchMap((action: any) => httpClient.post('/connect/token', urlEncodeBody({
        ...action.user,
        grant_type: 'password',
    }), urlEncodedHeader))
    .map((response: any) => {
        if (response.networkError || response.body.error) {
            return {type: actionTypes.LOGIN_REQUEST_FAILED, response};
        } else {
            return {type: actionTypes.LOGIN_REQUEST_SUCCESS, response};
        }
    });

export const profileEpic = (action$: any): any => action$.ofType(actionTypes.LOGIN_REQUEST_SUCCESS)
    .switchMap((action: any) => apiClient.profile())
    .map(triggerAction(actionTypes.PROFILE_REQUEST_SUCCESS, actionTypes.PROFILE_REQUEST_FAILED));

@Injectable()
export class AccountActions {

    constructor(private ngRedux: NgRedux<any>) {
    }

    login(user: UserLogin) {
        this.ngRedux.dispatch({type: actionTypes.LOGIN_REQUEST_START, user});
    }
}

addReducers({
    account: accountReducer,
});

addEpics([
    loginEpic,
    profileEpic,
]);
