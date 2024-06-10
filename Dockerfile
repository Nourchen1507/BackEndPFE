# Utiliser l'image de base officielle .NET pour le build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Définir des variables d'environnement pour les informations d'authentification
ARG USERNAME
ARG PASSWORD
ENV USERNAME=${USERNAME}
ENV PASSWORD=${PASSWORD}

# Copier les fichiers du projet et restaurer les dépendances
COPY . ./
RUN dotnet restore

# Construire l'application
RUN dotnet publish -c Release -o out

# Utiliser l'image de base officielle .NET pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Exposer le port sur lequel l'application écoute
EXPOSE 80

# Commande d'entrée pour exécuter l'application
ENTRYPOINT ["dotnet", "MyBackendApp.dll"]

