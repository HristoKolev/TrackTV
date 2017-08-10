import { Injectable, NgModule } from '@angular/core';
import { NavigationCancel, NavigationError, Router, RoutesRecognized } from '@angular/router';
import { NgRedux } from '@angular-redux/store';
import { Subscription } from 'rxjs/Subscription';
import { actionTypes } from './redux-helpers';

export const routerActions = actionTypes('router').ofType<{
    ROUTER_NAVIGATION: string;
    ROUTER_NAVIGATION_EXPLICIT: string;
    ROUTER_CANCEL: string;
    ROUTER_ERROR: string;
}>();

export interface RouterState {
    navigationId: number;
    active: boolean;
    location: string;
}

export const routerReducer = (state: RouterState = {} as RouterState, action: any): RouterState => {
    switch (action.type) {
        case routerActions.ROUTER_NAVIGATION:
        case routerActions.ROUTER_ERROR:
        case routerActions.ROUTER_CANCEL: {
            return {
                location: action.payload.location,
                navigationId: action.payload.event.id,
                active: true,
            };
        }
        default:
            return state;
    }
};

@Injectable()
export class CatsReduxRouter {

    private dispatchTriggeredByRouter: boolean = false; // used only in dev mode in combination with routerReducer

    private navigationTriggeredByDispatch: boolean = false; // used only in dev mode in combination with routerReducer

    private storeSubscription: Subscription;

    private routerSubscription: Subscription;

    public destroy(): void {

        if (this.storeSubscription) {
            this.storeSubscription.unsubscribe();
        }

        if (this.routerSubscription) {
            this.routerSubscription.unsubscribe();
        }
    }

    constructor(private router: Router,
                private store: NgRedux<any>) {

        this.storeSubscription = this.store.select(f => f).subscribe(state => {

            const routerStoreState = this.routerSelector(state);

            if (!routerStoreState.active || this.dispatchTriggeredByRouter) {
                return;
            }

            if (this.router.url !== routerStoreState.location) {
                this.navigationTriggeredByDispatch = true;
                this.router.navigateByUrl(routerStoreState.location);
            }
        });

        this.routerSubscription = this.router.events.subscribe(event => {

            let location: any;

            if (event instanceof RoutesRecognized) {

                location = event.state.url;

                if (!this.navigationTriggeredByDispatch) {
                    this.dispatchRouterAction(routerActions.ROUTER_NAVIGATION, {location, event});
                }

            } else if (event instanceof NavigationCancel) {

                this.dispatchRouterAction(routerActions.ROUTER_CANCEL, {location, event});
            } else if (event instanceof NavigationError) {

                this.dispatchRouterAction(routerActions.ROUTER_ERROR, {location, event});
            }
        });
    }

    private dispatchRouterAction(type: string, payload: any): void {

        this.dispatchTriggeredByRouter = true;

        try {

            this.store.dispatch({type, payload});

        } finally {

            this.dispatchTriggeredByRouter = false;
            this.navigationTriggeredByDispatch = false;
        }
    }

    private routerSelector = (store: any) => store.router;

}

@NgModule({
    providers: [CatsReduxRouter],
})
export class CatsReduxRouterModule {
}
