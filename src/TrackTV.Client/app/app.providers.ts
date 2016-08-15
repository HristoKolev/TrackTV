import {TypeBinder, PersistentContainer, LocalStorageContainer} from './shared/index';
import {ApiPath, Identity, Authentication, ShowsService, ShowService, SubscriptionService} from './services/index';

const binder = new TypeBinder();

binder.bind(PersistentContainer, LocalStorageContainer);

binder.bindToSelf(ApiPath);
binder.bindToSelf(Identity);
binder.bindToSelf(Authentication);
binder.bindToSelf(ShowsService);
binder.bindToSelf(ShowService);
binder.bindToSelf(SubscriptionService);

import {PaginationService} from 'ng2-pagination';

binder.bindToSelf(PaginationService);

export const typeBindings = binder.bindings;