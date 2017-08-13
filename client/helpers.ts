export interface IConstants {
    AOT: boolean;
    ENV: string;
    HOST: string;
    PORT: number;
    DEV_SERVER: boolean;
    DLL: boolean;
    E2E: boolean;
    WATCH: boolean;
    PROD: boolean;
}

import { existsSync } from 'fs';

import * as path from 'path';

const HOST = '127.0.0.1';
const DEV_PORT = 3000;
const PROD_PORT = 8088;

const EVENT = process.env.npm_lifecycle_event || '';
const PROD = EVENT.includes('prod');

export const CONSTANTS = {
    AOT: EVENT.includes('aot'),
    ENV: PROD ? JSON.stringify('production') : JSON.stringify('development'),
    HOST: JSON.stringify(HOST),
    PORT: PROD ? PROD_PORT : DEV_PORT,
    DEV_SERVER: EVENT.includes('webdev'),
    DLL: EVENT.includes('dll'),
    WATCH: process.argv.join('').indexOf('watch') > -1,
    PROD: PROD,
};

const isFunction = (functionToCheck: any) => functionToCheck && {}.toString.call(functionToCheck) === '[object Function]';

export const ifConst = (func: any, trueVal: any, falseVal: any = undefined) => {

    if (func(CONSTANTS)) {

        if (isFunction(trueVal)) {

            return trueVal();
        }

        return trueVal;
    } else {

        if (isFunction(falseVal)) {

            return falseVal();
        }

        return falseVal;
    }
};

export const root = (...args: any[]) => path.join(path.resolve(__dirname, './'), ...args);

export const testDll = () => {

    if (!existsSync(root('./dll/polyfill.dll.js')) || !existsSync(root('./dll/vendor.dll.js'))) {
        throw "DLL files do not exist, please use 'npm run build:dll' once to generate dll files.";
    }
};
