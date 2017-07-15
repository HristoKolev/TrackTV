import { Action } from 'redux';

import { UPDATE_ROUTE } from './actions';

export interface IRouterState {
    params: any;
    queryParams: any;
    data: any;
    location: string;
}

export const defaultRouterState: IRouterState = {
    params: {},
    queryParams: {},
    data: {},
    location: '',
};

export interface RouterAction extends Action {
    router: IRouterState;
}

export function routerReducer(state: IRouterState = defaultRouterState, action: RouterAction): IRouterState {

    switch (action.type) {

        case UPDATE_ROUTE: {
            return action.router || defaultRouterState;
        }
        default: {
            return state;
        }
    }
}
