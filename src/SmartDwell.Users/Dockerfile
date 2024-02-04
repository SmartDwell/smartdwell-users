FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SmartDwell.Users/SmartDwell.Users.csproj", "SmartDwell.Users/"]
RUN dotnet restore "SmartDwell.Users/SmartDwell.Users.csproj"
COPY . .
WORKDIR "/src/SmartDwell.Users"
RUN dotnet build "SmartDwell.Users.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartDwell.Users.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartDwell.Users.dll"]
