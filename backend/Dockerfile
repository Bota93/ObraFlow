FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore ObraFlow.slnx
RUN dotnet publish src/ObraFlow.Api/ObraFlow.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 10000

ENTRYPOINT ["dotnet", "ObraFlow.Api.dll"]
