import {ApplicationRef, NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule, Routes} from '@angular/router';

import {AppComponent, NotFound404Component} from './app.component';

import {globalErrorReducer, settingsReducer, userSessionReducer} from './global.state';
import {
  explicitRouterSaga, ReduxHelperModule, ReduxPersistService, ReduxRouterService, reduxStore,
  wrapDevToolsExtension,
} from '../infrastructure/redux-store';
import {HeaderComponent} from './layout/header.component';
import {LoadingComponent} from './layout/loading.component';

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
  providers: [],
})
export class AppModule {

  constructor(reduxRouter: ReduxRouterService, reduxPersist: ReduxPersistService, appRef: ApplicationRef) {

    const devTools = (window as any).devToolsExtension;

    const enhancers: any[] = [];

    if (devTools) {
      enhancers.push(wrapDevToolsExtension(devTools, appRef)());
    }

    reduxStore.initStore(enhancers);

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
      explicitRouterSaga,
      logSaga: {
        type: '*',
        saga: function* (action: any): any {
          console.log(action);
        },
      },
    });
  }
}
