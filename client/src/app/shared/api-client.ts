import { Action } from 'redux';
import { Observable } from 'rxjs/Observable';
import { FetchResponse, httpClient } from '../../infrastructure/http-client';
import { reduxState } from '../../infrastructure/redux-store';
import { ActionsObservable } from 'redux-observable';
import { globalActions } from '../global.state';
import { ReduxEpic } from '../../infrastructure/redux-types';

export class ApiClient {

    public profile(token: string): Observable<FetchResponse> {
        return httpClient.get('/api/user/profile', {'Authorization': `Bearer ${token}`});
    }

    public topShows(page: number): Observable<FetchResponse> {

        const showsPageSize = reduxState.getState().settings.showsPageSize;

        return httpClient.get(`/api/public/shows/top/${page}/${showsPageSize}`);
    }
}

export const apiClient = new ApiClient();

export type ActionSelector = (response: FetchResponse) => Action;

export const triggerAction = (successActionType: string, failureActionType: string): ActionSelector => {

    return (response => {

        if (response.networkError) {

            return {type: failureActionType, errorMessages: ['Server is down. Please, try again later.']};
        }

        if (!response.body.success) {

            return {type: failureActionType, errorMessages: response.body.errorMessages};
        }

        return {type: successActionType, payload: response.body.payload};
    });
};

export const createApiEpic = (startActionType: string, successActionType: string, apiCall: (action: any) => Observable<any>): ReduxEpic => {
    return (actions$: ActionsObservable<any>, store: any) => {
        return actions$
            .ofType(startActionType)
            .switchMap(apiCall)
            .map(triggerAction(successActionType, globalActions.GLOBAL_ERROR));
    };
};
