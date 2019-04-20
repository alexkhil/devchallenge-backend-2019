FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8080

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src

COPY ["src/Di/SC.DevChallenge.Configuration.Di/SC.DevChallenge.Configuration.Di.csproj", "src/Di/SC.DevChallenge.Configuration.Di/"]
COPY ["src/Di/SC.DevChallenge.DataAccess.Di/SC.DevChallenge.DataAccess.Di.csproj", "src/Di/SC.DevChallenge.DataAccess.Di/"]
COPY ["src/Di/SC.DevChallenge.MediatR.Di/SC.DevChallenge.MediatR.Di.csproj", "src/Di/SC.DevChallenge.MediatR.Di/"]
COPY ["src/Di/SC.DevChallenge.Domain.Di/SC.DevChallenge.Domain.Di.csproj", "src/Di/SC.DevChallenge.Domain.Di/"]

COPY ["src/SC.DevChallenge.Api/SC.DevChallenge.Api.csproj", "src/SC.DevChallenge.Api/"]
#COPY ["src/SC.DevChallenge.Api/Input/data.csv", "src/SC.DevChallenge.Api/"]
COPY ["src/SC.DevChallenge.Configuration/SC.DevChallenge.Configuration.csproj", "src/SC.DevChallenge.Configuration/"]
COPY ["src/SC.DevChallenge.Configuration.Abstractions/SC.DevChallenge.Configuration.Abstractions.csproj", "src/SC.DevChallenge.Configuration.Abstractions/"]
COPY ["src/SC.DevChallenge.DataAccess.Abstractions/SC.DevChallenge.DataAccess.Abstractions.csproj", "src/SC.DevChallenge.DataAccess.Abstractions/"]
COPY ["src/SC.DevChallenge.DataAccess.EF/SC.DevChallenge.DataAccess.EF.csproj", "src/SC.DevChallenge.DataAccess.EF/"]
COPY ["src/SC.DevChallenge.Domain/SC.DevChallenge.Domain.csproj", "src/SC.DevChallenge.Domain/"]
COPY ["src/SC.DevChallenge.Dto/SC.DevChallenge.Dto.csproj", "src/SC.DevChallenge.Dto/"]
COPY ["src/SC.DevChallenge.ExceptionHandler/SC.DevChallenge.ExceptionHandler.csproj", "src/SC.DevChallenge.ExceptionHandler/"]
COPY ["src/SC.DevChallenge.Mapping/SC.DevChallenge.Mapping.csproj", "src/SC.DevChallenge.Mapping/"]
COPY ["src/SC.DevChallenge.Mapping.Abstractions/SC.DevChallenge.Mapping.Abstractions.csproj", "src/SC.DevChallenge.Mapping.Abstractions/"]
COPY ["src/SC.DevChallenge.MediatR.Behaviors/SC.DevChallenge.MediatR.Behaviors.csproj", "src/SC.DevChallenge.MediatR.Behaviors/"]
COPY ["src/SC.DevChallenge.MediatR.Core/SC.DevChallenge.MediatR.Core.csproj", "src/SC.DevChallenge.MediatR.Core/"]
COPY ["src/SC.DevChallenge.MediatR.Queries/SC.DevChallenge.MediatR.Queries.csproj", "src/SC.DevChallenge.MediatR.Queries/"]
COPY ["src/SC.DevChallenge.Specification.Abstractions/SC.DevChallenge.Specification.Abstractions.csproj", "src/SC.DevChallenge.Specification.Abstractions/"]
COPY ["src/SC.DevChallenge.Specification/SC.DevChallenge.Specification.csproj", "src/SC.DevChallenge.Specification/"]

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