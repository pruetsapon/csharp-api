#!/usr/bin/env bash
dotnet clean
dotnet build

# Instrument assemblies inside 'test' folder to detect hits for source files inside 'src' folder
dotnet minicover instrument --workdir ../ --assemblies accounting.tests/**/bin/**/*.dll --sources accounting/**/*.cs --sources accounting/**/**/*.cs

# Reset hits count in case minicover was run for this project
dotnet minicover reset --workdir ../

dotnet test --no-build

# Uninstrument assemblies, it's important if you're going to publish or deploy build outputs
dotnet minicover uninstrument --workdir ../

# This command returns failure if the coverage is lower than the threshold
dotnet minicover report --workdir ../ --threshold 70