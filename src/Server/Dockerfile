﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SmartDwell.Users.Server/SmartDwell.Users.Server.csproj", "SmartDwell.Users.Server/"]
COPY ["SmartDwell.Models/SmartDwell.Models.csproj", "SmartDwell.Models/"]
COPY ["SmartDwell.Contracts/SmartDwell.Contracts.csproj", "SmartDwell.Contracts/"]
RUN dotnet restore "SmartDwell.Users.Server/SmartDwell.Users.Server.csproj"
COPY . .
WORKDIR "/src/SmartDwell.Users.Server"
RUN dotnet build "SmartDwell.Users.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SmartDwell.Users.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartDwell.Users.Server.dll"]
