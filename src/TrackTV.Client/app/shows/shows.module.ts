import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {GenrePanelComponent} from './genre-panel/genre-panel.component';
import {PagedShowListComponent} from './paged-show-list/paged-show-list.component';
import {ShortShowComponent} from './short-show/short-show.component';
import {ShowListComponent} from './show-list/show-list.component';

import {CapitaliseWords} from './capitaliseWords.pipe';

import {NetworkShowsComponent} from './network-shows.component';
import {SearchShowsComponent} from './search-shows.component';
import {ShowsComponent} from './shows.component';

import {ShowsService} from './shows.service';
import {ShowsResolve} from './shows-resolve.service';
import {ShowsByGenreResolve} from './shows-by-genre-resolve.service';

import {SharedModule} from '../shared/shared.module';

import {showsRouting} from './shows.routes';

@NgModule({
    imports: [
        BrowserModule,
        SharedModule,
        showsRouting
    ],
    declarations: [
        GenrePanelComponent,
        PagedShowListComponent,
        ShortShowComponent,
        ShowListComponent,

        NetworkShowsComponent,
        SearchShowsComponent,
        ShowsComponent,

        CapitaliseWords,

    ],
    providers: [
        ShowsService,
        ShowsResolve,
        ShowsByGenreResolve
    ]
})
export class ShowsModule {
}