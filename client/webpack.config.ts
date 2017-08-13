const {DefinePlugin, DllPlugin, DllReferencePlugin, ProgressPlugin, NoEmitOnErrorsPlugin} = require('webpack'),
    copyWebpackPlugin = require('copy-webpack-plugin'),
    {CheckerPlugin} = require('awesome-typescript-loader'),
    htmlWebpackPlugin = require('html-webpack-plugin'),
    namedModulesPlugin = require('webpack/lib/NamedModulesPlugin'),
    uglifyJsPlugin = require('webpack/lib/optimize/UglifyJsPlugin'),
    {BundleAnalyzerPlugin} = require('webpack-bundle-analyzer'),
    webpackMd5Hash = require('webpack-md5-hash'),
    {AotPlugin} = require('@ngtools/webpack');

import 'ts-helpers';

import { CONSTANTS, IConstants, ifConst, root, testDll } from './helpers';

if (CONSTANTS.DEV_SERVER) {
    testDll();
    console.log(`Starting dev server on: http://${CONSTANTS.HOST}:${CONSTANTS.PORT}`);
}

console.log('PRODUCTION BUILD: ', CONSTANTS.PROD);
console.log('AOT: ', CONSTANTS.AOT);

if (CONSTANTS.DLL) {
    console.log('BUILDING DLLs');
} else {
    console.log('BUILDING APP');
}

interface WebpackConfig {
    cache?: boolean;
    target?: string;
    devtool?: string;
    entry: any;
    externals?: any;
    output: any;
    module?: any;
    plugins?: Array<any>;
    resolve?: {
        extensions?: Array<string>;
    };
    devServer?: {
        contentBase?: string;
        port?: number;
        historyApiFallback?: { [key: string]: boolean } | boolean;
        hot?: boolean;
        inline?: boolean;
        proxy?: any;
        host?: string;
        stats?: string;
        quiet?: boolean;
        noInfo?: boolean;
        watchOptions?: any;
    };
    performance?: {
        hints?: boolean;
    };
    node?: {
        process?: boolean;
        global?: boolean;
        Buffer?: boolean;
        crypto?: boolean;
        module?: boolean;
        clearImmediate?: boolean;
        setImmediate?: boolean
        clearTimeout?: boolean;
        setTimeout?: boolean;
        __dirname?: boolean;
        __filename?: boolean;
    };
}

