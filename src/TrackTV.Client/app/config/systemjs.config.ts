System.config({
    packages: {
        app: {
            format: 'cjs',
            defaultExtension: 'js'
        }
    },
    paths: {
        jquery: './node_modules/jquery/dist/jquery.js',
        toastr: './node_modules/toastr/toastr.js',
    },
});

System.import('app/main')
    .then(null, console.error.bind(console));