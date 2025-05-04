# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copier uniquement le fichier .csproj pour optimiser le cache Docker
COPY RoomBookingApi/*.csproj ./RoomBookingApi/
WORKDIR /app/RoomBookingApi
RUN dotnet restore

# Copier tout le code source
COPY . /app
RUN dotnet build -c Release -o /publish
RUN dotnet publish -c Release -o /publish

# Étape 2 : Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /publish .
EXPOSE 80
CMD ["dotnet", "RoomBookingApi.dll"]
