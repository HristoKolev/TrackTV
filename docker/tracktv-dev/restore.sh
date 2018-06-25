#!/usr/bin/env bash

docker-compose down

docker-compose -f docker-compose-tools.yml run --rm restore-frontend

docker-compose up -d && docker-compose logs -f
