# Build du frontend
FROM node:20-alpine AS frontend
WORKDIR /app
COPY frontend/package*.json ./
RUN npm install
COPY frontend/ .
RUN npm run build

# Build du backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY backend/*.csproj ./
RUN dotnet restore
COPY backend/ .
RUN dotnet publish -c Release -o /app/publish

# Image finale
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=frontend /app/dist ./wwwroot
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
CMD ["dotnet", "backend.dll"]
