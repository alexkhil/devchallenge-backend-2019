FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app-source

# Copy csproj files
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do \
        echo $file \
        && mkdir -p src/${file%.*}/ \
        && mv $file src/${file%.*}/; \
    done

# Copy DI csproj files
COPY src/Di/*/*.csproj ./
RUN for file in $(ls *.csproj); do \
        echo $file \
        && mkdir -p src/Di/${file%.*}/ \
        && mv $file src/Di/${file%.*}/; \
    done

# Restore api csproj
RUN dotnet restore src/SC.DevChallenge.Api/SC.DevChallenge.Api.csproj

# Copy everything else and build
COPY . ./

# Publish to out folder
RUN dotnet publish src/SC.DevChallenge.Api/SC.DevChallenge.Api.csproj -c Release -o ../../out

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
COPY --from=build /app-source/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080/tcp

ENTRYPOINT ["dotnet", "SC.DevChallenge.Api.dll"]