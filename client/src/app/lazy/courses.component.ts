import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Course, CourseActions } from './courses-state';
import { reduxStore } from '../../infrastructure/redux-store';

@Component({
    encapsulation: ViewEncapsulation.Emulated,
    changeDetection: ChangeDetectionStrategy.Default,
    template: `
        <section>
            <button [routerLink]="0">Add</button>
            <button (click)="this.cats()">Cats</button>
            <br/>
            <br/>
            <label for="filterBox">Filter:</label>
            <input id="filterBox" [(ngModel)]="searchText">
            <div *ngFor="let course of courses" class="course">
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

    courses: Course[];

    constructor(private stateActions: CourseActions) {
    }

    ngOnInit(): void {

        reduxStore.select((state: any) => state.courses.filtered)
            .distinctUntilChanged()
            .subscribe((courses) => {
                this.courses = [...courses];
            });
    }

    set searchText(val: string) {
        this.stateActions.filterCourses(val);
    }

    cats() {
        reduxStore.dispatch({
            type: 'router/ROUTER_NAVIGATION_EXPLICIT',
            payload: [['/lazy', 2]],
        });
    }
}
