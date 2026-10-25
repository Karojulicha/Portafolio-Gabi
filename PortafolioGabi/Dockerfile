# ---------- Etapa de compilación ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restaurar dependencias (se cachea si el .csproj no cambia)
COPY PortafolioGabi.csproj ./
RUN dotnet restore PortafolioGabi.csproj

# Copiar el resto y publicar en Release
COPY . ./
RUN dotnet publish PortafolioGabi.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---------- Etapa de ejecución ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./

# Puerto por defecto (el hosting puede sobrescribirlo con la variable PORT)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "PortafolioGabi.dll"]
