# Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

ENV ASPNETCORE_URLS=http://+:80
WORKDIR /SolarWatch
COPY SolarWatch/SolarWatch.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /SolarWatch
COPY --from=build-env /SolarWatch/out .
ENTRYPOINT ["dotnet", "SolarWatch.dll"]