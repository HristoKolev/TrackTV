export interface IStorage {
    [key: string]: string;
}

const storageMock = () => {
    let storage = {} as IStorage;
    return {
        getItem: (key: string) => key in storage ? storage[key] : null,
        setItem: (key: string, value: string) => storage[key] = value || '',
        removeItem: (key: string) => delete storage[key],
        clear: () => storage = {},
    };
};

Object.defineProperty(window, 'localStorage', {value: storageMock()});
Object.defineProperty(window, 'sessionStorage', {value: storageMock()});
Object.defineProperty(window, 'getComputedStyle', {value: () => ['-webkit-appearance']});
