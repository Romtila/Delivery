﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5012

ENV ASPNETCORE_URLS http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CourierService.API/CourierService.API.csproj", "CourierService.API/"]
RUN dotnet restore "CourierService.API/CourierService.API.csproj"
COPY . .
WORKDIR "/src/CourierService.API"
RUN dotnet build "CourierService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CourierService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CourierService.API.dll"]
