FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app

COPY *.csproj ./
COPY /NuGet.Config ./

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

COPY --from=build-env /app/out ./

EXPOSE 80

ENTRYPOINT [ "dotnet", "api.dll" ]