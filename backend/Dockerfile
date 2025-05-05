# === Build Stage ===
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier le fichier projet
COPY backend.csproj ./
RUN dotnet restore

# Copier tout le code (y compris appsettings.json)
COPY . ./

# Publier en mode Release
RUN dotnet publish -c Release -o /publish

# === Runtime Stage ===
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copier les fichiers publiés
COPY --from=build /publish .

# Exposer le port
EXPOSE 80

# Point d'entrée
ENTRYPOINT ["dotnet", "backend.dll"]
