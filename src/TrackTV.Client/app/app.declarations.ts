import {HeaderComponent, FooterComponent} from  './layout/index';
import {LoginComponent, RegisterComponent} from  './account/index';
import {ShowComponent} from  './show/show.component';

import {
    ShowsComponent,
    NetworkShowsComponent,
    SearchShowsComponent,
    ShortShowComponent,
    GenrePanelComponent,
    PagedShowListComponent,
    ShowListComponent
} from  './shows/index';

import {DoubleDigit} from  './show/pipes/doubleDigit.pipe';
import {PremieredDate} from  './show/pipes/premieredDate.pipe';
import {Querify} from  './show/pipes/querify.pipe';
import {WeekDayName} from  './show/pipes/weekDayName.pipe';
import {CapitaliseWords} from  './shows/capitaliseWords.pipe';

const appComponents : any[] = [

    HeaderComponent, FooterComponent,
    LoginComponent, RegisterComponent,
    ShowComponent,
    ShowsComponent, NetworkShowsComponent, SearchShowsComponent,
    ShortShowComponent,
    GenrePanelComponent, PagedShowListComponent,
    ShowListComponent
];

const appPipes : any[] = [

    DoubleDigit, PremieredDate, Querify, WeekDayName,
    CapitaliseWords
];

import {PaginatePipe, PaginationControlsCmp} from 'ng2-pagination';

const libComponents : any[] = [

    PaginationControlsCmp
];

const libPipes : any[] = [

    PaginatePipe
];

export const appDeclarations : any[] = [...appComponents, ...appPipes, ...libComponents, ...libPipes];