import 'ts-helpers';
import { DEV_PORT, HOST, PROD_PORT } from './constants';

const CONSTANTS = (function() {

    const EVENT = process.env.npm_lifecycle_event || '';

    const PROD = EVENT.includes('prod');

    return {
        AOT: EVENT.includes('aot'),
        ENV: PROD ? JSON.stringify('production') : JSON.stringify('development'),
        HOST: JSON.stringify(HOST),
        PORT: PROD ? PROD_PORT : DEV_PORT,
        DEV_SERVER: EVENT.includes('webdev'),
        DLL: EVENT.includes('dll'),
        E2E: EVENT.includes('e2e'),
        WATCH: process.argv.join('').indexOf('watch') > -1,
        PROD: PROD,
    }
}());

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

const root = (args) => path.join.apply(path, [path.resolve(__dirname)].concat(args));

function testDll() {

    if (!fs.existsSync('./dll/polyfill.dll.js') || !fs.existsSync('./dll/vendor.dll.js')) {
        throw "DLL files do not exist, please use 'npm run build:dll' once to generate dll files.";
    }
}

if (CONSTANTS.DEV_SERVER) {
    testDll();
    console.log(`Starting dev server on: http://${CONSTANTS.HOST}:${CONSTANTS.PORT}`);
}

const COPY_FOLDERS: any[] = [
    {from: 'src/assets', to: 'assets'},

];

if (!CONSTANTS.DEV_SERVER) {
    COPY_FOLDERS.unshift({from: 'src/index.html'});
} else {
    COPY_FOLDERS.push({from: 'dll'});
}

console.log('PRODUCTION BUILD: ', CONSTANTS.PROD);
console.log('AOT: ', CONSTANTS.AOT);

const clientConfig = function webpackConfig(): WebpackConfig {

    const config: WebpackConfig = Object.assign({});

    config.module = {
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
    };

    config.plugins = [
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
    ];

    config.cache = true;
    config.target = 'web';
    config.devtool = 'source-map';

    config.devServer = {
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
    };

    config.performance = {
        hints: false,
    };

    config.node = {
        global: true,
        process: true,
        Buffer: false,
        crypto: true,
        module: false,
        clearImmediate: false,
        setImmediate: false,
        clearTimeout: true,
        setTimeout: true,
    };

    config.resolve = {
        extensions: ['.ts', '.js', '.json'],
    };

    if (CONSTANTS.DEV_SERVER) {
        config.plugins.push(
            new DllReferencePlugin({
                context: '.',
                manifest: require(`./dll/polyfill-manifest.json`),
            }),
            new DllReferencePlugin({
                context: '.',
                manifest: require(`./dll/vendor-manifest.json`),
            }),
        );
    }

    if (CONSTANTS.DLL) {
        config.plugins.push(
            new DllPlugin({
                name: '[name]',
                path: root('dll/[name]-manifest.json'),
            }),
        );
    }

    if (!CONSTANTS.DLL) {
        config.plugins.push(
            new CopyWebpackPlugin(COPY_FOLDERS, {ignore: ['*dist_root/*']}),
            new CopyWebpackPlugin([{from: 'src/assets/dist_root'}]),
        );
    }

    if (CONSTANTS.PROD) {
        config.plugins.push(
            new NoEmitOnErrorsPlugin(),
            new UglifyJsPlugin({
                beautify: false,
                comments: false,
            }),
        );
    }

    if (CONSTANTS.PROD && !CONSTANTS.E2E && !CONSTANTS.WATCH) {

        config.plugins.push(
            new BundleAnalyzerPlugin({analyzerPort: 5000}),
        );
    }

    if (CONSTANTS.DLL) {
        config.entry = {
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
        };
    }

    if (!CONSTANTS.DLL) {
        config.entry = {
            main: root('./src/main.browser.ts'),
        };
    }

    if (!CONSTANTS.DLL) {
        config.output = {
            path: root('dist/client'),
            filename: !CONSTANTS.PROD ? '[name].bundle.js' : '[name].[chunkhash].bundle.js',
            sourceMapFilename: !CONSTANTS.PROD ? '[name].bundle.map' : '[name].[chunkhash].bundle.map',
            chunkFilename: !CONSTANTS.PROD ? '[id].chunk.js' : '[id].[chunkhash].chunk.js',
        };
    }

    if (CONSTANTS.DLL) {
        config.output = {
            path: root('dll'),
            filename: '[name].dll.js',
            library: '[name]',
        };
    }

    return config;

}();

if (CONSTANTS.DLL) {
    console.log('BUILDING DLLs');
} else {
    console.log('BUILDING APP');
}

module.exports = clientConfig;
