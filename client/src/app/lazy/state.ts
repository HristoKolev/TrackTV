import { Injectable } from '@angular/core';
import { NgRedux } from '@angular-redux/store';
import { addReducers } from '../store';

export interface Course {
    name: string;
    description: string;
    id: number;
}

export interface ICoursesState {
    all: Course[];
    filtered: Course[];
}

const courses: Course[] = [
    {
        name: 'Mastering Git',
        description: 'Master the best source control system available.',
        id: 1,
    },
    {
        name: 'Learn Git',
        description: 'Lets start learning Git',
        id: 2,
    },
    {
        name: 'Angular 100 for cats',
        description: 'My cats are great.',
        id: 3,
    },
];

const initialState: ICoursesState = {
    all: courses,
    filtered: courses,
};

const actionTypes = {
    FILTER_COURSES: 'courses/FILTER_COURSES',
    ADD_COURSE: 'courses/ADD_COURSE',
};

const coursesReducer = (state: ICoursesState = initialState, action: any): ICoursesState => {
    switch (action.type) {
        case actionTypes.FILTER_COURSES:
            return {
                ...state,
                filtered: state.all.filter(c => c.name.toLowerCase()
                    .indexOf(action.searchText.toLowerCase()) > -1),
            };
        case actionTypes.ADD_COURSE: {
            return {
                ...state,
                all: [...state.all.filter(c => c.id !== action.course.id), action.course],
            };
        }
        default: {
            return state;
        }
    }
};

let nextId = courses.length + 1;

@Injectable()
export class CourseActions {

    constructor(private ngRedux: NgRedux<any>) {
    }

    filterCourses(searchText: string) {

        this.ngRedux.dispatch({
            type: actionTypes.FILTER_COURSES,
            searchText,
        });
    }

    addCourse(course: Course) {

        course.id = course.id || nextId++;

        this.ngRedux.dispatch({
            type: actionTypes.ADD_COURSE,
            course,
        });
    }
}

addReducers({
    courses: coursesReducer,
});
