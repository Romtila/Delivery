FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5011

ENV ASPNETCORE_URLS http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AdministratorService.API/AdministratorService.API.csproj", "AdministratorService.API/"]
RUN dotnet restore "AdministratorService.API/AdministratorService.API.csproj"
COPY . .
WORKDIR "/src/AdministratorService.API"
RUN dotnet build "AdministratorService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdministratorService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdministratorService.API.dll"]
