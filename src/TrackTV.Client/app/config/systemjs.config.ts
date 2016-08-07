System.config({
    map: {
        'rxjs': 'node_modules/rxjs',
        '@angular': 'node_modules/@angular'
    },
    packages: {
        'app': {
            main: 'main',
            defaultExtension: 'js'
        },
        'rxjs': {main: 'index.js'},
        '@angular/core': {main: 'index.js'},
        '@angular/common': {main: 'index.js'},
        '@angular/compiler': {main: 'index.js'},
        '@angular/router': {main: 'index.js'},
        '@angular/platform-browser': {main: 'index.js'},
        '@angular/platform-browser-dynamic': {main: 'index.js'},
        '@angular/forms': {main: 'index.js'},
        '@angular/http': {main: 'index.js'},
    },
    paths: {
        jquery: './node_modules/jquery/dist/jquery.js',
        toastr: './node_modules/toastr/toastr.js',
    },
});

System.import('app/main')
    .then(null, console.error.bind(console));