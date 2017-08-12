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

    location: string;
}

export const routerReducer = (state: RouterState = {} as RouterState, action: any): RouterState => {
    switch (action.type) {
        case routerActions.ROUTER_NAVIGATION:
        case routerActions.ROUTER_ERROR:
        case routerActions.ROUTER_CANCEL: {
            return {
                location: action.location,
            };
        }
        default:
            return state;
    }
};

let angularRouter: Router;

export const explicitRouterEpic = (actions$: any) => actions$
    .ofType(routerActions.ROUTER_NAVIGATION_EXPLICIT)
    .map((action: any) => ({
        type: routerActions.ROUTER_NAVIGATION,
        location: angularRouter.createUrlTree.apply(angularRouter, action.payload).toString(),
    }));

@Injectable()
export class CatsReduxRouter {

    private routerSubscription: Subscription;

    private storeSubscription: Subscription;

    public destroy(): void {

        if (this.routerSubscription) {
            this.routerSubscription.unsubscribe();
        }

        if (this.storeSubscription) {
            this.storeSubscription.unsubscribe();
        }
    }

    constructor(private router: Router,
                private store: NgRedux<any>) {

        angularRouter = router;

        let dispatchTriggeredByNavigation = false;
        let navigationTriggeredByDispatch = false;

        this.storeSubscription = store.select((s: any) => s).subscribe((state: any) => {

            //debugger;

            const location = state.router.location;

            if (location && this.router.url !== location) {

                if (dispatchTriggeredByNavigation) {
                    dispatchTriggeredByNavigation = false;
                    return;
                }

                navigationTriggeredByDispatch = true;
                this.router.navigateByUrl(location);
            }
        });

        let location: any;

        this.routerSubscription = this.router.events.subscribe(event => {

            if (event instanceof RoutesRecognized) {

                location = event.state.url;

                if (navigationTriggeredByDispatch) {
                    navigationTriggeredByDispatch = false;
                    return;
                }

                //debugger;

                dispatchTriggeredByNavigation = true;

                this.dispatchRouterAction(routerActions.ROUTER_NAVIGATION, location);

            } else if (event instanceof NavigationCancel) {

                this.dispatchRouterAction(routerActions.ROUTER_CANCEL, location);
            } else if (event instanceof NavigationError) {

                this.dispatchRouterAction(routerActions.ROUTER_ERROR, location);
            }
        });

    }

    private dispatchRouterAction(type: string, location: any): void {

        this.store.dispatch({type, location});
    }
}

@NgModule({
    providers: [CatsReduxRouter],
})
export class CatsReduxRouterModule {
}


