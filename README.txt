Accounting Webapi

accounting is a api example. Use to create / read / update / delete their api.

Prerequisites:
- Any IDE
- .NET SDK 5.0
- MSSQL

=====================================
Development Environment
=====================================

MSSQL:
- accounting application require a MSSQL database to store it's data. Make sure to update the file "appsettings.json" file.

accounting application:
- On any terminal move to the "accounting" folder (the folder containing the "accounting.csproj" file) and execute these commands:

dotnet restore
dotnet build
dotnet ef migrations add initDB
dotnet ef database update
dotnet run

- The application will be listening on http://localhost:3001
- Now you can call the api using any tool, like Postman, Curl, etc (See "Some Curl command examples" section)

=====================================
To run unit tests
=====================================

- On any terminal move to the "accounting.tests" folder (the folder containing the "accounting.tests.csproj" file) and execute these commands:

dotnet restore
dotnet build
dotnet test

- To check code coverage, execute the batch script:

coverage.sh

=====================================
Some Curl command examples
=====================================

curl -i -H "Content-Type: application/json" -X POST -d "{'Remark':'expenditure', 'Amount':'50'}" http://localhost:3001/api/expenditure
curl -i -H "Content-Type: application/json" http://localhost:3001/api/expenditure
