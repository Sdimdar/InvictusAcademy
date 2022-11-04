FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["Gateway/SessionGatewayService/SessionGatewayService.API/SessionGatewayService.API.csproj", "Gateway/SessionGatewayService/SessionGatewayService.API/"]
COPY ["Gateway/SessionGatewayService/SessionGatewayService.Application/SessionGatewayService.Application.csproj", "Gateway/SessionGatewayService/SessionGatewayService.Application/"]
COPY ["Gateway/SessionGatewayService/SessionGatewayService.Domain/SessionGatewayService.Domain.csproj", "Gateway/SessionGatewayService/SessionGatewayService.Domain/"]
COPY ["Gateway/SessionGatewayService/SessionGatewayService.Infrastructure/SessionGatewayService.Infrastructure.csproj", "Gateway/SessionGatewayService/SessionGatewayService.Infrastructure/"]
COPY ["Common/DataTransferLib/DataTransferLib.csproj", "Common/DataTransferLib/"]
COPY ["Common/PasswordsHash/StringHash.csproj", "Common/PasswordsHash/"]
COPY ["Common/ServicesContracts/ServicesContracts.csproj", "Common/ServicesContracts/"]
COPY ["Services/Request/Request.Domain/Request.Domain.csproj", "Services/Request/Request.Domain/"]
RUN dotnet restore "Gateway/SessionGatewayService/SessionGatewayService.API/SessionGatewayService.API.csproj"
COPY . .
WORKDIR "/src/Gateway/SessionGatewayService/SessionGatewayService.API"
RUN dotnet build "SessionGatewayService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SessionGatewayService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SessionGatewayService.API.dll"]