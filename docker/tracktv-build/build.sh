#!/usr/bin/env bash

cp ../../src ./backend/src -R

docker-compose build && docker-compose push

rm ./backend/src -rf

