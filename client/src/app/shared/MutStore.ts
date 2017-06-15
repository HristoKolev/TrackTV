export interface User {

    access_token: string;

    username: string;
}

export interface GlobalModel {
    user: User;
}

export class MutStore {

    public payload: GlobalModel;

    private key: string;

    constructor() {

        this.key = 'MutStore';
    }

    public save(): void {

        localStorage.setItem(this.key, JSON.stringify(this.payload));
    }

    public load(): void {

        const json = localStorage.getItem(this.key);

        this.payload = JSON.parse(json || '{}') as GlobalModel;
    }
}

export const store = new MutStore();
