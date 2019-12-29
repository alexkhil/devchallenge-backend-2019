ARG DotnetVersion=3.1
FROM mcr.microsoft.com/dotnet/core/sdk:$DotnetVersion-alpine AS build
WORKDIR /app

# Copy csproj files
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do \
        echo $file \
        && mkdir -p src/${file%.*}/ \
        && mv $file src/${file%.*}/; \
    done

# Restore api csproj
RUN dotnet restore ./src/SC.DevChallenge.Api/SC.DevChallenge.Api.csproj

# Copy everything else and build
COPY . ./

# Publish to out folder
RUN dotnet publish ./src/SC.DevChallenge.Api/SC.DevChallenge.Api.csproj -c Release --no-restore -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime:$DotnetVersion-alpine AS base
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080/tcp

ENTRYPOINT ["dotnet", "SC.DevChallenge.Api.dll"]
