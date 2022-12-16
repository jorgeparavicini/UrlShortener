FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UrlShortener.API/UrlShortener.API.csproj", "UrlShortener.API/"]
RUN dotnet restore "UrlShortener.API/UrlShortener.API.csproj"
COPY . .
WORKDIR "/src/UrlShortener.API"
RUN dotnet build "UrlShortener.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortener.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortener.API.dll"]
