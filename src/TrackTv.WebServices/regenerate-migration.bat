del Migrations\** /q
del Migrations\AppDb\* /q

del auth-database-schema.sql
del app-database-schema.sql

dotnet ef migrations add -c AuthContext "AuthContext_Created"
dotnet ef migrations script -c AuthContext -o "auth-database-schema.sql"

dotnet ef migrations add -c AppDbContext "AppDbContext_Created"
dotnet ef migrations script -c AppDbContext -o "app-database-schema.sql"