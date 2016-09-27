import {User} from './identity.models';
import {LocalStorageContainer} from '../shared/localStorageContainer';

export class UserContainer extends LocalStorageContainer<User> {
}