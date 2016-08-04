System.config({
    packages: {
        app: {
            format: 'cjs',
            defaultExtension: 'js'
        }
    }
});

System.import('app/main')
    .then(null, console.error.bind(console));