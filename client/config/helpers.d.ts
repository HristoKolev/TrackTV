export declare const HOST: string;
export declare const DEV_PORT: number;
export declare const E2E_PORT: number;
export declare const PROD_PORT: number;
export declare const CONSTANTS: IConstants;
export declare const ifConst: any;
export declare const testDll: any;
export declare const root: any;

export interface IConstants {
    AOT: boolean;
    ENV: string;
    HOST: string;
    PORT: number;
    DEV_SERVER: boolean;
    DLL:   boolean;
    E2E:   boolean;
    WATCH: boolean;
    PROD: boolean,
}
