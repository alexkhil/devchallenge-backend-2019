FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/SC.DevChallenge.Api/SC.DevChallenge.Api.csproj", "src/SC.DevChallenge.Api/"]
COPY ["src/SC.DevChallenge.Dto/SC.DevChallenge.Dto.csproj", "src/SC.DevChallenge.Dto/"]
COPY ["src/Di/SC.DevChallenge.Configuration.Di/SC.DevChallenge.Configuration.Di.csproj", "src/Di/SC.DevChallenge.Configuration.Di/"]
COPY ["src/SC.DevChallenge.Configuration/SC.DevChallenge.Configuration.csproj", "src/SC.DevChallenge.Configuration/"]
COPY ["src/SC.DevChallenge.Configuration.Abstractions/SC.DevChallenge.Configuration.Abstractions.csproj", "src/SC.DevChallenge.Configuration.Abstractions/"]
COPY ["src/SC.DevChallenge.MediatR.Queries/SC.DevChallenge.MediatR.Queries.csproj", "src/SC.DevChallenge.MediatR.Queries/"]
COPY ["src/SC.DevChallenge.Mapping/SC.DevChallenge.Mapping.csproj", "src/SC.DevChallenge.Mapping/"]
COPY ["src/SC.DevChallenge.Mapping.Abstractions/SC.DevChallenge.Mapping.Abstractions.csproj", "src/SC.DevChallenge.Mapping.Abstractions/"]
COPY ["src/Di/SC.DevChallenge.DataAccess.Di/SC.DevChallenge.DataAccess.Di.csproj", "src/Di/SC.DevChallenge.DataAccess.Di/"]
COPY ["src/Di/SC.DevChallenge.MediatR.Di/SC.DevChallenge.MediatR.Di.csproj", "src/Di/SC.DevChallenge.MediatR.Di/"]
COPY ["src/SC.DevChallenge.MediatR.Behaviors/SC.DevChallenge.MediatR.Behaviors.csproj", "src/SC.DevChallenge.MediatR.Behaviors/"]
RUN dotnet restore "src/SC.DevChallenge.Api/SC.DevChallenge.Api.csproj"
COPY . .
WORKDIR "/src/src/SC.DevChallenge.Api"
RUN dotnet build "SC.DevChallenge.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SC.DevChallenge.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SC.DevChallenge.Api.dll"]