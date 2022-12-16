#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/FreeArticles/FreeArticles.API/FreeArticles.API.csproj", "Services/FreeArticles/FreeArticles.API/"]
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
COPY ["Services/FreeArticles/FreeArticles.Application/FreeArticles.Application.csproj", "Services/FreeArticles/FreeArticles.Application/"]
COPY ["Services/FreeArticles/FreeArticles.Domain/FreeArticles.Domain.csproj", "Services/FreeArticles/FreeArticles.Domain/"]
COPY ["Services/FreeArticles/FreeArticles.Infrastructure/FreeArticles.Infrastructure.csproj", "Services/FreeArticles/FreeArticles.Infrastructure/"]
RUN dotnet restore "Services/FreeArticles/FreeArticles.API/FreeArticles.API.csproj"
COPY . .
WORKDIR "/src/Services/FreeArticles/FreeArticles.API"
RUN dotnet build "FreeArticles.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreeArticles.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreeArticles.API.dll"]