require('ts-node/register');

const path = require('path');

const {CONSTANTS, root} = require('./helpers');

exports.config = {
    baseUrl: `http://localhost:${CONSTANTS.E2E_PORT}/`,

    // use `npm run e2e`
    specs: [
        root('e2e/**/**.e2e.ts'),
        root('e2e/**/*.e2e.ts'),
        root('src/**/**.e2e.ts'),
        root('src/**/*root.e2e.ts')
    ],
    exclude: [],

    framework: 'jasmine2',

    allScriptsTimeout: 110000,

    jasmineNodeOpts: {
        showTiming: true,
        showColors: true,
        isVerbose: false,
        includeStackTrace: false,
        defaultTimeoutInterval: 400000
    },
    directConnect: true,

    capabilities: {
        'browserName': 'chrome',
        'chromeOptions': {
            'args': ['show-fps-counter=true']
        }
    },

    onPrepare: function () {
        browser.ignoreSynchronization = true;
    },

    /**
     * Angular 2 configuration
     *
     * useAllAngular2AppRoots: tells Protractor to wait for any angular2 apps on the page instead of just the one matching
     * `rootEl`
     */
    useAllAngular2AppRoots: true
};