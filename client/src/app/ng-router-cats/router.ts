import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRouteSnapshot, NavigationEnd, Router, RoutesRecognized } from '@angular/router';
import { NgRedux } from '@angular-redux/store';
import { Observable } from 'rxjs/Observable';
import { ISubscription } from 'rxjs/Subscription';
import { UPDATE_ROUTE } from './actions';
import { IRouterState } from './reducer';

import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';

@Injectable()
export class NgReduxRouter {

    private initialized = false;
    private currentLocation: string;
    private initialLocation: string;

    private urlStateSubscription: ISubscription;
    private reduxSubscription: ISubscription;

    constructor(private router: Router,
                private ngRedux: NgRedux<any>,
                private location: Location) {
    }

    private selectRouterState: (state: any) => IRouterState = (state) => state.router;

    /**
     * Destroys the bindings between @angular-redux/router and @angular/router.
     * This method unsubscribes from both @angular-redux/router and @angular router, in case
     * your app needs to tear down the bindings without destroying Angular or Redux
     * at the same time.
     */
    destroy(): void {

        if (this.urlStateSubscription) {
            this.urlStateSubscription.unsubscribe();
        }

        if (this.reduxSubscription) {
            this.reduxSubscription.unsubscribe();
        }

        this.initialized = false;
    }

    /**
     * Initialize the bindings between @angular-redux/router and @angular/router
     *
     * This should only be called once for the lifetime of your app, for
     * example in the constructor of your root component.
     *
     *
     * @param {(state: any) => string} selectRouterState Optional: If your
     * router state is in a custom location, supply this argument to tell the
     * bindings where to find the router state object in the state.
     * @param {Observable<string>} urlState$ Optional: If you have a custom setup
     * when listening to router changes, or use a different router than @angular/router
     * you can supply this argument as an Observable of the current url state.
     */
    initialize(selectRouterState: ((state: any) => IRouterState) | undefined = undefined,
               urlState$: Observable<IRouterState> | undefined = undefined): void {

        if (this.initialized) {
            throw new Error('@angular-redux/router already initialized! If you meant to re-initialize, call destroy first.');
        }

        if (selectRouterState) {
            this.selectRouterState = selectRouterState;
        }

        urlState$ = urlState$ || this.router.events
                .filter(x => x instanceof RoutesRecognized)
                .switchMap((x: RoutesRecognized) => this.router.events
                    .filter(y => y instanceof NavigationEnd)
                    .map(() => this.getRouteState(x.state.root)),
                ).distinctUntilChanged();

        // Subscribe for changes to the router.
        this.urlStateSubscription = urlState$.subscribe(this.routeChanged.bind(this));

        // Subscribe for changes in the redux store.
        this.reduxSubscription = this.ngRedux
            .select(this.selectRouterState)
            .distinctUntilChanged()
            .subscribe(this.reduxStateChanged.bind(this));

        this.initialized = true;
    }

    /**
     * Recursively walks the ActivatedRouteSnapshot tree,
     * gets params, queryParams and data from each snapshot and merges it together into one object.
     *
     * @param {ActivatedRouteSnapshot} route The current route snapshot.
     * @param {IRouterState} state The current state. Used only for recursion.
     * @returns {IRouterState}
     */
    private getRouteState(route: ActivatedRouteSnapshot, state: IRouterState = {} as IRouterState): IRouterState {

        state.params = Object.assign(state.params || {}, route.params);
        state.queryParams = Object.assign(state.queryParams || {}, route.queryParams);
        state.data = Object.assign(state.data || {}, route.data);

        if (route.firstChild) {
            return this.getRouteState(route.firstChild, state);
        }

        state.location = this.location.path();

        return state;
    }

    private routeChanged(newState: IRouterState): void {

        if (newState.location === this.currentLocation) {
            // Dont dispatch changes if we haven't changed location.
            return;
        }

        this.currentLocation = newState.location;

        if (this.initialLocation === undefined) {
            this.initialLocation = newState.location;

            // Fetch initial location from store and make sure
            // we dont dispath an event if the current url equals
            // the initial url.
            const oldState = this.selectRouterState(this.ngRedux.getState());

            if (oldState.location === this.currentLocation) {
                return;
            }
        }

        this.ngRedux.dispatch({
            type: UPDATE_ROUTE,
            router: newState,
        });
    }

    private reduxStateChanged(routerState: IRouterState): void {

        if (this.initialLocation === undefined) {
            // Wait for router to set initial location.
            return;
        }

        if (this.currentLocation === routerState.location || this.initialLocation) {
            // Dont change router location if its equal to the one in the store.
            return;
        }

        this.currentLocation = routerState.location;
        this.router.navigateByUrl(routerState.location);
    }
}
