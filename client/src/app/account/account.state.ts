import {ApiClient} from '../shared/api-client';
import {put} from 'redux-saga/effects';
import {ReduxReducer, ReduxSagaMap} from '../../infrastructure/redux/meta';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {Injectable} from '@angular/core';
import {globalActions} from '../../infrastructure/redux/redux-global-actions';
import {routerActions} from '../../infrastructure/redux/redux-router-service';

export interface ILoginState {

  errorMessages?: string[];
}

export interface IRegisterState {

  errorMessages?: string[];
}

export const actions = {
  LOGIN_REQUEST_START: 'account/LOGIN_REQUEST_START',
  LOGIN_REQUEST_SUCCESS: 'account/LOGIN_REQUEST_SUCCESS',
  LOGIN_REQUEST_FAILED: 'account/LOGIN_REQUEST_FAILED',
  LOGIN_CLEAR_ERROR_MESSAGES: 'account/LOGIN_CLEAR_ERROR_MESSAGES',

  REGISTER_REQUEST_START: 'account/REGISTER_REQUEST_START',
  REGISTER_REQUEST_SUCCESS: 'account/REGISTER_REQUEST_SUCCESS',
  REGISTER_REQUEST_FAILED: 'account/REGISTER_REQUEST_FAILED',
  REGISTER_CLEAR_ERROR_MESSAGES: 'account/REGISTER_CLEAR_ERROR_MESSAGES',
};

@Injectable()
export class AccountActions {

  constructor(private store: ReduxStoreService) {
  }

  login(user: any) {
    this.store.dispatch({type: actions.LOGIN_REQUEST_START, user});
  }

  register(user: any) {
    this.store.dispatch({type: actions.REGISTER_REQUEST_START, user});
  }

  clearLoginErrorMessages() {
    this.store.dispatch({type: actions.LOGIN_CLEAR_ERROR_MESSAGES});
  }

  clearRegisterErrorMessages() {
    this.store.dispatch({type: actions.REGISTER_CLEAR_ERROR_MESSAGES});
  }
}

const initialLoginState = {
  errorMessages: [],
};

export const loginReducer: ReduxReducer<ILoginState> = (state = initialLoginState, action: any) => {

  switch (action.type) {

    case actions.LOGIN_CLEAR_ERROR_MESSAGES:
    case actions.LOGIN_REQUEST_START:
    case actions.LOGIN_REQUEST_SUCCESS: {

      return {
        ...state,
        errorMessages: [],
      };
    }
    case actions.LOGIN_REQUEST_FAILED: {

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

const initialRegisterState = {
  errorMessages: [],
};

export const registerReducer: ReduxReducer<IRegisterState> = (state = initialRegisterState, action: any) => {

  switch (action.type) {

    case actions.REGISTER_CLEAR_ERROR_MESSAGES:
    case actions.REGISTER_REQUEST_START:
    case actions.REGISTER_REQUEST_SUCCESS: {
      return {
        ...state,
        errorMessages: [],
      };
    }
    case actions.REGISTER_REQUEST_FAILED: {

      return {
        ...state,
        errorMessages: action.response.errorMessages,
      };
    }

    default: {
      return state;
    }
  }
};

export const accountSagas = (apiClient: ApiClient): ReduxSagaMap => ({
  loginSaga: {
    type: actions.LOGIN_REQUEST_START,
    saga: function* (action: any) {

      yield  put({type: globalActions.START_TRANSITION});

      const loginResponse = yield apiClient.login(action.user);

      if (!loginResponse.success) {

        yield put({type: actions.LOGIN_REQUEST_FAILED, responses: {loginResponse}});
        yield put({type: globalActions.END_TRANSITION});
        return;
      }

      const profileResponse = yield apiClient.profile(loginResponse.payload.token);

      const responses = {loginResponse, profileResponse};

      if (!profileResponse.success) {

        yield put({type: actions.LOGIN_REQUEST_FAILED, responses});

      } else {

        yield put({type: actions.LOGIN_REQUEST_SUCCESS, responses});
        yield put({type: globalActions.LOGIN_USER, responses});
        yield put({type: routerActions.ROUTER_NAVIGATION_EXPLICIT, payload: [['/shows']]});
      }

      yield put({type: globalActions.END_TRANSITION});
    },
  },
  registerSaga: {
    type: actions.REGISTER_REQUEST_START,
    saga: function* (action: any) {

      yield put({type: globalActions.START_TRANSITION});

      const response = yield apiClient.register(action.user);

      if (!response.success) {

        yield put({type: actions.REGISTER_REQUEST_FAILED, response});
        yield put({type: globalActions.END_TRANSITION});
        return;
      }

      yield put({type: actions.REGISTER_REQUEST_SUCCESS, response});

      yield put({type: actions.LOGIN_REQUEST_START, user: action.user});
      yield put({type: globalActions.END_TRANSITION});
    },
  },
});

