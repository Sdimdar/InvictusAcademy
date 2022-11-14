FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateway/AdminGateway.MVC/AdminGateway.MVC.csproj", "Gateway/AdminGateway.MVC/"]
COPY ["Common/DataTransferLib/DataTransferLib.csproj", "Common/DataTransferLib/"]
COPY ["Common/GlobalExceptionHandler/GlobalExceptionHandler.csproj", "Common/GlobalExceptionHandler/"]
COPY ["Common/ExtendedHttpClient/ExtendedHttpClient.csproj", "Common/ExtendedHttpClient/"]
COPY ["Common/ServicesContracts/ServicesContracts.csproj", "Common/ServicesContracts/"]
COPY ["Services/Courses/Courses.Domain/Courses.Domain.csproj", "Services/Courses/Courses.Domain/"]
COPY ["Common/CommonRepository/CommonRepository.csproj", "Common/CommonRepository/"]
COPY ["Common/CommonStructures/CommonStructures.csproj", "Common/CommonStructures/"]
COPY ["Services/User/User.Domain/User.Domain.csproj", "Services/User/User.Domain/"]
COPY ["Services/Request/Request.Domain/Request.Domain.csproj", "Services/Request/Request.Domain/"]
RUN dotnet restore "Gateway/AdminGateway.MVC/AdminGateway.MVC.csproj"
COPY . .
WORKDIR "/src/Gateway/AdminGateway.MVC"
RUN dotnet build "AdminGateway.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminGateway.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminGateway.MVC.dll"]