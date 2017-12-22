interface MishapOptions {
  apyKey: string;
  urlOverride?: string;
}

type RecordType = 'Error';

interface Record {
  recordExtendedDescription?: string;
  recordContext?: string;
  recordDescription: string;
  recordTitle: string;
  recordType?: RecordType;
}

interface MishapApi {
  log(error: Record): void;
}

export const createMishapClient = (options: MishapOptions): MishapApi => {

  const sendRequest = (record: Record) => {

    return fetch(options.urlOverride || 'https://mishap.hristo.tech/records', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'ApiKey': options.apyKey,
      },
      body: JSON.stringify(record),
    })
      .then(res => res.json())
      .catch(reason => {
        console.error(reason);
      });
  };

  return {
    log: sendRequest,
  };
};
