del Migrations\* /q
del database-schema.sql

dotnet ef migrations add "Created"
dotnet ef migrations script -o "database-schema.sql"
