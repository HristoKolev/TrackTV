$connectionString = "Server=vm5;Port=4202;Database=test;Uid=test;Pwd=test;";
$namespace = "TrackTv.Data.Tests";
$pocoClassName = "TestDbPocos";

ClassGenerator.PostgreSQL `
-c  $connectionString `
-t "../../src/TrackTv.WebServices/db-classes-template.txt" `
-o "./TestPocos.cs" `
-n $namespace `
-p $pocoClassName

#ClassGenerator.PostgreSQL `
#-c  $connectionString `
#-t "./tests-template.txt" `
#-o "./DbTests.cs" `
#-n $namespace `
#-p $pocoClassName
