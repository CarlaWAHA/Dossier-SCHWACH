# Étape 1 : build du frontend
FROM node:20 AS frontend
WORKDIR /app
COPY frontend ./frontend
RUN cd frontend && npm install && npm run build

# Étape 2 : build du backend
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY backend ./backend
COPY --from=frontend /app/frontend/dist ./backend/wwwroot
RUN dotnet publish ./backend/backend.csproj -c Release -o /app/publish

# Étape 3 : image finale
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "backend.dll"]
