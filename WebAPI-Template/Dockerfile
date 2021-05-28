#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

ARG BUilDCONFIG=Release
ARG VERSION=1.0.0

COPY WebAPI-Template.csproj /build/

RUN dotnet restore ./build/WebAPI-Template.csproj

COPY . ./build/
WORKDIR /build/

RUN dotnet publish ./WebAPI-Template.csproj -c ${BUilDCONFIG} -o out /p:Version=${VERSION}

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /build/out .
ENTRYPOINT ["dotnet", "WebAPI-Template.dll"]