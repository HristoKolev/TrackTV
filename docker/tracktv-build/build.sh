#!/usr/bin/env bash

cp ../../src ./backend-src -R
cp ../../client ./frontend-src -R

docker-compose build && docker-compose push

rm ./backend-src -R
rm ./frontend-src -R