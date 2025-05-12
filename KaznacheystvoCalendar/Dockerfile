FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KaznacheystvoCalendar/KaznacheystvoCalendar.csproj", "KaznacheystvoCalendar/"]
RUN dotnet restore "KaznacheystvoCalendar/KaznacheystvoCalendar.csproj"
COPY . .
WORKDIR "/src/KaznacheystvoCalendar"
RUN dotnet build "KaznacheystvoCalendar.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KaznacheystvoCalendar.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KaznacheystvoCalendar.dll"]
