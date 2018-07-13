#!/usr/bin/env bash

base_path=$(readlink -f $(pwd)/../../)

export backend_src=$base_path/src
export frontend_src=$base_path/client

export db_username=tracktv
export db_password=test123
