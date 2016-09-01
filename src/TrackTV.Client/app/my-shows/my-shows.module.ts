import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {MyShowsService} from './my-shows.service';
import {LongDate} from './my-episode/long-date.pipe';
import {MyEpisodeComponent} from './my-episode/my-episode.component';
import {PagedMyShowListComponent} from './paged-my-show-list/paged-my-show-list.component';
import {MyShowsComponent} from './my-shows.component';

import {SharedModule} from '../shared/shared.module';

import {myShowsRouting} from './my-shows.routes';

@NgModule({
    imports: [
        BrowserModule,
        SharedModule,
        myShowsRouting
    ],
    declarations: [

        LongDate,
        MyEpisodeComponent,
        PagedMyShowListComponent,
        MyShowsComponent,

    ],
    providers: [
        MyShowsService
    ]
})
export class MyShowsModule {
}