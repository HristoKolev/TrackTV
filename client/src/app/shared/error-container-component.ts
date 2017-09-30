import { ChangeDetectionStrategy, Component, Input, ViewEncapsulation } from '@angular/core';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.OnPush,
    template: `
        <div *ngIf="this.errorMessages && this.errorMessages.length">
            <div *ngFor="let message of this.errorMessages">
                <div class="errorMessage">{{message}}</div>
            </div>
        </div>
    `,
    styles: [`
        .errorMessage {
            color: red;
        }
    `],
    selector: 'error-container-component',
})
export class ErrorContainerComponent {

    @Input()
    errorMessages: string[];
}
