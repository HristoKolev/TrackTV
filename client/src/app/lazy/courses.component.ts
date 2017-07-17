import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Course, CourseActions, ICoursesState } from './state';
import { NgRedux } from '@angular-redux/store';

@Component({
    template: `
        <section>

            <button [routerLink]="0">Add</button>

            <br/>

            <br/>

            <label for="filterBox">Filter:</label>

            <input id="filterBox" [(ngModel)]="searchText">

            <div *ngFor="let course of filteredCourses$ | async" class="course">
                {{course.name}}
                <button [routerLink]="[ course.id]">Edit</button>
            </div>
        </section>
    `,
    styles: [`
        .course {
            color: #f25a30;
            margin: 20px;
            font-weight: 900;
        }
    `],
})
export class CoursesComponent implements OnInit {

    filteredCourses$: Observable<Course[]>;

    constructor(private stateActions: CourseActions,
                private ngRedux: NgRedux<{ courses: ICoursesState }>) {

    }

    ngOnInit(): void {

        this.searchText = '';

        this.filteredCourses$ = this.ngRedux.select(x => x.courses.filtered);
    }

    set searchText(val: string) {
        this.stateActions.filterCourses(val);
    }
}
