FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Courses/Courses.API/Courses.API.csproj", "Services/Courses/Courses.API/"]
COPY ["Common/DataTransferLib/DataTransferLib.csproj", "Common/DataTransferLib/"]
COPY ["Common/GlobalExceptionHandler/GlobalExceptionHandler.csproj", "Common/GlobalExceptionHandler/"]
COPY ["Common/ServicesContracts/ServicesContracts.csproj", "Common/ServicesContracts/"]
COPY ["Services/Courses/Courses.Domain/Courses.Domain.csproj", "Services/Courses/Courses.Domain/"]
COPY ["Common/CommonRepository/CommonRepository.csproj", "Common/CommonRepository/"]
COPY ["Common/CommonStructures/CommonStructures.csproj", "Common/CommonStructures/"]
COPY ["Services/User/User.Domain/User.Domain.csproj", "Services/User/User.Domain/"]
COPY ["Services/Request/Request.Domain/Request.Domain.csproj", "Services/Request/Request.Domain/"]
COPY ["Services/Courses/Courses.Infrastructure/Courses.Infrastructure.csproj", "Services/Courses/Courses.Infrastructure/"]
COPY ["Services/Courses/Courses.Application/Courses.Application.csproj", "Services/Courses/Courses.Application/"]
RUN dotnet restore "Services/Courses/Courses.API/Courses.API.csproj"
COPY . .
WORKDIR "/src/Services/Courses/Courses.API"
RUN dotnet build "Courses.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Courses.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Courses.API.dll"]