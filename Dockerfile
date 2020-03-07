FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ProSMan.Backend.sln ./
COPY ProSMan.Backend/ProSMan.Backend.API.csproj ProSMan.Backend/
COPY ProSMan.Backend.Core/ProSMan.Backend.Core.csproj ProSMan.Backend.Core/
COPY ProSMan.Backend.Domain/ProSMan.Backend.Domain.csproj ProSMan.Backend.Domain/
COPY ProSMan.Backend.Infrastructure/ProSMan.Backend.Infrastructure.csproj ProSMan.Backend.Infrastructure/
COPY ProSMan.Backend.Model/ProSMan.Backend.Model.csproj ProSMan.Backend.Model/
COPY ProSMan.Backend.UnitTests/ProSMan.Backend.UnitTests.csproj ProSMan.Backend.UnitTests/

RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProSMan.Backend.API.dll"]
