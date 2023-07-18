# Microservices with C# ASP.Net 7

## Learning Goals

1. Create a best practices structure
2. Add basic set of services such as:
   - Logging
   - Swagger documentation
   - Error handling
   - Automapper
   - Fluent Validation
3. Add DB capabilities such as migrations and ORM
   - Migrations
   - Model queries
   - Seeds
4. Authentication and Authorization using Oauth2 with Keycloak
5. HttpClient setup
6. C# language basics and advanced

## Pre requisites

### Tech stack

- C# and Dotnet core
- Postgres
- Docker

### Knowledge

- REST API development
- Basic Design patterns

### Installations

- Install docker on machine using `...` and run the following to setup needed services:
  ```shell
  docker run -it ... -> postgres
  docker run -it ... -> keycloak
  ```

## Simple local setup

We use 1 command to set everything locally using `docker-compose`

Run:

```shell
docker-compose up
```

This spins up a web api, a keycloak server and a Postgres DB.

## Detailed Local Setup

### Environment setup

Setup the following variables needed for the application:

```shell
ASPNETCORE_ENVIRONMENT=<Development|Test>
DBContext=<Connection string for the postgres DB>
```

If you are switching branches to access other tutorials, please remember to update the env values.

### Migration/DB Setup

Install dotnet EF global tool using:
```shell
dotnet tool install -g dotnet-ef
```

Assuming the environment is set, please run:
```shell
dotnet ef database update
```

This should output the following:
```shell
Build started...
Build succeeded.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (16ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT EXISTS (SELECT 1 FROM pg_catalog.pg_class c JOIN pg_catalog.pg_namespace n ON n.oid=c.relnamespace WHERE c.relname='__EFMigrationsHistory');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT EXISTS (SELECT 1 FROM pg_catalog.pg_class c JOIN pg_catalog.pg_namespace n ON n.oid=c.relnamespace WHERE c.relname='__EFMigrationsHistory');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT "MigrationId", "ProductVersion"
      FROM "__EFMigrationsHistory"
      ORDER BY "MigrationId";
info: Microsoft.EntityFrameworkCore.Migrations[20405]
```

### IDE like Rider and Visual Studio

- Open project using sample-api.sln
- Let IDE restore and index packages
- The IDE picks up environment variables from `Properties/launchSettings.json`
- Click Run to start the web server. This should return the output as:
  ```shell
  Building...
  info: Microsoft.Hosting.Lifetime[14]
  Now listening on: http://0.0.0.0:5001
  info: Microsoft.Hosting.Lifetime[0]
  Application started. Press Ctrl+C to shut down.
  info: Microsoft.Hosting.Lifetime[0]
  Hosting environment: Development
  info: Microsoft.Hosting.Lifetime[0]
  ```
- Access http://localhost:5001/swagger to checkout the APIs

### CLI

- Use dotnet CLI to restore, build and run the web api
  ```shell
  dotnet restore # installs dependencies
  dotnet build # builds and verifies package
  dotnet publish # publishes DLL in /Sample.Api/bin/Debug/net7.0/Sample.Api.dll
  dotnet run ./bin/Debug/net7.0/Sample.Api.dll
  ```

- This should return the output as:
  ```shell
  Building...
  info: Microsoft.Hosting.Lifetime[14]
  Now listening on: http://0.0.0.0:5001
  info: Microsoft.Hosting.Lifetime[0]
  Application started. Press Ctrl+C to shut down.
  info: Microsoft.Hosting.Lifetime[0]
  Hosting environment: Development
  info: Microsoft.Hosting.Lifetime[0]
  ```

## Testing

### Unit testing

### Integration testing with docker-compose

## Branches with custom tutorials

1. `pattern/mediator`: Mediator pattern in C# Asp.net API using MediatR
2. `feat/keycloak`: Implementation of OAuth2 keycloak authentication using middleware
3. `validation/fluent`: Rule based fluent validations
4. `test/xunit`: Unit and Integration tests written using xunit with coverage integration
5. `deployment/docker-k8s`: Deployment config using docker and k8s