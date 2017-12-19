import {NgModule} from '@angular/core';
import {ReduxRouterService} from './router';
import {ReduxStoreService} from './redux-store-service';
import {ReduxPersistService} from './redux-persist-service';

@NgModule({
  providers: [ReduxRouterService, ReduxPersistService, ReduxStoreService],
})
export class ReduxHelperModule {
}
