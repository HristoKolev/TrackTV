const path = require('path');
const fs = require('fs');
const {DefinePlugin, DllPlugin, DllReferencePlugin, ProgressPlugin, NoEmitOnErrorsPlugin,} = require('webpack');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const {CheckerPlugin} = require('awesome-typescript-loader');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const NamedModulesPlugin = require('webpack/lib/NamedModulesPlugin');
const UglifyJsPlugin = require('webpack/lib/optimize/UglifyJsPlugin');
const {BundleAnalyzerPlugin} = require('webpack-bundle-analyzer');
const WebpackMd5Hash = require('webpack-md5-hash');
const {AotPlugin} = require('@ngtools/webpack');

import 'ts-helpers';
import { CONSTANTS, ifConst, root, testDll } from './constants';

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
            {
                test: /\.scss$/,
                loaders: ['to-string-loader', 'css-loader', 'sass-loader'],
            },

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
        new NamedModulesPlugin(),
        new WebpackMd5Hash(),
        new HtmlWebpackPlugin({
            template: 'src/index.html',
            metadata: {isDevServer: CONSTANTS.DEV_SERVER},
        }),

        ...ifConst(x => x.DEV_SERVER, [

                new DllReferencePlugin({
                    context: '.',
                    manifest: require(`./dll/polyfill-manifest.json`),
                }),
                new DllReferencePlugin({
                    context: '.',
                    manifest: require(`./dll/vendor-manifest.json`),
                }),],
            []),

        ...ifConst(x => x.DLL, [

                new DllPlugin({
                    name: '[name]',
                    path: root('dll/[name]-manifest.json'),
                }),],
            [

                new CopyWebpackPlugin([
                        ...ifConst(x => !x.DEV_SERVER, [{from: 'src/index.html'}], []),
                        {from: 'src/assets', to: 'assets',},
                        ...ifConst(x => x.DEV_SERVER, [{from: 'dll'}], [])],
                    {ignore: ['*dist_root/*']}),

                new CopyWebpackPlugin([{from: 'src/assets/dist_root'}]),
            ]),

        ...ifConst(x => x.PROD, [

                new NoEmitOnErrorsPlugin(),
                new UglifyJsPlugin({
                    beautify: false,
                    comments: false,
                }),],
            []),

        ...ifConst(x => x.PROD && !x.E2E && !x.WATCH, [

                new BundleAnalyzerPlugin({analyzerPort: 5000}),],
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
    entry: ifConst(x => x.DLL, {
            app_assets: ['./src/main.browser'],
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
    output: ifConst(x => x.DLL, {
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
