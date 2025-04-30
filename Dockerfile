FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cursos.WebApp/Cursos.WebApp.csproj", "Cursos.WebApp/"]
COPY ["Cursos.Application/Cursos.Application.csproj", "Cursos.Application/"]
COPY ["Cursos.Infrastructure/Cursos.Infrastructure.csproj", "Cursos.Infrastructure/"]
COPY ["Cursos.Domain/Cursos.Domain.csproj", "Cursos.Domain/"]
COPY ["Cursos.CrossCutting/Cursos.CrossCutting.csproj", "Cursos.CrossCutting/"]
COPY ["Cursos.IoC/Cursos.IoC.csproj", "Cursos.IoC/"]
RUN dotnet restore "Cursos.WebApp/Cursos.WebApp.csproj"
COPY . .
WORKDIR "/src/Cursos.WebApp"
RUN dotnet build "Cursos.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Cursos.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cursos.WebApp.dll"]