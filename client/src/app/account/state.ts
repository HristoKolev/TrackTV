import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { addEpics, addReducers } from '../store';

export interface ICurrentSession {
    resource: string;
    token_type: string;
    access_token: string;
    expires_in: number;
    username: string;
}

export interface IAccountState {
}

export interface UserLogin {
    username: string;
    password: string;
}

const initialState: IAccountState = {};

const actionTypes = {
    LOGIN_REQUEST_START: 'account/LOGIN_REQUEST_START',
    LOGIN_REQUEST_COMPLETED: 'account/LOGIN_REQUEST_COMPLETED',
    LOGIN_REQUEST_FAILED: 'account/LOGIN_REQUEST_FAILED',

};

export const accountReducer = (state: IAccountState = initialState, action: any): IAccountState => {
    switch (action.type) {

        default: {
            return state;
        }
    }
};

export const loginEpic = (action$: any, state: { account: IAccountState }) => {

    console.log('mine cats');

    return action$.ofType('account/LOGIN_REQUEST_START')
        .switchMap((action: any) => fetch('http://192.168.1.103:7000/connect/token', {
            method: 'POST',
            headers: {'Content-Type': 'application/x-www-form-urlencoded'},
            body: Object.entries(action.user).map(p => p.join('=')).join('&'),
        }))
        .do(console.log)
        .map((res: any) => ({type: 'account/LOGIN_REQUEST_COMPLETED', res}))
        .catch((res: any) => ({type: 'account/LOGIN_REQUEST_FAILED', res}));
};

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

