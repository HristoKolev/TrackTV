import { Component, OnInit } from '@angular/core';
import { Course, CourseActions, ICoursesState } from './state';
import { NgRedux } from '@angular-redux/store';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

@Component({
    template: `
        <label>Name: </label> <input [(ngModel)]="this.course.name"/> <br/>

        <label>Description: </label> <input [(ngModel)]="this.course.description"/>

        <br/>

        <button (click)="this.addCourse()" [disabled]="!this.course.name || !this.course.description">
            {{ this.course.id === 0 ? 'Add' : 'Update' }}
        </button>

        <br/>

        <button [routerLink]="['../', this.course.id + 1]">Increment</button>
        <button [routerLink]="['../', this.course.id - 1]">Decrement</button>
    `,
})
export class EditCourseComponent implements OnInit {

    course: Course;

    constructor(private stateActions: CourseActions,
                private ngRedux: NgRedux<{ courses: ICoursesState }>,
                private router: Router,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {

        this.route.paramMap.map((params: ParamMap) => +(params.get('id') as string))
            .switchMap(id => this.ngRedux.select(state => state.courses.all.filter(c => c.id === id))
                .map(courses => [...courses, {id, description: '', name: ''}]))
            .map(courses => courses[0])
            .subscribe(course => {
                this.course = course;
            });
    }

    addCourse() {

        this.stateActions.addCourse(this.course);
        this.router.navigate(['../'], {relativeTo: this.route});
    }
}
