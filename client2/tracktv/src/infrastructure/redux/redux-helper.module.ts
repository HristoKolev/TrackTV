import {NgModule} from '@angular/core';
import {ReduxStoreService} from './redux-store-service';
import {ReduxPersistService} from './redux-persist-service';
import {ReduxRouterService} from './redux-router-service';

@NgModule({
  providers: [ReduxRouterService, ReduxPersistService, ReduxStoreService],
})
export class ReduxHelperModule {
}
