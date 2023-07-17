FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5017

ENV ASPNETCORE_URLS http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RaitingService.API/RaitingService.API.csproj", "RaitingService.API/"]
RUN dotnet restore "RaitingService.API/RaitingService.API.csproj"
COPY . .
WORKDIR "/src/RaitingService.API"
RUN dotnet build "RaitingService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RaitingService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RaitingService.API.dll"]
