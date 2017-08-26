import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { FormsModule } from '@angular/forms';
import { TopShowsComponent } from './shows-component';

const routes: Routes = [
    {path: 'top', component: TopShowsComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
    ],
    declarations: [TopShowsComponent],
    providers: [],
})
export class LazyModule {
    constructor() {

    }
}

