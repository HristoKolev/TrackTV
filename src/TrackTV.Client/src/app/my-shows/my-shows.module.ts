import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {MyShowsService} from './my-shows.service';
import {LongDate} from './my-episode/long-date.pipe';
import {MyEpisodeComponent} from './my-episode/my-episode.component';
import {PagedMyShowListComponent} from './paged-my-show-list/paged-my-show-list.component';
import {MyShowsComponent} from './my-shows.component';
import {MyShowsResolve} from './my-shows-resolve.service';
import {SharedModule} from '../shared/shared.module';
import {myShowsRouting} from './my-shows.routes';
import {IdentityModule} from '../identity/identity.module';
import {Ng2PaginationModule} from 'ng2-pagination';

@NgModule({
    imports: [
        BrowserModule,
        SharedModule,
        myShowsRouting,
        IdentityModule,
        Ng2PaginationModule
    ],
    declarations: [

        LongDate,
        MyEpisodeComponent,
        PagedMyShowListComponent,
        MyShowsComponent,
    ],
    providers: [
        MyShowsService,
        MyShowsResolve
    ]
})
export class MyShowsModule {
}