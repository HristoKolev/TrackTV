import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {GenrePanelComponent} from './genre-panel/genre-panel.component';
import {PagedShowListComponent} from './paged-show-list/paged-show-list.component';
import {ShortShowComponent} from './short-show/short-show.component';
import {ShowListComponent} from './show-list/show-list.component';
import {CapitaliseWords} from './capitaliseWords.pipe';
import {ShowsByNetworkComponent} from './shows-by-network.component';
import {ShowsByNameComponent} from './shows-by-name.component';
import {ShowsComponent} from './shows.component';
import {ShowsService} from './shows.service';
import {ShowsResolve} from './shows-resolve.service';
import {ShowsByNetworkResolve} from './shows-by-network-resolve.service';
import {ShowsByGenreResolve} from './shows-by-genre-resolve.service';
import {ShowsByNameResolve} from './shows-by-name-resolve.service';
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

        ShowsByNetworkComponent,
        ShowsByNameComponent,
        ShowsComponent,

        CapitaliseWords,

    ],
    providers: [
        ShowsService,
        ShowsResolve,
        ShowsByGenreResolve,
        ShowsByNetworkResolve,
        ShowsByNameResolve
    ]
})
export class ShowsModule {
}