import {NgModule} from '@angular/core';
import {ReduxPersistService} from './persist';
import {ReduxRouterService} from './router';
import {ReduxStoreService} from './redux-store-service';

@NgModule({
  providers: [ReduxRouterService, ReduxPersistService, ReduxStoreService],
})
export class ReduxHelperModule {
}
