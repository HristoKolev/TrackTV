import {ParamMap} from '@angular/router';

export const parseParams = (paramMap: ParamMap) => paramMap.keys
  .reduce((result: any, key: string) => ({...result, [key]: paramMap.get(key)}), {});

export const removeFalsyProperties = (obj: any): any => {
  const result = {} as any;

  for (const [key, value] of Object.entries(obj)) {
    if (value) {

      result[key] = value;
    }
  }

  return result;
};
