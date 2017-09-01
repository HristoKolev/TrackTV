import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { ApiClient, ApiResponse } from '../shared/api-client';
import { actionTypes, ReduxEpic, ReduxEpicMap, ReduxReducer } from '../../infrastructure/redux-types';
import { globalActions } from '../global.state';

export interface IAccountState {

    errorMessages?: string[];
}

export const accountActions = actionTypes('account').ofType<{
    LOGIN_REQUEST_START: string;
    LOGIN_REQUEST_SUCCESS: string;
    LOGIN_REQUEST_FAILED: string;
}>();

const initialState = {
    errorMessages: [],
};

export const accountReducer: ReduxReducer<IAccountState> = (state = initialState, action: any) => {

    switch (action.type) {

        case accountActions.LOGIN_REQUEST_SUCCESS: {

            return {
                ...state,
                errorMessages: [],
            };
        }
        case accountActions.LOGIN_REQUEST_FAILED: {

            let errorMessages: string[] = [];

            if (!action.responses.loginResponse.success) {

                errorMessages = action.responses.loginResponse.errorMessages;
            } else if (!action.responses.profileResponse.success) {
                errorMessages = action.responses.profileResponse.errorMessages;
            }

            return {
                ...state,
                errorMessages,
            };
        }
        default: {
            return state;
        }
    }
};

export const accountEpics = (httpClient: any, apiClient: ApiClient): ReduxEpicMap => {

    const loginEpic: ReduxEpic = (action$: any): any => {

        return action$.ofType(accountActions.LOGIN_REQUEST_START)
            .switchMap((action: any) => apiClient.login(action.user)
                .switchMap((loginResponse: ApiResponse): any => {

                    if (!loginResponse.success) {
                        return [{loginResponse}];
                    }

                    return apiClient.profile(loginResponse.payload.access_token)
                        .map(profileResponse => ({loginResponse, profileResponse}));
                }))
            .switchMap((responses: any) => {

                const {loginResponse, profileResponse} = responses;

                if (!loginResponse.success || !profileResponse.success) {
                    return [{type: accountActions.LOGIN_REQUEST_FAILED, responses}];
                } else {
                    return [
                        {type: accountActions.LOGIN_REQUEST_SUCCESS, responses},
                        {type: globalActions.USER_LOGIN, responses},
                    ];
                }
            });
    };

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
