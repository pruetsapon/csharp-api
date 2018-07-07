Challenge Statement

accounting is a api example. Use to create / read / update / delete their api.

Prerequisites:
- Any IDE
- .NET Core SDK 2.1.4
- MSSQL

=====================================
Development Environment
=====================================

MSSQL:
- accounting application require a MSSQL database to store it's data. Make sure to update the file "appsettings.json" file.

accounting application:
- execute these commands.

dotnet restore
dotnet build
dotnet ef migrations add initDB
dotnet ef database update
dotnet run

- The application will be listening on http://localhost:3001
- Now you can call the api using any tool, like Postman, Curl, etc (See "Some Curl command examples" section)

=====================================
Some Curl command examples
=====================================

curl -i -H "Content-Type: application/json" -X POST -d "{'Remark':'expenditure', 'Amount':'50'}" http://localhost:3001/api/expenditure
curl -i -H "Content-Type: application/json" http://localhost:3001/api/expenditure