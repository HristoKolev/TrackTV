import {TypeBinder, PersistentContainer, LocalStorageContainer} from './shared/index';
import {ApiPath, Identity, Authentication, ShowsService} from './services/index';

const binder = new TypeBinder();

binder.bind(PersistentContainer, LocalStorageContainer);

binder.bindToSelf(ApiPath);
binder.bindToSelf(Identity);
binder.bindToSelf(Authentication);
binder.bindToSelf(ShowsService);

export const typeBindings = binder.bindings;