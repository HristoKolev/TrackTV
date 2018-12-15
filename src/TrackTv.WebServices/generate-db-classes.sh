#!/usr/bin/env bash

schema2code \
-c "Server=vm5.lan;Port=4201;Database=tracktv;Uid=tracktv;Pwd=test123;" \
-t "./db-classes-template.cshtml" \
-o "../TrackTv.Data/Poco.cs" \
-n "TrackTv.Data" \
-p "TrackTvPocos" \
-m "TrackTvMetadata"