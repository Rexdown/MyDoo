FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyDooBot2.0/MyDooBot2.0/MyDooBot2.0.csproj", "MyDooBot2.0/MyDooBot2.0/"]
COPY ["MyDoo.EFDal/MyDoo.EFDal.csproj", "MyDoo.EFDal/"]
COPY ["MyDoo.Entities/MyDoo.Entities.csproj", "MyDoo.Entities/"]
COPY ["MyDoo.DAL.Interfaces/MyDoo.DAL.Interfaces.csproj", "MyDoo.DAL.Interfaces/"]
RUN dotnet restore "MyDooBot2.0/MyDooBot2.0/MyDooBot2.0.csproj"
COPY . .
WORKDIR "/src/../MyDooBot2.0/MyDooBot2.0"
RUN dotnet build "MyDooBot2.0.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyDooBot2.0.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app    
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyDooBot2.0.dll"]
