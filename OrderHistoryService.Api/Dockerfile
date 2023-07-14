FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrderHistoryService.API/OrderHistoryService.API.csproj", "OrderHistoryService.API/"]
RUN dotnet restore "OrderHistoryService.API/OrderHistoryService.API.csproj"
COPY . .
WORKDIR "/src/OrderHistoryService.API"
RUN dotnet build "OrderHistoryService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderHistoryService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderHistoryService.API.dll"]
