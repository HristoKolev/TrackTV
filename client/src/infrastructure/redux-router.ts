import { Injectable, NgModule } from '@angular/core';
import { NavigationCancel, NavigationError, Router, RoutesRecognized } from '@angular/router';
import { NgRedux } from '@angular-redux/store';
import { actionTypes } from './redux-types';
import { RouterState } from './redux-store';
import { put } from 'redux-saga/effects';

export const routerActions = actionTypes('router').ofType<{
    ROUTER_NAVIGATION: string;
    ROUTER_NAVIGATION_EXPLICIT: string;
    ROUTER_CANCEL: string;
    ROUTER_ERROR: string;
}>();

let angularRouter: Router;

export const explicitRouterSaga = {
    type: routerActions.ROUTER_NAVIGATION_EXPLICIT,
    saga: function* (action: any) {

        const newAction = {
            type: routerActions.ROUTER_NAVIGATION,
            location: angularRouter.createUrlTree.apply(angularRouter, action.payload).toString(),
        };

        yield put(newAction);
    },
};

@Injectable()
export class ReduxRouter {

    public init(routerStateSelector: (state: any) => RouterState) {

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
            } else if (event instanceof NavigationError) {

                this.store.dispatch({type: routerActions.ROUTER_ERROR, location});
            }
        });
    }

    constructor(private router: Router,
                private store: NgRedux<any>) {

        angularRouter = router;
    }
}

@NgModule({
    providers: [ReduxRouter],
})
export class ReduxRouterModule {
}
