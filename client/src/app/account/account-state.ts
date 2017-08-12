import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { apiClient, triggerAction } from '../shared/api-client';
import { httpClient, urlEncodeBody, urlEncodedHeader } from '../../infrastructure/http-client';
import { actionTypes } from '../../infrastructure/redux-helpers';

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

export const sessionActions = actionTypes('account').ofType<{
    LOGIN_REQUEST_START: string;
    LOGIN_REQUEST_SUCCESS: string;
    LOGIN_REQUEST_FAILED: string;

    PROFILE_REQUEST_SUCCESS: string;
    PROFILE_REQUEST_FAILED: string;
}>();

const initialState = {
    errorMessages: [],
};

export const accountReducer = (state: IAccountState = initialState as IAccountState, action: any): IAccountState => {

    switch (action.type) {

        case sessionActions.LOGIN_REQUEST_SUCCESS: {

            return {
                ...state,
                session: {
                    access_token: action.response.body.access_token,
                },
            };
        }
        case sessionActions.LOGIN_REQUEST_FAILED: {

            return {
                ...state,
                errorMessages: [
                    ...(action.response.networkError ? ['Server is down. Please, try again later.'] : []),
                    ...(action.response.body ? [action.response.body.error_description] : [])
                ],
            };
        }
        case sessionActions.PROFILE_REQUEST_SUCCESS: {

            return {
                ...state,
                user: action.data,
            };
        }
        case sessionActions.PROFILE_REQUEST_FAILED: {

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

export const loginEpic = (action$: any): any => action$.ofType(sessionActions.LOGIN_REQUEST_START)
    .switchMap((action: any) => httpClient.post('/connect/token', urlEncodeBody({
        ...action.user,
        grant_type: 'password',
    }), urlEncodedHeader))
    .map((response: any) => {
        if (response.networkError || response.body.error) {
            return {type: sessionActions.LOGIN_REQUEST_FAILED, response};
        } else {
            return {type: sessionActions.LOGIN_REQUEST_SUCCESS, response};
        }
    });

export const profileEpic = (action$: any): any => action$.ofType(sessionActions.LOGIN_REQUEST_SUCCESS)
    .switchMap((action: any) => apiClient.profile())
    .map(triggerAction(sessionActions.PROFILE_REQUEST_SUCCESS, sessionActions.PROFILE_REQUEST_FAILED));

@Injectable()
export class AccountActions {

    constructor(private ngRedux: NgRedux<any>) {
    }

    login(user: UserLogin) {
        this.ngRedux.dispatch({type: sessionActions.LOGIN_REQUEST_START, user});
    }
}
