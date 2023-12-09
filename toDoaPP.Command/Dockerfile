FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["toDoaPP.Command/toDoaPP.Command.csproj", "toDoaPP.Command/"]
COPY ["toDoApp.Database/toDoApp.Database.csproj", "toDoApp.Database/"]
COPY ["toDoApp.Core/toDoApp.Core.csproj", "toDoApp.Core/"]
RUN dotnet restore "toDoaPP.Command/toDoaPP.Command.csproj"
COPY . .
WORKDIR "/src/toDoaPP.Command"
RUN dotnet build "toDoaPP.Command.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "toDoaPP.Command.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "toDoaPP.Command.dll"]
