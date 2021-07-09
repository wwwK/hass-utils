#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["/src/HassUtils/HassUtils.csproj", "/rn-build/HassUtils/"]
RUN dotnet restore "/rn-build/HassUtils/HassUtils.csproj"
COPY /src/HassUtils/ /rn-build/HassUtils/

WORKDIR /rn-build/HassUtils/
RUN dotnet build "HassUtils.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HassUtils.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HassUtils.dll"]
