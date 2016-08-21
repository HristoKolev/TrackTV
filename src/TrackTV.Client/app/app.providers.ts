import {TypeBinder, PersistentContainer, LocalStorageContainer} from './shared/index';
import {ApiPath, Identity, Authentication, ShowsService, SubscriptionService} from './services/index';
import {ShowService} from  './show/show.service';
import {MyShowsService} from  './my-shows/my-shows.service';

const binder = new TypeBinder();

binder.bind(PersistentContainer, LocalStorageContainer);

binder.bindToSelf(ApiPath);
binder.bindToSelf(Identity);
binder.bindToSelf(Authentication);
binder.bindToSelf(ShowsService);
binder.bindToSelf(ShowService);
binder.bindToSelf(SubscriptionService);
binder.bindToSelf(MyShowsService);

import {PaginationService} from 'ng2-pagination';

binder.bindToSelf(PaginationService);

export const typeBindings = binder.bindings;