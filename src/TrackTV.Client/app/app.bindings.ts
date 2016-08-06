import {TypeBinder, PersistentContainer, LocalStorageContainer} from './shared/index';
import {ApiPath, Identity, Authentication} from './services/index';

export function applyBindings(binder : TypeBinder) {

    binder.bind(PersistentContainer, LocalStorageContainer);

    binder.bindToSelf(ApiPath);
    binder.bindToSelf(Identity);
    binder.bindToSelf(Authentication);
}