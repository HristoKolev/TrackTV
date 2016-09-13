System.config({
    map: {
        '@angular': 'node_modules/@angular',
        'ng2-pagination': 'node_modules/ng2-pagination',
        'ng2-gravatar-directive': 'node_modules/ng2-gravatar-directive/src',
        'md5': 'node_modules/md5',
        'crypt': 'node_modules/crypt',
        'charenc': 'node_modules/charenc',
        'is-buffer': 'node_modules/is-buffer',

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
        'app': {main: 'main'},

        '@angular/core': {main: 'core.umd.min.js'},
        '@angular/common': {main: 'common.umd.min.js'},
        '@angular/compiler': {main: 'compiler.umd.min.js'},
        '@angular/platform-browser': {main: 'platform-browser.umd.min.js'},
        '@angular/platform-browser-dynamic': {main: 'platform-browser-dynamic.umd.min.js'},

        '@angular/router': {main: 'router.umd.min.js'},
        '@angular/http': {main: 'http.umd.min.js'},
        '@angular/forms': {main: 'forms.umd.min.js'},

        'ng2-pagination': {main: 'index.js'},

        'ng2-gravatar-directive': {main: 'gravatar'},
        'md5': {main: 'md5'},
        'crypt': {main: 'crypt'},
        'charenc': {main: 'charenc'},
        'is-buffer': {main: 'index'},
    }
});