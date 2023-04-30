FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyBot/MyBot.csproj", "MyBot/"]
COPY ["MyDoo.DAL.Interfaces/MyDoo.DAL.Interfaces.csproj", "MyDoo.DAL.Interfaces/"]
COPY ["MyDoo.Entities/MyDoo.Entities.csproj", "MyDoo.Entities/"]
COPY ["MyDoo.EFDal/MyDoo.EFDal.csproj", "MyDoo.EFDal/"]
RUN dotnet restore "MyBot/MyBot.csproj"
COPY . .
WORKDIR "/src/MyBot"
RUN dotnet build "MyBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyBot.dll"]
