import {globalActions} from '../../infrastructure/redux/redux-global-actions';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';
import {Injectable} from '@angular/core';
import {go} from '../../infrastructure/redux/redux-router-service';
import {FetchResponse, HttpClient} from './http-client';

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

@Injectable()
export class ApiClient {

  constructor(private store: ReduxStoreService,
              private httpClient: HttpClient) {
  }

  public profile(token: string): Promise<ApiResponse> {

    return this.httpClient.get('/api/user/profile', {'Authorization': `Bearer ${token}`})
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  public login(userLogin: any): Promise<ApiResponse> {

    return this.httpClient.post('/api/public/auth/login', JSON.stringify(userLogin))
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  public register(user: any): Promise<ApiResponse> {

    return this.httpClient.post('/api/public/auth/register', JSON.stringify(user))
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  public shows(query: any): Promise<ApiResponse> {

    const requestBody = {
      ...query,
      pageSize: this.showsPageSize,
      page: query.page || 1,
    };

    return this.httpClient.post(`/api/public/shows`, JSON.stringify(requestBody))
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  public getGenres(): Promise<ApiResponse> {

    return this.httpClient.get(`/api/public/genres`)
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  public show(showId: number): Promise<ApiResponse> {
    return this.httpClient.get(`/api/public/show/${showId}`)
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  public subscribe(showId: number): Promise<ApiResponse> {
    return this.httpClient.put(`/api/user/subscription/${showId}`, {})
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  public myShows(): Promise<ApiResponse> {
    return this.httpClient.get(`/api/user/myshows`)
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  public unsubscribe(showId: number): Promise<ApiResponse> {
    return this.httpClient.del(`/api/user/subscription/${showId}`, {})
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  public calendar(): Promise<ApiResponse> {
    return this.httpClient.get(`/api/user/calendar`)
      .then(this.parseResponse.bind(this) as () => ApiResponse);
  }

  private get showsPageSize() {
    return this.store.getState().settings.showsPageSize;
  }

  private parseResponse(response: FetchResponse): ApiResponse {

    if (response.networkError) {
      return errorResponse([networkIsDownMessage]);
    }

    if (response.status === 401) {

      this.store.dispatch({type: globalActions.LOGOUT_USER});
      this.store.dispatch(go(['/shows']));
      return errorResponse([loggedOutMessage]);
    }

    if (!response.body) {
      return errorResponse([serverErrorMessage]);
    }

    return response.body;
  }

}

export const triggerAction = (successActionType: string, failureActionType: string, response: ApiResponse, rest: any = {}): any => {

  if (response.success) {

    return {type: successActionType, payload: response.payload, ...rest};
  }

  return {type: failureActionType, errorMessages: response.errorMessages};
};
