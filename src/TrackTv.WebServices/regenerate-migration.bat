del Migrations\ApplicationDb\* /q
del Migrations\** /q

dotnet ef migrations add -c AuthContext "AuthContext_Created"
dotnet ef migrations script -c AuthContext -o "Migrations\auth-database-schema.sql"

dotnet ef migrations add -c ApplicationDbContext "ApplicationDbContext_Created"
dotnet ef migrations script -c ApplicationDbContext -o "Migrations\app-database-schema.sql"