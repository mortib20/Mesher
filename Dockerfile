FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Packages.props", "."]

COPY ["Mesher.Main/Mesher.Main.csproj", "Mesher.Main/"]
COPY ["Mesher.Global/Mesher.Global.csproj", "Mesher.Global/"]
COPY ["Mesher.Mesh/Mesher.Mesh.csproj", "Mesher.Mesh/"]

RUN dotnet restore "Mesher.Main/Mesher.Main.csproj"
COPY . .
WORKDIR "/src/Mesher.Main"
RUN dotnet build "Mesher.Main.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Mesher.Main.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mesher.Main.dll"]