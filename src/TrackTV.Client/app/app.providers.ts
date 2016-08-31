import {TypeBinder, PersistentContainer, LocalStorageContainer} from './shared/index';
import {ApiPath, Identity, Authentication, SubscriptionService} from './services/index';
import {ShowService} from  './show/show.service';
import {MyShowsService} from  './my-shows/my-shows.service';
import {AuthGuard} from  './services/AuthGuard.service';
import {ShowsService} from  './shows/shows.service';

const binder = new TypeBinder();

binder.bind(PersistentContainer, LocalStorageContainer);

binder.bindToSelf(ApiPath);
binder.bindToSelf(Identity);
binder.bindToSelf(Authentication);
binder.bindToSelf(ShowsService);
binder.bindToSelf(ShowService);
binder.bindToSelf(SubscriptionService);
binder.bindToSelf(MyShowsService);
binder.bindToSelf(AuthGuard);

import {PaginationService} from 'ng2-pagination';

binder.bindToSelf(PaginationService);

export const typeBindings = binder.bindings;