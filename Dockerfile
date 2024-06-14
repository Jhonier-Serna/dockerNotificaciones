# Usar la imagen base de ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Usar la imagen de SDK de ASP.NET Core para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MsNotificaciones/MsNotificaciones.csproj", "MsNotificaciones/"]
RUN dotnet restore "MsNotificaciones/MsNotificaciones.csproj"
COPY . .
WORKDIR "/src/MsNotificaciones"
RUN dotnet build "MsNotificaciones.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsNotificaciones.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsNotificaciones.dll"]
