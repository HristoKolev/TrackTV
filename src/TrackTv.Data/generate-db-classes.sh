#!/usr/bin/env bash

schema2code \
-c "Server=dev-host.lan;Port=4201;Database=tracktv;Uid=tracktv;Pwd=test123;" \
-t "./db-classes-template.cshtml" \
-o "./Poco.cs" \
-n "TrackTv.Data" \
-p "TrackTvPocos" \
-m "TrackTvMetadata"