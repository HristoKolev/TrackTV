import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { addReducers } from '../store';

export interface IAccountState {
}

export interface LoginUser {
    username: string;
    password: string;
}

const initialState: IAccountState = {};

const actionTypes = {
    LOGIN: 'account/LOGIN',
};

export const accountReducer = (state: IAccountState = initialState, action: any): IAccountState => {
    switch (action.type) {
        case actionTypes.LOGIN:
            return state;

        default: {
            return state;
        }
    }
};

@Injectable()
export class AccountActions {

    constructor(private ngRedux: NgRedux<any>) {
    }

    login(user: LoginUser) {

        this.ngRedux.dispatch({
            type: actionTypes.LOGIN,
            user,
        });
    }
}

addReducers({
    account: accountReducer,
});
