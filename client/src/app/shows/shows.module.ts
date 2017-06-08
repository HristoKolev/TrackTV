import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {GenrePanelComponent} from './genre-panel/genre-panel.component';
import {PagedShowListComponent} from './paged-show-list/paged-show-list.component';
import {ShortShowComponent} from './short-show/short-show.component';
import {ShowListComponent} from './show-list/show-list.component';
import {CapitaliseWords} from './capitaliseWords.pipe';
import {ShowsByNetworkComponent} from './shows-by-network.component';
import {ShowsByNameComponent} from './shows-by-name.component';
import {ShowsByGenreComponent} from './shows-by-genre.component';
import {ShowsComponent} from './shows.component';
import {ShowsService} from './shows.service';
import {ShowsResolve} from './shows-resolve.service';
import {ShowsByNetworkResolve} from './shows-by-network-resolve.service';
import {ShowsByGenreResolve} from './shows-by-genre-resolve.service';
import {ShowsByNameResolve} from './shows-by-name-resolve.service';
import {SharedModule} from '../shared/shared.module';
import {showsRouting} from './shows.routes';
import {ShowsByNameGuard} from './shows-by-name-guard.service';
import {Ng2PaginationModule} from 'ng2-pagination';

@NgModule({
    imports: [
        BrowserModule,
        SharedModule,
        showsRouting,
        Ng2PaginationModule
    ],
    declarations: [
        GenrePanelComponent,
        PagedShowListComponent,
        ShortShowComponent,
        ShowListComponent,

        ShowsComponent,
        ShowsByGenreComponent,
        ShowsByNetworkComponent,
        ShowsByNameComponent,

        CapitaliseWords,

    ],
    providers: [
        ShowsService,

        ShowsResolve,
        ShowsByGenreResolve,
        ShowsByNetworkResolve,
        ShowsByNameResolve,
        ShowsByNameGuard
    ]
})
export class ShowsModule {
}