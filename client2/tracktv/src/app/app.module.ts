import {ApplicationRef, ErrorHandler, NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {Router, RouterModule, Routes} from '@angular/router';
import {AppComponent, NotFound404Component} from './app.component';
import {globalErrorReducer, settingsReducer, userSessionReducer} from './global.state';
import {reduxStore} from '../infrastructure/redux-store';
import {HeaderComponent} from './layout/header.component';
import {LoadingComponent} from './layout/loading.component';
import {wrapDevToolsExtension} from '../infrastructure/redux/dev-tools';
import {explicitRouterSaga, ReduxRouterService, routerReducer} from '../infrastructure/redux/router';
import {ReduxPersistService} from '../infrastructure/redux/persist';
import {ReduxHelperModule} from '../infrastructure/redux/redux-helper.module';
import {GlobalErrorHandler} from '../infrastructure/GlobalErrorHandler';

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
              router: Router) {

    const devTools = (window as any).devToolsExtension;

    const enhancers: any[] = [];

    if (devTools) {
      enhancers.push(wrapDevToolsExtension(devTools, appRef, reduxStore)());
    }

    const initialReducers = {
      router: routerReducer,
    };

    reduxStore.initStore(enhancers, initialReducers);

    reduxRouter.init(state => state.router);

    reduxPersist.initialize({
      session: 'localStorage',
    });

    reduxStore.addReducers({
      settings: settingsReducer,
      global: globalErrorReducer,
      session: userSessionReducer,
    });

    reduxStore.addSagas({
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
