FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UrlShortener.Service/UrlShortener.Service.csproj", "UrlShortener.Service/"]
RUN dotnet restore "UrlShortener.Service/UrlShortener.Service.csproj"
COPY . .
WORKDIR "/src/UrlShortener.Service"
RUN dotnet build "UrlShortener.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortener.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortener.Service.dll"]
