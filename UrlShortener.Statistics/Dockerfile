FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5025
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UrlShortener.Statistics/UrlShortener.Statistics.csproj", "UrlShortener.Statistics/"]
RUN dotnet restore "UrlShortener.Statistics/UrlShortener.Statistics.csproj"
COPY . .
WORKDIR "/src/UrlShortener.Statistics"
RUN dotnet build "UrlShortener.Statistics.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortener.Statistics.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortener.Statistics.dll"]
