FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5014

ENV ASPNETCORE_URLS http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DeliveryService.API/DeliveryService.API.csproj", "DeliveryService.API/"]
RUN dotnet restore "DeliveryService.API/DeliveryService.API.csproj"
COPY . .
WORKDIR "/src/DeliveryService.API"
RUN dotnet build "DeliveryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliveryService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryService.API.dll"]
