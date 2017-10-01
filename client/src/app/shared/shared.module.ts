import { ChangeDetectionStrategy, Component, Input, NgModule, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';

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

@NgModule({
    imports: [
        CommonModule,
    ],
    declarations: [ErrorContainerComponent],
    providers: [],
    exports: [ErrorContainerComponent],
})
export class SharedModule {
}
