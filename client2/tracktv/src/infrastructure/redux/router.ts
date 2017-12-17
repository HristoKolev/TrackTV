import {Injectable} from '@angular/core';
import {NavigationCancel, NavigationError, Router, RoutesRecognized} from '@angular/router';
import {reduxStore} from '../redux-store';
import {put} from 'redux-saga/effects';
import {actionTypes, ReduxReducer} from './meta';

export const routerActions = actionTypes('router').ofType<{
  ROUTER_NAVIGATION: string;
  ROUTER_NAVIGATION_EXPLICIT: string;
  ROUTER_CANCEL: string;
  ROUTER_ERROR: string;
}>();

export interface RouterState {
  location?: string;
}

export interface RouterAction {
  type: string;
  location: string;
}

@Injectable()
export class ReduxRouterService {
  constructor(private router: Router) {
  }

  init(routerStateSelector: (state: any) => RouterState) {

    if (!routerStateSelector) {
      throw new Error('The router state selector is falsy.');
    }

    let dispatchTriggeredByNavigation = false;
    let navigationTriggeredByDispatch = false;

    reduxStore.select(routerStateSelector).subscribe(state => {

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

        reduxStore.dispatch({type: routerActions.ROUTER_NAVIGATION, location});
      } else if (event instanceof NavigationCancel) {

        reduxStore.dispatch({type: routerActions.ROUTER_CANCEL, location});
      } else if (event instanceof NavigationError) {

        console.error(event.error);
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


