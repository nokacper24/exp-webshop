#! /usr/bin/bash

source .env
echo 'DROP DATABASE IF EXISTS webshop; CREATE DATABASE webshop;' \
| docker compose exec -T db mysql -p${MYSQL_ROOT_PASSWORD}