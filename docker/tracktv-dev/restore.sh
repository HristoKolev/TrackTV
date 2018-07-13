#!/usr/bin/env bash

source ./env.sh

docker-compose down

docker-compose -f docker-compose-tools.yml run --rm restore-frontend
