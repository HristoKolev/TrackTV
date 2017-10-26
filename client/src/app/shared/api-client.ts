import { FetchResponse, httpClient } from '../../infrastructure/http-client';
import { reduxStore } from '../../infrastructure/redux-store';
import { Promise } from 'es6-promise';
import { globalActions, go } from '../global.state';

export interface ApiResponse {
    errorMessages: string[];
    payload?: any;
    success: boolean;
}

const networkIsDownMessage = 'Network error. Please, try again later.';
const serverErrorMessage = 'Server error. Please, try again later.';
const loggedOutMessage = 'You are not logged in.';

const errorResponse = (errorMessages: string[]) => ({errorMessages, success: false} as ApiResponse);
const successResponse = (payload: any) => ({payload, errorMessages: [], success: true} as ApiResponse);

export class ApiClient {

    public profile(token: string): Promise<ApiResponse> {

        return httpClient.get('/api/user/profile', {'Authorization': `Bearer ${token}`})
            .then(this.parseResponse);
    }

    public login(userLogin: any): Promise<ApiResponse> {

        return httpClient.post('/api/public/auth/login', JSON.stringify(userLogin))
            .then(this.parseResponse);
    }

    public register(user: any): Promise<ApiResponse> {

        return httpClient.post('/api/public/auth/register', JSON.stringify(user))
            .then(this.parseResponse);
    }

    public topShows(page: number): Promise<ApiResponse> {

        return httpClient.get(`/api/public/shows/top/${page}/${this.showsPageSize}`)
            .then(this.parseResponse);
    }

    public searchShows(query: string, page: number): Promise<ApiResponse> {

        return httpClient.get(`/api/public/shows/search/${query}/${page}/${this.showsPageSize}`)
            .then(this.parseResponse);
    }

    public getGenres(): Promise<ApiResponse> {

        return httpClient.get(`/api/public/genres`)
            .then(this.parseResponse);
    }

    public showsByGenre(genreId: number, page: number): Promise<ApiResponse> {

        return httpClient.get(`/api/public/shows/genre/${genreId}/${page}/${this.showsPageSize}`)
            .then(this.parseResponse);
    }

    public show(showId: number): Promise<ApiResponse> {
        return httpClient.get(`/api/public/show/${showId}`)
            .then(this.parseResponse);
    }

    public subscribe(showId: number): Promise<ApiResponse> {
        return httpClient.put(`/api/user/subscription/${showId}`, {})
            .then(this.parseResponse);
    }

    public myShows(): Promise<ApiResponse> {
        return httpClient.get(`/api/user/myshows`)
            .then(this.parseResponse);
    }

    public unsubscribe(showId: number): Promise<ApiResponse> {
        return httpClient.del(`/api/user/subscription/${showId}`, {})
            .then(this.parseResponse);
    }

    public calendar(): Promise<ApiResponse> {
        return httpClient.get(`/api/user/calendar`)
            .then(this.parseResponse);
    }

    private get showsPageSize() {
        return reduxStore.getState().settings.showsPageSize;
    }

    private parseResponse(response: FetchResponse): ApiResponse {

        if (response.networkError) {
            return errorResponse([networkIsDownMessage]);
        }

        if (response.status === 401) {

            reduxStore.dispatch({type: globalActions.LOGOUT_USER});
            go(['/shows']);
            return errorResponse([loggedOutMessage]);
        }

        if (!response.body) {
            return errorResponse([serverErrorMessage]);
        }

        return response.body;
    }

}

export const apiClient = new ApiClient();

export const triggerAction = (successActionType: string, failureActionType: string, response: ApiResponse, rest: any = {}): any => {

    if (response.success) {

        return {type: successActionType, payload: response.payload, ...rest};
    }

    return {type: failureActionType, errorMessages: response.errorMessages};
};
