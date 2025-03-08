FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Api/WebApi/EksiSozluk.Api.WebApi/EksiSozluk.Api.WebApi.csproj", "src/Api/WebApi/EksiSozluk.Api.WebApi/"]
COPY ["src/Api/Infrastructure/EksiSozluk.Infrastructure.Persistence/EksiSozluk.Infrastructure.Persistence.csproj", "src/Api/Infrastructure/EksiSozluk.Infrastructure.Persistence/"]
COPY ["src/Api/Core/EksiSozluk.Api.Application/EksiSozluk.Api.Application.csproj", "src/Api/Core/EksiSozluk.Api.Application/"]
COPY ["src/Api/Core/EksiSozluk.Api.Domain/EksiSozluk.Api.Domain.csproj", "src/Api/Core/EksiSozluk.Api.Domain/"]
COPY ["src/Common/EksiSozluk.Common/EksiSozluk.Common.csproj", "src/Common/EksiSozluk.Common/"]
RUN dotnet restore "src/Api/WebApi/EksiSozluk.Api.WebApi/EksiSozluk.Api.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Api/WebApi/EksiSozluk.Api.WebApi"
RUN dotnet build "EksiSozluk.Api.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EksiSozluk.Api.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EksiSozluk.Api.WebApi.dll"]
