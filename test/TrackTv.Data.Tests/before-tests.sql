﻿drop schema "public" cascade;

create schema "public";

create table "public"."test1" (
	test_id serial,
	test_name1 varchar(255) not null,
	test_name2 varchar(255),
	test_date1 date not null,
	test_date2 date,
	test_timestamp1 timestamp not null,
	test_timestamp2 timestamp,
	test_boolean1 boolean not null,
	test_boolean2 boolean,
  test_integer1 integer,
  test_integer2 integer not NULL,
  test_bigint1 bigint,
  test_bigint2 bigint not NULL,
  test_text1 text,
  test_text2 text not NULL,
  test_real1 real,
  test_real2 real not NULL,
  test_double1 double precision,
  test_double2 double precision NOT NULL,
  test_char1 char,
  test_char2 char NOT NULL,

	primary key (test_id)
);

CREATE TABLE "public"."test2" (
  test_id serial,
  test_name text NOT NULL,
  test_date timestamp NOT NULL,
  PRIMARY KEY (test_id)
);

DROP view if exists "public"."view1";

create or REPLACE view "public"."view1" as select
   test_name1,
   test_name2,
   test_date1,
   test_date2,
   test_timestamp1,
   test_timestamp2,
   test_boolean1,
   test_boolean2,
   test_integer1,
   test_integer2,
   test_bigint1,
   test_bigint2,
   test_text1,
   test_text2,
   test_real1,
   test_real2,
   test_double1,
   test_double2,
   test_char1,
   test_char2,
   test_name,
   test_date
from test1 JOIN test2 on test1.test_id = test2.test_id;

