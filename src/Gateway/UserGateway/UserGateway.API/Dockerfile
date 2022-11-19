FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateway/UserGateway/UserGateway.API/UserGateway.API.csproj", "Gateway/UserGateway/UserGateway.API/"]
COPY ["Common/DataTransferLib/DataTransferLib.csproj", "Common/DataTransferLib/"]
COPY ["Common/GlobalExceptionHandler/GlobalExceptionHandler.csproj", "Common/GlobalExceptionHandler/"]
COPY ["Common/ExtendedHttpClient/ExtendedHttpClient.csproj", "Common/ExtendedHttpClient/"]
COPY ["Gateway/UserGateway/UserGateway.Application/UserGateway.Application.csproj", "Gateway/UserGateway/UserGateway.Application/"]
COPY ["Common/PasswordsHash/StringHash.csproj", "Common/PasswordsHash/"]
COPY ["Common/ServicesContracts/ServicesContracts.csproj", "Common/ServicesContracts/"]
COPY ["Services/Courses/Courses.Domain/Courses.Domain.csproj", "Services/Courses/Courses.Domain/"]
COPY ["Common/CommonRepository/CommonRepository.csproj", "Common/CommonRepository/"]
COPY ["Common/CommonStructures/CommonStructures.csproj", "Common/CommonStructures/"]
COPY ["Services/User/User.Domain/User.Domain.csproj", "Services/User/User.Domain/"]
COPY ["Services/Request/Request.Domain/Request.Domain.csproj", "Services/Request/Request.Domain/"]
COPY ["Gateway/UserGateway/UserGateway.Domain/UserGateway.Domain.csproj", "Gateway/UserGateway/UserGateway.Domain/"]
COPY ["Gateway/UserGateway/UserGateway.Infrastructure/UserGateway.Infrastructure.csproj", "Gateway/UserGateway/UserGateway.Infrastructure/"]
RUN dotnet restore "Gateway/UserGateway/UserGateway.API/UserGateway.API.csproj"
COPY . .
WORKDIR "/src/Gateway/UserGateway/UserGateway.API"
RUN dotnet build "UserGateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserGateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserGateway.API.dll"]