FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Projections/EksiSozluk.Projections.FavoriteService/EksiSozluk.Projections.FavoriteService.csproj", "src/Projections/EksiSozluk.Projections.FavoriteService/"]
COPY ["src/Common/EksiSozluk.Common/EksiSozluk.Common.csproj", "src/Common/EksiSozluk.Common/"]
RUN dotnet restore "src/Projections/EksiSozluk.Projections.FavoriteService/EksiSozluk.Projections.FavoriteService.csproj"
COPY . .
WORKDIR "/src/src/Projections/EksiSozluk.Projections.FavoriteService"
RUN dotnet build "EksiSozluk.Projections.FavoriteService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EksiSozluk.Projections.FavoriteService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EksiSozluk.Projections.FavoriteService.dll"]
