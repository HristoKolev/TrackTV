import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { apiClient, triggerAction } from '../shared/api-client';
import { httpClient, urlEncodeBody, urlEncodedHeader } from '../../infrastructure/http-client';
import { actionTypes, ReduxEpic, ReduxReducer } from '../../infrastructure/redux-types';

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

export const accountActions = actionTypes('account').ofType<{
    LOGIN_REQUEST_START: string;
    LOGIN_REQUEST_SUCCESS: string;
    LOGIN_REQUEST_FAILED: string;

    PROFILE_REQUEST_SUCCESS: string;
    PROFILE_REQUEST_FAILED: string;
}>();

const initialState = {
    errorMessages: [],
};

export const accountReducer: ReduxReducer<IAccountState> = (state = initialState, action: any) => {

    switch (action.type) {

        case accountActions.LOGIN_REQUEST_SUCCESS: {
            return {
                ...state,
                session: {
                    access_token: action.response.body.access_token,
                },
                errorMessages: [],
            };
        }
        case accountActions.LOGIN_REQUEST_FAILED: {

            return {
                ...state,
                errorMessages: [
                    ...(action.response.networkError ? ['Server is down. Please, try again later.'] : []),
                    ...(action.response.body ? [action.response.body.error_description] : [])
                ],
            };
        }
        case accountActions.PROFILE_REQUEST_SUCCESS: {

            return {
                ...state,
                user: action.data,
                errorMessages: [],
            };
        }
        case accountActions.PROFILE_REQUEST_FAILED: {

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

export const loginEpic: ReduxEpic = (action$: any): any => action$.ofType(accountActions.LOGIN_REQUEST_START)
    .switchMap((action: any) => httpClient.post('/connect/token', urlEncodeBody({
        ...action.user,
        grant_type: 'password',
    }), urlEncodedHeader))
    .map((response: any) => {
        if (response.networkError || response.body.error) {
            return {type: accountActions.LOGIN_REQUEST_FAILED, response};
        } else {
            return {type: accountActions.LOGIN_REQUEST_SUCCESS, response};
        }
    });

export const profileEpic: ReduxEpic = (action$: any): any => action$.ofType(accountActions.LOGIN_REQUEST_SUCCESS)
    .switchMap((action: any) => apiClient.profile())
    .map(triggerAction(accountActions.PROFILE_REQUEST_SUCCESS, accountActions.PROFILE_REQUEST_FAILED));

export interface UserLogin {
    username: string;
    password: string;
}

@Injectable()
export class AccountActions {

    constructor(private ngRedux: NgRedux<any>) {
    }

    login(user: UserLogin) {
        this.ngRedux.dispatch({type: accountActions.LOGIN_REQUEST_START, user});
    }
}
