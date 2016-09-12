import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {ShowComponent} from './show.component';
import {PremieredDate} from './pipes/premieredDate.pipe';
import {Querify} from './pipes/querify.pipe';
import {WeekDayName} from './pipes/weekDayName.pipe';
import {ShowService} from './show.service';
import {ShowResolve} from './show-resolve.service';
import {showRouting} from './show.routes';
import {SharedModule} from '../shared/shared.module';

@NgModule({
    imports: [
        BrowserModule,
        SharedModule,
        showRouting
    ],
    declarations: [
        ShowComponent,
        PremieredDate,
        Querify,
        WeekDayName
    ],
    providers: [
        ShowService,
        ShowResolve
    ]
})
export class ShowModule {
}