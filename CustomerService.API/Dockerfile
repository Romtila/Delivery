FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5013

ENV ASPNETCORE_URLS http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CustomerService.API/CustomerService.API.csproj", "CustomerService.API/"]
RUN dotnet restore "CustomerService.API/CustomerService.API.csproj"
COPY . .
WORKDIR "/src/CustomerService.API"
RUN dotnet build "CustomerService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerService.API.dll"]
