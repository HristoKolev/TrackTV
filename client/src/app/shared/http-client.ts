import {Injectable} from '@angular/core';
import {ReduxStoreService} from '../../infrastructure/redux/redux-store-service';

export interface FetchResponse {
  status: number;
  body: any;
  headers: any;
  networkError: boolean;
}

@Injectable()
export class HttpClient {

  constructor(private store: ReduxStoreService) {
  }

  get(url: string, headers: any = {}): Promise<FetchResponse> {

    const options = {
      method: 'get',
      headers: {
        ...this.defaultHeaders,
        ...(headers || {}),
      },
    };

    return fetch(url, options)
      .then(this.parseResponse, this.handleError);
  }

  post(url: string, body: any, headers?: any): Promise<FetchResponse> {

    const options = {
      method: 'post',
      headers: {
        ...this.defaultHeaders,
        ...(headers || {}),
        'Content-Type': 'application/json',
      },
      body,
    };

    return fetch(url, options)
      .then(this.parseResponse, this.handleError);
  }

  put(url: string, body: any, headers?: any): Promise<FetchResponse> {

    const options = {
      method: 'put',
      headers: {
        ...this.defaultHeaders,
        ...(headers || {}),
      },
      body,
    };

    return fetch(url, options)
      .then(this.parseResponse, this.handleError);
  }

  del(url: string, body: any, headers?: any): Promise<FetchResponse> {

    const options = {
      method: 'delete',
      headers: {
        ...this.defaultHeaders, ...(headers || {}),
      },
      body,
    };

    return fetch(url, options)
      .then(this.parseResponse, this.handleError);
  }

  handleError(error: any) {
    return {networkError: true};
  }

  parseResponse(res: any): any {

    return res.json()
      .then((body: any) => ({
        status: res.status,
        body,
        headers: Array.from(res.headers.entries())
          .reduce((acc: any, x: any) => {
            acc[x[0]] = x[1];
            return acc;
          }, {}),
        networkError: false,
      }), (err: any) => ({
        status: res.status,
        headers: Array.from(res.headers.entries())
          .reduce((acc: any, x: any) => {
            acc[x[0]] = x[1];
            return acc;
          }, {}),
        networkError: false,
      }));
  }

  private get defaultHeaders(): any {

    const headers: any = {};

    const state = this.store.getState();

    let token;

    if (state.session) {

      token = state.session.token;
    }

    if (token) {
      headers['Authorization'] = `JWT ${token}`;
    }

    return headers;
  }

}
