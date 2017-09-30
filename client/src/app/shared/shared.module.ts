import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorContainerComponent } from './error-container-component';

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

