del Migrations\ApplicationDb\* /q
del Migrations\** /q

del auth-database-schema.sql
del app-database-schema.sql

dotnet ef migrations add -c AuthContext "AuthContext_Created"
dotnet ef migrations script -c AuthContext -o "auth-database-schema.sql"

dotnet ef migrations add -c ApplicationDbContext "ApplicationDbContext_Created"
dotnet ef migrations script -c ApplicationDbContext -o "app-database-schema.sql"