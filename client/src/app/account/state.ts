import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { addEpics, addReducers } from '../store';
import { client, networkAction, urlEncodeBody, urlEncodedHeader } from '../shared/http-client';

export interface ICurrentSession {
    access_token: string;
}

export interface ICurrentUser {
    username?: string;
}

export interface IAccountState {
    session?: ICurrentSession;
    user: ICurrentUser;
    errorMessages?: string[];
}

export interface UserLogin {
    username: string;
    password: string;
}

const actionTypes = {
    LOGIN_REQUEST_START: 'account/LOGIN_REQUEST_START',
    LOGIN_REQUEST_COMPLETED: 'account/LOGIN_REQUEST_COMPLETED',
    LOGIN_REQUEST_FAILED: 'account/LOGIN_REQUEST_FAILED',
};

export const accountReducer = (state: IAccountState = {} as IAccountState, action: any): IAccountState => {

    switch (action.type) {

        case actionTypes.LOGIN_REQUEST_COMPLETED: {
            const body = action.response.body;
            return {
                ...state,
                errorMessages: body.error ? [body.error_description] : [],
                session: {
                    access_token: body.access_token,
                },
                user: {},

            };
        }
        case actionTypes.LOGIN_REQUEST_FAILED: {
            const networkError = action.response.networkError;
            return {
                ...state, response: {
                    errorMessages: networkError ? ['Server is down. Please, try again later.'] : [],
                    success: false,
                },
            };
        }
        default: {
            return state;
        }
    }
};

export const loginEpic = (action$: any): any => action$.ofType(actionTypes.LOGIN_REQUEST_START)
    .switchMap((x: any) => client.post('/connect/token', urlEncodeBody({...x.user, grant_type: 'password'}), urlEncodedHeader))
    .map(networkAction(actionTypes.LOGIN_REQUEST_COMPLETED, actionTypes.LOGIN_REQUEST_FAILED));

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
]);
