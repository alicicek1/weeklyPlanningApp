FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["toDoApp.Api/toDoApp.Api.csproj", "toDoApp.Api/"]
COPY ["toDoApp.Database/toDoApp.Database.csproj", "toDoApp.Database/"]
COPY ["toDoApp.Core/toDoApp.Core.csproj", "toDoApp.Core/"]
RUN dotnet restore "toDoApp.Api/toDoApp.Api.csproj"
COPY . .
WORKDIR "/src/toDoApp.Api"
RUN dotnet build "toDoApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "toDoApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "toDoApp.Api.dll"]
