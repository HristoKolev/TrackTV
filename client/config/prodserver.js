"use strict";

const compression = require('compression'),
    express = require('express');

const {E2E_PORT, HOST, PROD_PORT, root} = require('./helpers');

const app = express();

app.use(compression());
app.use(express.static(root('dist/client')));

const renderIndex = (req, res) => res.sendFile(root('dist/client/index.html'));

app.get('/*', renderIndex);

const ENV = process.env.npm_lifecycle_event;

const PORT = ENV === 'e2e:server' ? E2E_PORT : PROD_PORT;

app.listen(PORT, () => {
    console.log(`Listening on: http://${HOST}:${PORT}`);
});
