#!/usr/bin/env bash

connectionString="Server=vm5;Port=4202;Database=test;Uid=test;Pwd=test;";
namespace="TrackTv.Data.Tests";
pocoClassName="TestDbPocos";
metadataClassName="TestDbMetadata";

schema2code \
-c  connectionString \
-t "../../src/TrackTv.WebServices/db-classes-template.txt" \
-o "./TestPocos.cs" \
-n namespace \
-p pocoClassName \
-m metadataClassName

schema2code \
-c connectionString \
-t "./tests-template.txt" \
-o "./DbTests.cs" \
-n namespace \
-p pocoClassName \
-m metadataClassName
