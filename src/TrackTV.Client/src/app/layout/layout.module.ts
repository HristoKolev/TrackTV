import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {Gravatar} from '../ng2-gravatar-directive';
import {HeaderComponent} from './site-header/site-header.component';
import {FooterComponent} from './site-footer/site-footer.component';
import {IdentityModule} from '../identity/identity.module';

@NgModule({
    imports: [
        BrowserModule,
        RouterModule,
        FormsModule,
        IdentityModule
    ],
    declarations: [
        HeaderComponent,
        FooterComponent,

        Gravatar
    ],
    providers: [],
    exports: [
        HeaderComponent,
        FooterComponent
    ]
})
export class LayoutModule {
}