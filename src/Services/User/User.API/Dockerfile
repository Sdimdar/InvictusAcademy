#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/User/User.API/User.API.csproj", "Services/User/User.API/"]
COPY ["Common/DataTransferLib/DataTransferLib.csproj", "Common/DataTransferLib/"]
COPY ["Common/GlobalExceptionHandler/GlobalExceptionHandler.csproj", "Common/GlobalExceptionHandler/"]
COPY ["Common/CommonStructures/CommonStructures.csproj", "Common/CommonStructures/"]
COPY ["Services/User/User.Application/User.Application.csproj", "Services/User/User.Application/"]
COPY ["Common/CommonRepository/CommonRepository.csproj", "Common/CommonRepository/"]
COPY ["Common/PasswordsHash/StringHash.csproj", "Common/PasswordsHash/"]
COPY ["Common/ServicesContracts/ServicesContracts.csproj", "Common/ServicesContracts/"]
COPY ["Services/CloudStorage/CloudStorage.Domain/CloudStorage.Domain.csproj", "Services/CloudStorage/CloudStorage.Domain/"]
COPY ["Services/Courses/Courses.Domain/Courses.Domain.csproj", "Services/Courses/Courses.Domain/"]
COPY ["Services/User/User.Domain/User.Domain.csproj", "Services/User/User.Domain/"]
COPY ["Services/Payment/Payment.Domain/Payment.Domain.csproj", "Services/Payment/Payment.Domain/"]
COPY ["Services/Request/Request.Domain/Request.Domain.csproj", "Services/Request/Request.Domain/"]
COPY ["Gateway/UserGateway/UserGateway.Application/UserGateway.Application.csproj", "Gateway/UserGateway/UserGateway.Application/"]
COPY ["Common/ExtendedHttpClient/ExtendedHttpClient.csproj", "Common/ExtendedHttpClient/"]
COPY ["Gateway/UserGateway/UserGateway.Domain/UserGateway.Domain.csproj", "Gateway/UserGateway/UserGateway.Domain/"]
COPY ["Services/Courses/Courses.Application/Courses.Application.csproj", "Services/Courses/Courses.Application/"]
COPY ["Services/Request/Request.Application/Request.Application.csproj", "Services/Request/Request.Application/"]
COPY ["Services/User/User.Infrastructure/User.Infrastructure.csproj", "Services/User/User.Infrastructure/"]
RUN dotnet restore "Services/User/User.API/User.API.csproj"
COPY . .
WORKDIR "/src/Services/User/User.API"
RUN dotnet build "User.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.API.dll"]