"use strict";

const compression = require('compression'),
    express = require('express'),
    path = require('path');

const {E2E_PORT, HOST, PROD_PORT} = require('./helpers');

const app = express();

app.use(compression());
app.use(express.static('dist/client'));

const renderIndex = (req, res) => res.sendFile(path.resolve(__dirname, 'dist/client/index.html'));

app.get('/*', renderIndex);

const ENV = process.env.npm_lifecycle_event;

const PORT = ENV === 'e2e:server' ? E2E_PORT : PROD_PORT;

app.listen(PORT, () => {
    console.log(`Listening on: http://${HOST}:${PORT}`);
});
