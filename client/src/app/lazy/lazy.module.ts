import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { CourseActions, coursesReducer } from './courses-state';
import { FormsModule } from '@angular/forms';
import { CoursesComponent } from './courses.component';
import { EditCourseComponent } from './edit-course.component';
import { reduxStore } from '../../infrastructure/redux-store';

const routes: Routes = [
    {path: '', component: CoursesComponent},
    {path: ':id', component: EditCourseComponent},
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        FormsModule,
    ],
    declarations: [
        CoursesComponent,
        EditCourseComponent,
    ],
    providers: [
        CourseActions,
    ],
})
export class LazyModule {
    constructor() {

        reduxStore.addReducers({
            courses: coursesReducer,
        });
    }
}

