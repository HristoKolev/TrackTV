import {HeaderComponent} from  './layout/header/header.component';
import {FooterComponent} from  './layout/footer/footer.component';

import {ShowsComponent} from  './shows/shows.component';
import {NetworkShowsComponent} from  './shows/network-shows.component';
import {SearchShowsComponent} from  './shows/search-shows.component';
import {ShortShowComponent} from  './shows/short-show/short-show.component';
import {GenrePanelComponent} from  './shows/genre-panel/genre-panel.component';
import {PagedShowListComponent} from  './shows/paged-show-list/paged-show-list.component';
import {ShowListComponent} from  './shows/show-list/show-list.component';
import {CapitaliseWords} from  './shows/capitaliseWords.pipe';

import {LongDate} from  './my-shows/my-episode/long-date.pipe';
import {MyShowsComponent} from  './my-shows/my-shows.component';
import {PagedMyShowListComponent} from  './my-shows/paged-my-show-list/paged-my-show-list.component';
import {MyEpisodeComponent} from "./my-shows/my-episode/my-episode.component";

import {PaginatePipe, PaginationControlsCmp} from 'ng2-pagination';

const appComponents : any[] = [

    HeaderComponent, FooterComponent,

    ShowsComponent, NetworkShowsComponent, SearchShowsComponent,
    ShortShowComponent,
    GenrePanelComponent, PagedShowListComponent,
    ShowListComponent,
    MyShowsComponent,
    PagedMyShowListComponent,
    MyEpisodeComponent,

];

const appPipes : any[] = [

    CapitaliseWords,
    LongDate
];

const libComponents : any[] = [

    PaginationControlsCmp
];

const libPipes : any[] = [

    PaginatePipe
];

export const appDeclarations : any[] = [...appComponents, ...appPipes, ...libComponents, ...libPipes];