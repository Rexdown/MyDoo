FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyDoo/MyDoo.csproj", "MyDoo/"]
COPY ["MyDoo.Bll/MyDoo.Bll.csproj", "MyDoo.Bll/"]
COPY ["MyDoo.Bll.Interfaces/MyDoo.Bll.Interfaces.csproj", "MyDoo.Bll.Interfaces/"]
COPY ["MyDoo.Entities/MyDoo.Entities.csproj", "MyDoo.Entities/"]
COPY ["MyDoo.DAL.Interfaces/MyDoo.DAL.Interfaces.csproj", "MyDoo.DAL.Interfaces/"]
COPY ["MyDoo.EFDal/MyDoo.EFDal.csproj", "MyDoo.EFDal/"]
RUN dotnet restore "MyDoo/MyDoo.csproj"
COPY . .
WORKDIR "/src/MyDoo"
RUN dotnet build "MyDoo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyDoo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyDoo.dll"]
