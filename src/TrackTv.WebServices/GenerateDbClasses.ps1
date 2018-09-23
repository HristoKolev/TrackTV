ClassGenerator.PostgreSQL `
-c "Server=vm5;Port=4201;Database=tracktv;Uid=tracktv;Pwd=test123;" `
-t "./db-classes-template.txt" `
-o "../TrackTv.Data/Poco.cs" `
-n "TrackTv.Data" `
-p "DbPocos"