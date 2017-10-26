import { Injectable } from '@angular/core';
import { actionTypes, ReduxReducer, reduxStore } from '../../infrastructure/redux-store';

export interface Course {
    name: string;
    description: string;
    id: number;
}

export interface ICoursesState {
    all: Course[];
    filtered: Course[];
    filter: string;
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
    filter: '',
};

const coursesActions = actionTypes('courses').ofType<{
    FILTER_COURSES: string;
    ADD_COURSE: string;
}>();

export const coursesReducer: ReduxReducer<ICoursesState> = (state = initialState, action: any) => {
    switch (action.type) {
        case coursesActions.FILTER_COURSES:
            return {
                ...state,
                filtered: state.all.filter(c => c.name.toLowerCase()
                    .indexOf(action.filter.toLowerCase()) > -1),
                filter: action.filter,
            };
        case coursesActions.ADD_COURSE: {
            return {
                ...state,
                all: [...state.all.filter(c => c.id !== action.course.id), action.course],
                filtered: state.all.filter(c => c.name.toLowerCase()
                    .indexOf(state.filter.toLowerCase()) > -1),
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

    filterCourses(filter: string) {

        reduxStore.dispatch({
            type: coursesActions.FILTER_COURSES,
            filter: filter,
        });
    }

    addCourse(course: Course) {

        course.id = course.id || nextId++;

        reduxStore.dispatch({
            type: coursesActions.ADD_COURSE,
            course,
        });
    }
}
