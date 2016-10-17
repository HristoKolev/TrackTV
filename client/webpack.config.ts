/* tslint:disable: variable-name max-line-length */
/**
 * Try to not make your own edits to this file, use the constants folder instead.
 * If more constants should be added file an issue or create PR.
 */
import 'ts-helpers';
import {
    DEV_PORT,
    PROD_PORT,
    EXCLUDE_SOURCE_MAPS,
    HOST,
    USE_DEV_SERVER_PROXY,
    DEV_SERVER_PROXY_CONFIG,
    DEV_SOURCE_MAPS,
    PROD_SOURCE_MAPS,
    MY_CLIENT_PLUGINS,
    MY_CLIENT_PRODUCTION_PLUGINS,
    MY_CLIENT_RULES
} from './constants';

const {
    ContextReplacementPlugin,
    DefinePlugin,
    ProgressPlugin,
    NoErrorsPlugin, ProvidePlugin
} = require('webpack');

const CopyWebpackPlugin = require('copy-webpack-plugin');
const {ForkCheckerPlugin} = require('awesome-typescript-loader');
const NamedModulesPlugin = require('webpack/lib/NamedModulesPlugin');
const UglifyJsPlugin = require('webpack/lib/optimize/UglifyJsPlugin');

const root = require('./helpers.js').root;

const ENV = process.env.npm_lifecycle_event;
const AOT = ENV === 'build:aot' || ENV === 'build:aot:dev' || ENV === 'server:aot' || ENV === 'watch:aot';
const isProd = ENV === 'build:prod' || ENV === 'server:prod' || ENV === 'watch:prod' || ENV === 'build:aot';

let port : number;
if (isProd) {
    port = PROD_PORT;
} else {
    port = DEV_PORT;
}

const PORT = port;

console.log('PRODUCTION BUILD: ', isProd);
console.log('AOT: ', AOT);
if (ENV === 'webdev') {
    console.log(`Starting dev server on: http://${HOST}:${PORT}`);
}

const CONSTANTS = {
    AOT: AOT,
    ENV: isProd ? JSON.stringify('production') : JSON.stringify('development'),
    HOST: JSON.stringify(HOST),
    PORT: PORT
};

// type definition for WebpackConfig at the bottom
const clientConfig = function webpackConfig() : WebpackConfig {

    let config : WebpackConfig = Object.assign({});

    config.module = {
        rules: [
            {
                test: /\.js$/,
                loader: 'source-map-loader',
                exclude: [EXCLUDE_SOURCE_MAPS]
            },
            {
                test: /\.ts$/,
                loaders: [
                    'awesome-typescript-loader',
                    'angular2-template-loader',
                    'angular2-router-loader?loader=system&genDir=src/compiled/src/app&aot=' + AOT
                ],
                exclude: [/\.(spec|e2e|d)\.ts$/]
            },
            {test: /\.json$/, loader: 'json-loader'},
            {test: /\.html/, loader: 'raw-loader', exclude: [root('src/index.html')]},
            {test: /\.css$/, loader: 'raw-loader'},
            {test: /\.less$/, loader: 'style-loader!css-loader!less-loader'},

            {test: /\.woff(2)?(\?v=\d+\.\d+\.\d+)?$/, loader: "file-loader" },
            {test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/, loader: "file-loader" },
            {test: /\.eot(\?v=\d+\.\d+\.\d+)?$/, loader: "file-loader" },
            {test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, loader: "file-loader" },
            ...MY_CLIENT_RULES
        ]
    };

    config.plugins = [
        new ContextReplacementPlugin(
            /angular(\\|\/)core(\\|\/)(esm(\\|\/)src|src)(\\|\/)linker/,
            root('./src')
        ),
        new ProgressPlugin(),
        new ForkCheckerPlugin(),
        new DefinePlugin(CONSTANTS),
        new NamedModulesPlugin(),
        new CopyWebpackPlugin([{
            from: 'src/assets',
            to: 'assets'
        }, {
            from: 'src/index.html',
            to: ''
        }]),
        ...MY_CLIENT_PLUGINS
    ];

    if (isProd) {
        config.plugins.push(
            new NoErrorsPlugin(),
            new UglifyJsPlugin({
                beautify: false,
                comments: false
            }),
            ...MY_CLIENT_PRODUCTION_PLUGINS
        );
    }

    config.cache = true;
    isProd ? config.devtool = PROD_SOURCE_MAPS : config.devtool = DEV_SOURCE_MAPS;

    if (AOT) {
        config.entry = {
            main: './src/main.browser.aot'
        };
    } else {
        config.entry = {
            main: './src/main.browser'
        };
    }

    config.output = {
        path: root('dist/client'),
        filename: 'index.js'
    };

    config.devServer = {
        contentBase: AOT ? './src/compiled' : './src',
        port: CONSTANTS.PORT,
        historyApiFallback: true,
        host: '0.0.0.0'
    };

    if (USE_DEV_SERVER_PROXY) {
        Object.assign(config.devServer, {
            proxy: DEV_SERVER_PROXY_CONFIG
        });
    }

    config.node = {
        global: true,
        process: true,
        Buffer: false,
        crypto: true,
        module: false,
        clearImmediate: false,
        setImmediate: false,
        clearTimeout: true,
        setTimeout: true
    };

    config.resolve = {
        extensions: ['.ts', '.js', '.json']
    };

    return config;

}();

console.log('BUILDING APP');
module.exports = clientConfig;

// // Types
interface WebpackConfig {
    cache? : boolean;
    target? : string;
    devtool? : string;
    entry : any;
    externals? : any;
    output : any;
    module? : any;
    plugins? : Array<any>;
    resolve? : {
        extensions? : Array<string>;
    };
    devServer? : {
        contentBase? : string;
        port? : number;
        historyApiFallback? : boolean;
        hot? : boolean;
        inline? : boolean;
        proxy? : any;
        host? : string;
    };
    node? : {
        process? : boolean;
        global? : boolean;
        Buffer? : boolean;
        crypto? : boolean;
        module? : boolean;
        clearImmediate? : boolean;
        setImmediate? : boolean
        clearTimeout? : boolean;
        setTimeout? : boolean;
        __dirname? : boolean;
        __filename? : boolean;
    };
}
