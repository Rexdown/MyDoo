FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["../MyDoo.DataAccess.Service2/MyDoo.DataAccess.Service2.csproj", "../MyDoo.DataAccess.Service2/"]
COPY ["../MyDoo.Entities/MyDoo.Entities.csproj", "../MyDoo.Entities/"]
RUN dotnet restore "../MyDoo.DataAccess.Service2/MyDoo.DataAccess.Service2.csproj"
COPY . .
WORKDIR "/src/../MyDoo.DataAccess.Service2"
RUN dotnet build "MyDoo.DataAccess.Service2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyDoo.DataAccess.Service2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyDoo.DataAccess.Service2.dll"]
