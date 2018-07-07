dotnet clean
dotnet build
dotnet minicover instrument --workdir ../ --assemblies accounting.tests/**/bin/**/*.dll --sources accounting/**/*.cs --sources accounting/**/**/*.cs

dotnet minicover reset --workdir ../

dotnet test
dotnet minicover uninstrument --workdir ../
dotnet minicover report --workdir ../ --threshold 70