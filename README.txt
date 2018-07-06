Challenge Statement

web-api-example is a api example. Use to create / read / update / delete their api.

Prerequisites:
- Any IDE
- .NET Core SDK 2.1.4
- MSSQL

=====================================
Development Environment
=====================================

MSSQL:
- web-api-example application require a MSSQL database to store it's data. Make sure to update the file "appsettings.json" file.

web-api-example application:
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

curl -i -H "Content-Type: application/json" -H "Authorization: Bearer your_token" -X POST -d "{'Name':'Docker', 'Lastname':'xyz'}" http://localhost:3001/api/customer
curl -i -H "Content-Type: application/json" -H "Authorization: Bearer your_token" http://localhost:3001/api/customer