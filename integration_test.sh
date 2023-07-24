#!/bin/bash

# Run Migrations
cd /app/Sample.Api || exit
~/.dotnet/tools/dotnet-ef database update

# Run dotnet unit test cases
cd /app/ || exit
dotnet test