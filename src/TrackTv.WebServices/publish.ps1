rm ./bin/publish/* -Recurse -Force

dotnet publish -c Release -r centos.7-x64 -o ./bin/publish

winscp /script=winscp-script.txt