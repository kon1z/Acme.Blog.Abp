#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Acme.Blog.HttpApi.Host/Acme.Blog.HttpApi.Host.csproj", "src/Acme.Blog.HttpApi.Host/"]
COPY ["framework/Acme.Auditing.Elasticsearch/Acme.Auditing.Elasticsearch.csproj", "framework/Acme.Auditing.Elasticsearch/"]
COPY ["src/Acme.Blog.Application/Acme.Blog.Application.csproj", "src/Acme.Blog.Application/"]
COPY ["src/Acme.Blog.Domain/Acme.Blog.Domain.csproj", "src/Acme.Blog.Domain/"]
COPY ["src/Acme.Blog.Domain.Shared/Acme.Blog.Domain.Shared.csproj", "src/Acme.Blog.Domain.Shared/"]
COPY ["src/Acme.Blog.Application.Contracts/Acme.Blog.Application.Contracts.csproj", "src/Acme.Blog.Application.Contracts/"]
COPY ["src/Acme.Blog.EntityFrameworkCore/Acme.Blog.EntityFrameworkCore.csproj", "src/Acme.Blog.EntityFrameworkCore/"]
COPY ["src/Acme.Blog.HttpApi/Acme.Blog.HttpApi.csproj", "src/Acme.Blog.HttpApi/"]
RUN dotnet restore "./src/Acme.Blog.HttpApi.Host/./Acme.Blog.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Acme.Blog.HttpApi.Host"
RUN dotnet build "./Acme.Blog.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Acme.Blog.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acme.Blog.HttpApi.Host.dll"]