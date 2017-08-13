// Extra variables that live on Global that will be replaced by webpack DefinePlugin

declare const ENV: string;
declare const System: SystemJS;

interface SystemJS {
    //noinspection ReservedWordAsName
    import: (path?: string) => Promise<any>;
}

