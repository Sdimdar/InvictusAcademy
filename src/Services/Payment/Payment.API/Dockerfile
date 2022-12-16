#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Payment/Payment.API/Payment.API.csproj", "Services/Payment/Payment.API/"]
COPY ["Common/DataTransferLib/DataTransferLib.csproj", "Common/DataTransferLib/"]
COPY ["Common/GlobalExceptionHandler/GlobalExceptionHandler.csproj", "Common/GlobalExceptionHandler/"]
COPY ["Common/CommonStructures/CommonStructures.csproj", "Common/CommonStructures/"]
COPY ["Common/ServicesContracts/ServicesContracts.csproj", "Common/ServicesContracts/"]
COPY ["Services/CloudStorage/CloudStorage.Domain/CloudStorage.Domain.csproj", "Services/CloudStorage/CloudStorage.Domain/"]
COPY ["Common/CommonRepository/CommonRepository.csproj", "Common/CommonRepository/"]
COPY ["Services/Courses/Courses.Domain/Courses.Domain.csproj", "Services/Courses/Courses.Domain/"]
COPY ["Services/User/User.Domain/User.Domain.csproj", "Services/User/User.Domain/"]
COPY ["Services/Payment/Payment.Domain/Payment.Domain.csproj", "Services/Payment/Payment.Domain/"]
COPY ["Services/Request/Request.Domain/Request.Domain.csproj", "Services/Request/Request.Domain/"]
COPY ["Services/Payment/Payment.Application/Payment.Application.csproj", "Services/Payment/Payment.Application/"]
COPY ["Services/Payment/Payment.Infrastructure/Payment.Infrastructure.csproj", "Services/Payment/Payment.Infrastructure/"]
RUN dotnet restore "Services/Payment/Payment.API/Payment.API.csproj"
COPY . .
WORKDIR "/src/Services/Payment/Payment.API"
RUN dotnet build "Payment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.API.dll"]