import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { ApiClient } from '../shared/api-client';
import { urlEncodeBody, urlEncodedHeader } from '../../infrastructure/http-client';
import { actionTypes, ReduxEpic, ReduxEpicMap, ReduxReducer } from '../../infrastructure/redux-types';

export interface ICurrentSession {
    access_token?: string;
    isLoggedIn?: boolean;
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
    session: {},
};

export const accountReducer: ReduxReducer<IAccountState> = (state = initialState, action: any) => {

    switch (action.type) {

        case accountActions.LOGIN_REQUEST_SUCCESS: {

            return {
                ...state,
                session: {
                    access_token: action.response.body.access_token,
                    isLoggedIn: true,
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
                user: action.payload,
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

export const accountEpics = (httpClient: any, apiClient: ApiClient): ReduxEpicMap => {

    const loginEpic: ReduxEpic = (action$: any): any => action$.ofType(accountActions.LOGIN_REQUEST_START)
        .switchMap((action: any) => {
            return httpClient.post('/connect/token', urlEncodeBody({...action.user, grant_type: 'password'}), urlEncodedHeader)
                .switchMap((loginResponse: any) => {

                    if (loginResponse.networkError || loginResponse.body.error) {
                        return {loginResponse};
                    }

                    return apiClient
                        .profile(loginResponse.body.access_token)
                        .map(profileResponse => ({
                            loginResponse,
                            profileResponse,
                        }));
                });
        })
        .map((responses: any) => {

            const {loginResponse, profileResponse} = responses;

            if (loginResponse.networkError || loginResponse.body.error) {
                return {type: accountActions.LOGIN_REQUEST_FAILED, responses};
            } else {
                return {type: accountActions.LOGIN_REQUEST_SUCCESS, responses};
            }
        });

    return {
        loginEpic,

    };
};

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
