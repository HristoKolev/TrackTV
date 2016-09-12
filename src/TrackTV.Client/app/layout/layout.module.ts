import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {HeaderComponent} from './header/header.component';
import {FooterComponent} from './footer/footer.component';

@NgModule({
    imports: [
        BrowserModule,
        RouterModule,
        FormsModule
    ],
    declarations: [
        HeaderComponent,
        FooterComponent
    ],
    providers: [],
    exports: [
        HeaderComponent,
        FooterComponent
    ]
})
export class LayoutModule {
}