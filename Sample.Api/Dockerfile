FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["sample-api/sample-api.csproj", "sample-api/"]
RUN dotnet restore "sample-api/sample-api.csproj"
COPY . .
WORKDIR "/src/sample-api"
RUN dotnet build "sample-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sample-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sample-api.dll"]
