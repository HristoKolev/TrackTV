System.config({
    map: {
        '@angular': 'node_modules/@angular',
    },
    meta: {
        '@angular/*': {'format': 'cjs'}
    },
    paths: {
        'node_modules/@angular/*': 'node_modules/@angular/*/bundles',

        'jquery': './node_modules/jquery/dist/jquery.js',
        'toastr': './node_modules/toastr/toastr.js',
        'moment': './node_modules/moment/moment.js',
        'underscore.string': './node_modules/underscore.string/dist/underscore.string.js',
    },
    packages: {
        'app': {
            main: 'main',
            defaultExtension: 'js'
        },

        '@angular/core': {main: 'core.umd.min.js'},
        '@angular/common': {main: 'common.umd.min.js'},
        '@angular/compiler': {main: 'compiler.umd.min.js'},
        '@angular/platform-browser': {main: 'platform-browser.umd.min.js'},
        '@angular/platform-browser-dynamic': {main: 'platform-browser-dynamic.umd.min.js'},

        '@angular/router': {main: 'router.umd.min.js'},
        '@angular/http': {main: 'http.umd.min.js'},
        '@angular/forms': {main: 'forms.umd.min.js'}
    }
});

System.import('app/main')
    .then(null, console.error.bind(console));