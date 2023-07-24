FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy solution file and NuGet config.
COPY *.sln .

# Install package
RUN dotnet tool install --global dotnet-ef 

# Copy project files and restore solution structure.
COPY Sample.Api/*.csproj /app/Sample.Api/
COPY Sample.Tests/*.csproj /app/Sample.Tests/
COPY integration_test.sh /app/

# Restore packages.
RUN dotnet restore

# Copy everything else for both apps
COPY Sample.Api/ /app/Sample.Api/
COPY Sample.Tests/ /app/Sample.Tests/

ENTRYPOINT ["bash", "-c"]
