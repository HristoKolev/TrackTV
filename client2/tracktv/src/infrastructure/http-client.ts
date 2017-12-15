import {reduxStore} from './redux-store';

export interface FetchResponse {
  status: number;
  body: any;
  headers: any;
  networkError: boolean;
}

export const createHttpClient = (getBaseUrl: () => string, getDefaultHeaders: () => any) => {

  const parseResponse = (res: any): any => {

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
  };

  const handleError = (error: any) => ({networkError: true});

  return {
    get(url: string, headers: any = {}): Promise<FetchResponse> {

      const options = {
        method: 'get',
        headers: {
          ...getDefaultHeaders(),
          ...(headers || {})
        },
      };

      return fetch(getBaseUrl() + url, options)
        .then(parseResponse, handleError);
    },
    post(url: string, body: any, headers?: any): Promise<FetchResponse> {

      const options = {
        method: 'post',
        headers: {
          ...getDefaultHeaders(),
          ...(headers || {}),
          'Content-Type': 'application/json'
        },
        body,
      };

      return fetch(getBaseUrl() + url, options)
        .then(parseResponse, handleError);
    },
    put(url: string, body: any, headers?: any): Promise<FetchResponse> {

      const options = {
        method: 'put',
        headers: {
          ...getDefaultHeaders(),
          ...(headers || {})
        },
        body,
      };

      return fetch(getBaseUrl() + url, options)
        .then(parseResponse, handleError);
    },
    del(url: string, body: any, headers?: any): Promise<FetchResponse> {

      const options = {
        method: 'delete',
        headers: {
          ...getDefaultHeaders(),
          ...(headers || {})
        },
        body,
      };

      return fetch(getBaseUrl() + url, options)
        .then(parseResponse, handleError);
    }
  };
};

class HttpClient {

  public get baseUrl(): string {

    return reduxStore.getState().settings.baseUrl;
  }

  get(url: string, headers: any = {}): Promise<FetchResponse> {

    const options = {
      method: 'get',
      headers: {
        ...this.defaultHeaders,
        ...(headers || {})
      },
    };

    return fetch(this.baseUrl + url, options)
      .then(this.parseResponse, this.handleError);
  }

  post(url: string, body: any, headers?: any): Promise<FetchResponse> {

    const options = {
      method: 'post',
      headers: {
        ...this.defaultHeaders,
        ...(headers || {}),
        'Content-Type': 'application/json'
      },
      body,
    };

    return fetch(this.baseUrl + url, options)
      .then(this.parseResponse, this.handleError);
  }

  put(url: string, body: any, headers?: any): Promise<FetchResponse> {

    const options = {
      method: 'put',
      headers: {
        ...this.defaultHeaders,
        ...(headers || {})
      },
      body,
    };

    return fetch(this.baseUrl + url, options)
      .then(this.parseResponse, this.handleError);
  }

  del(url: string, body: any, headers?: any): Promise<FetchResponse> {

    const options = {
      method: 'delete',
      headers: {
        ...this.defaultHeaders, ...(headers || {})
      },
      body,
    };

    return fetch(this.baseUrl + url, options)
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

    const state = reduxStore.getState();

    let token;

    if (state.session) {

      token = state.session.access_token;
    }

    if (token) {
      headers['Authorization'] = `Bearer ${token}`;
    }

    return headers;
  }

}

export const httpClient = new HttpClient();
