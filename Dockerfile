FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

LABEL version="2.0.0" description="new ecommerce version"


WORKDIR /src/backend


COPY *.sln ./
COPY TatooMarket.Api/*.csproj ./TatooMarket.Api/
COPY TatooMarket.Infrastructure/*.csproj ./TatooMarket.Infrastructure/
COPY TatooMarket.Domain/*.csproj ./TatooMarket.Domain/

RUN dotnet restore


COPY . .

WORKDIR /src/TatooMarket.Api
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "TatooMarket.Api.dll"]