const config: WebpackConfig = {
    module: {
        rules: [
            {
                test: /\.js$/,
                loader: 'source-map-loader',
                exclude: [
                    // these packages have problems with their sourcemaps
                    root('node_modules/@angular'),
                    root('node_modules/rxjs'),
                ],
            },
            {
                test: /\.ts$/,
                loaders: !CONSTANTS.DLL && !CONSTANTS.DEV_SERVER ? ['@ngtools/webpack'] : [
                    'awesome-typescript-loader?{configFileName: "tsconfig.webpack.json"}',
                    'angular2-template-loader',
                    'angular-router-loader?loader=system&genDir=compiled&aot=' + CONSTANTS.AOT,
                ],
                exclude: [/\.(spec|e2e|d)\.ts$/],
            },
            {test: /\.json$/, loader: 'json-loader'},
            {test: /\.html/, loader: 'raw-loader', exclude: [root('src/index.html')]},
            {test: /\.css$/, loader: 'raw-loader'},
            {test: /\.scss$/, loaders: ['to-string-loader', 'css-loader', 'sass-loader']},

        ],
    },
    plugins: [

        new AotPlugin({
            tsConfigPath: root('./src/tsconfig.browser.json'),
            skipCodeGeneration: !CONSTANTS.AOT,
        }),
        new ProgressPlugin(),
        new CheckerPlugin(),
        new DefinePlugin(CONSTANTS),
        new namedModulesPlugin(),
        new webpackMd5Hash(),
        new htmlWebpackPlugin({
            template: root('src/index.html'),
            metadata: {isDevServer: CONSTANTS.DEV_SERVER},
        }),

        ...ifConst((x: IConstants) => x.DEV_SERVER, () => [

                new DllReferencePlugin({
                    context: '.',
                    manifest: require(root(`./dll/polyfill-manifest.json`)),
                }),
                new DllReferencePlugin({
                    context: '.',
                    manifest: require(root(`./dll/vendor-manifest.json`)),
                })],
            []),

        ...ifConst((x: IConstants) => x.DLL, () => [

                new DllPlugin({
                    name: '[name]',
                    path: root('dll/[name]-manifest.json'),
                })],
            () => [

                new copyWebpackPlugin([
                        ...ifConst((x: IConstants) => !x.DEV_SERVER, [{from: root('src/index.html')}], []),
                        {from: root('src/assets'), to: 'assets'},
                        ...ifConst((x: IConstants) => x.DEV_SERVER, [{from: root('dll')}], [])
                    ],

                    {ignore: ['*dist_root/*']}),

                new copyWebpackPlugin([{from: 'src/assets/dist_root'}]),
            ]),

        ...ifConst((x: IConstants) => x.PROD, () => [

                new NoEmitOnErrorsPlugin(),
                new uglifyJsPlugin({
                    beautify: false,
                    comments: false,
                }),
                new DefinePlugin({
                    'process.env': {
                        'NODE_ENV': JSON.stringify('production'),
                    },
                })],
            []),

        ...ifConst((x: IConstants) => x.PROD && !x.E2E && !x.WATCH, () => [

                new BundleAnalyzerPlugin({analyzerPort: 5000})],
            []),
    ],

    cache: true,
    target: 'web',
    devtool: 'source-map',
    devServer: {

        contentBase: CONSTANTS.AOT ? './compiled' : './src',
        port: CONSTANTS.PORT,
        historyApiFallback: {

            disableDotRule: true,
        },
        stats: 'minimal',
        host: '0.0.0.0',
        watchOptions: {
            poll: undefined,
            aggregateTimeout: 300,
            ignored: /node_modules/,
        },
    },
    performance: {

        hints: false,
    },
    node: {

        global: true,
        process: true,
        Buffer: false,
        crypto: true,
        module: false,
        clearImmediate: false,
        setImmediate: false,
        clearTimeout: true,
        setTimeout: true,
    },
    resolve: {

        extensions: ['.ts', '.js', '.json'],
    },
    entry: ifConst((x: IConstants) => x.DLL, {

            app_assets: [root('./src/main.browser')],
            polyfill: [
                'sockjs-client',
                'ts-helpers',
                'zone.js',
                'core-js/client/shim.js',
                'core-js/es6/reflect.js',
                'core-js/es7/reflect.js',
                'querystring-es3',
                'strip-ansi',
                'url',
                'punycode',
                'events',
                'webpack-dev-server/client/socket.js',
                'webpack/hot/emitter.js',
                'zone.js/dist/long-stack-trace-zone.js',

            ],
            vendor: [
                '@angular/common',
                '@angular/compiler',
                '@angular/core',
                '@angular/forms',
                '@angular/http',
                '@angular/platform-browser',
                '@angular/platform-browser-dynamic',
                '@angular/router',
                'rxjs',

            ],
        }, {
            main: root('./src/main.browser.ts'),
        },
    ),
    output: ifConst((x: IConstants) => x.DLL, {

        path: root('dll'),
        filename: '[name].dll.js',
        library: '[name]',
    }, {

        path: root('dist/client'),
        filename: !CONSTANTS.PROD ? '[name].bundle.js' : '[name].[chunkhash].bundle.js',
        sourceMapFilename: !CONSTANTS.PROD ? '[name].bundle.map' : '[name].[chunkhash].bundle.map',
        chunkFilename: !CONSTANTS.PROD ? '[id].chunk.js' : '[id].[chunkhash].chunk.js',
    }),
};

module.exports = config;
