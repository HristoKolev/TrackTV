import {ApplicationRef, ErrorHandler, NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {Router, RouterModule, Routes} from '@angular/router';
import {AppComponent, NotFound404Component} from './app.component';
import {globalErrorReducer, IGlobalState, ISessionState, ISettingsState, settingsReducer, userSessionReducer} from './global.state';
import {HeaderComponent} from './layout/header.component';
import {LoadingComponent} from './layout/loading.component';
import {wrapDevToolsExtension} from '../infrastructure/redux/dev-tools';
import {ReduxHelperModule} from '../infrastructure/redux/redux-helper.module';
import {GlobalErrorHandler} from '../infrastructure/GlobalErrorHandler';
import {ReduxPersistService} from '../infrastructure/redux/redux-persist-service';
import {ReduxStoreService} from '../infrastructure/redux/redux-store-service';
import {SharedModule} from './shared/shared.module';
import {explicitRouterSaga, ReduxRouterService, routerReducer, RouterState} from '../infrastructure/redux/redux-router-service';

export const routes: Routes = [
  {path: '', redirectTo: '/shows', pathMatch: 'full'},
  {path: 'account', loadChildren: './account/account.module#AccountModule'},
  {path: 'shows', loadChildren: './shows/shows.module#ShowsModule'},
  {path: 'show', loadChildren: './show/show.module#ShowModule'},
  {path: 'my-shows', loadChildren: './my-shows/my-shows.module#MyShowsModule'},
  {path: 'calendar', loadChildren: './calendar/calendar.module#CalendarModule'},
  {path: '**', component: NotFound404Component},
];

@NgModule({
  declarations: [
    AppComponent,
    NotFound404Component,
    HeaderComponent,
    LoadingComponent,
  ],
  entryComponents: [],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes, {useHash: false}),
    ReduxHelperModule,
    SharedModule,
  ],
  bootstrap: [AppComponent],
  exports: [AppComponent],
  providers: [
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler,
    },
  ],
})
export class AppModule {

  constructor(reduxRouter: ReduxRouterService,
              reduxPersist: ReduxPersistService,
              appRef: ApplicationRef,
              router: Router,
              store: ReduxStoreService) {

    const devTools = (window as any).devToolsExtension;

    const enhancers: any[] = [];

    if (devTools) {
      enhancers.push(wrapDevToolsExtension(devTools, appRef, store)());
    }

    const initialReducers = {
      router: routerReducer,
    };

    store.initStore(enhancers, initialReducers);

    reduxRouter.init();

    reduxPersist.initialize({
      session: 'localStorage',
    });

    store.addReducers({
      settings: settingsReducer,
      global: globalErrorReducer,
      session: userSessionReducer,
    });

    store.addSagas({
      explicitRouterSaga: explicitRouterSaga(router),
      logSaga: {
        type: '*',
        saga: function* (action: any): any {
          console.log(action);
        },
      },
    });
  }
}

declare module '../infrastructure/redux/redux-state' {

  interface IReduxState {
    router: RouterState;
    settings: ISettingsState;
    session: ISessionState;
    global: IGlobalState;
  }
}
