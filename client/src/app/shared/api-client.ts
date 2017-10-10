import { FetchResponse, httpClient, multipartForm, urlEncodeBody, urlEncodedHeader } from '../../infrastructure/http-client';
import { reduxState } from '../../infrastructure/redux-store';
import { Promise } from 'es6-promise';

export interface ApiResponse {
    errorMessages: string[];
    payload?: any;
    success: boolean;
}

const networkIsDownMessage = 'Network error. Please, try again later.';
const serverErrorMessage = 'Server error. Please, try again later.';

const errorResponse = (errorMessages: string[]) => ({errorMessages, success: false} as ApiResponse);
const successResponse = (payload: any) => ({payload, errorMessages: [], success: true} as ApiResponse);

export class ApiClient {

    public profile(token: string): Promise<ApiResponse> {

        return httpClient.get('/api/user/profile', {'Authorization': `Bearer ${token}`})
            .then(this.parseResponse);
    }

    public login(userLogin: any): Promise<ApiResponse> {

        return httpClient.post('/connect/token', urlEncodeBody({...userLogin, grant_type: 'password'}), urlEncodedHeader)
            .then(response => {
                if (response.networkError) {
                    return errorResponse([networkIsDownMessage]);
                } else if (response.body.error) {
                    return errorResponse([response.body.error_description]);
                } else {
                    return successResponse(response.body);
                }
            });
    }

    public register(user: any): Promise<ApiResponse> {

        const form = multipartForm(user);

        return httpClient.post('/api/public/auth/register', form.body, form.headers)
            .then(this.parseResponse);
    }

    public topShows(page: number): Promise<FetchResponse> {

        return httpClient.get(`/api/public/shows/top/${page}/${this.showsPageSize}`);
    }

    public searchShows(query: string, page: number): Promise<FetchResponse> {

        return httpClient.get(`/api/public/shows/search/${query}/${page}/${this.showsPageSize}`);
    }

    public getGenres(): Promise<FetchResponse> {

        return httpClient.get(`/api/public/genres`);
    }

    public showsByGenre(genreId: number, page: number): Promise<FetchResponse> {


        return httpClient.get(`/api/public/shows/genre/${genreId}/${page}/${this.showsPageSize}`);
    }

    private get showsPageSize() {
        return reduxState.getState().settings.showsPageSize;
    }

    private parseResponse(response: FetchResponse): ApiResponse {

        if (response.networkError) {
            return errorResponse([networkIsDownMessage]);
        }

        if (!response.body) {
            return errorResponse([serverErrorMessage]);
        }

        return response.body;
    }

}

export const apiClient = new ApiClient();

export const triggerAction = (successActionType: string, failureActionType: string, response: FetchResponse): any => {

    if (response.networkError) {

        return {type: failureActionType, errorMessages: [networkIsDownMessage]};
    }

    if (!response.body.success) {

        return {type: failureActionType, errorMessages: response.body.errorMessages};
    }

    return {type: successActionType, payload: response.body.payload};
};
