import { Component } from '@angular/core';
import { smartComponent } from '../infrastructure/component-helpers';

@Component({
    ...smartComponent,
    selector: 'my-not-found',
    template: '<h3>Error 404: Not found</h3>',
})
export class NotFound404Component {
}
