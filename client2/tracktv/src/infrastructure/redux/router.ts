import {Injectable} from '@angular/core';
import {NavigationCancel, Router, RoutesRecognized} from '@angular/router';
import {reduxStore} from '../redux-store';
import {put} from 'redux-saga/effects';
import {ReduxReducer} from './meta';
import {ReduxStoreService} from './redux-store-service';

export const routerActions = {
  ROUTER_NAVIGATION: 'ROUTER/ROUTER_NAVIGATION',
  ROUTER_NAVIGATION_EXPLICIT: 'ROUTER/ROUTER_NAVIGATION_EXPLICIT',
  ROUTER_CANCEL: 'ROUTER/ROUTER_CANCEL',
  ROUTER_ERROR: 'ROUTER/ROUTER_ERROR',
};

export interface RouterState {
  location?: string;
}

export interface RouterAction {
  type: string;
  location: string;
}

@Injectable()
export class ReduxRouterService {
  constructor(private router: Router,
              private store: ReduxStoreService) {
  }

  init(routerStateSelector: (state: any) => RouterState) {

    if (!routerStateSelector) {
      throw new Error('The router state selector is falsy.');
    }

    let dispatchTriggeredByNavigation = false;
    let navigationTriggeredByDispatch = false;

    this.store.select(routerStateSelector).subscribe(state => {

      if (state.location && this.router.url !== state.location) {

        if (dispatchTriggeredByNavigation) {
          dispatchTriggeredByNavigation = false;
          return;
        }

        navigationTriggeredByDispatch = true;
        this.router.navigateByUrl(state.location);
      }
    });

    let location: any;

    this.router.events.subscribe(event => {

      if (event instanceof RoutesRecognized) {

        location = event.state.url;

        if (navigationTriggeredByDispatch) {
          navigationTriggeredByDispatch = false;
          return;
        }

        dispatchTriggeredByNavigation = true;

        this.store.dispatch({type: routerActions.ROUTER_NAVIGATION, location});
      } else if (event instanceof NavigationCancel) {

        this.store.dispatch({type: routerActions.ROUTER_CANCEL, location});
      }
    });
  }
}

export const routerReducer: ReduxReducer<RouterState> = (state = {}, action: RouterAction) => {
  switch (action.type) {
    case routerActions.ROUTER_NAVIGATION:
    case routerActions.ROUTER_ERROR: {
      return {
        ...state,
        location: action.location,
      };
    }
    default: {
      return state;
    }
  }
};

export const explicitRouterSaga = (router: Router) => ({
  type: routerActions.ROUTER_NAVIGATION_EXPLICIT,
  saga: function* (action: any) {

    const newAction = {
      type: routerActions.ROUTER_NAVIGATION,
      location: router.createUrlTree.apply(router, action.payload).toString(),
    };

    yield put(newAction);
  },
});

export const go = (...payload: any[]) => reduxStore.dispatch({type: routerActions.ROUTER_NAVIGATION_EXPLICIT, payload});


