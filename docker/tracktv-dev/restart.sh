#!/usr/bin/env bash

source ./env.sh

docker-compose down
docker-compose up --build -d