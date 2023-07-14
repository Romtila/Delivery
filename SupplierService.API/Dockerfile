FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SupplierService.API/SupplierService.API.csproj", "SupplierService.API/"]
RUN dotnet restore "SupplierService.API/SupplierService.API.csproj"
COPY . .
WORKDIR "/src/SupplierService.API"
RUN dotnet build "SupplierService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SupplierService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupplierService.API.dll"]
