#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Acme.Blog.DbMigrator/Acme.Blog.DbMigrator.csproj", "src/Acme.Blog.DbMigrator/"]
COPY ["src/Acme.Blog.Application.Contracts/Acme.Blog.Application.Contracts.csproj", "src/Acme.Blog.Application.Contracts/"]
COPY ["src/Acme.Blog.Domain.Shared/Acme.Blog.Domain.Shared.csproj", "src/Acme.Blog.Domain.Shared/"]
COPY ["src/Acme.Blog.EntityFrameworkCore/Acme.Blog.EntityFrameworkCore.csproj", "src/Acme.Blog.EntityFrameworkCore/"]
COPY ["src/Acme.Blog.Domain/Acme.Blog.Domain.csproj", "src/Acme.Blog.Domain/"]
RUN dotnet restore "./src/Acme.Blog.DbMigrator/./Acme.Blog.DbMigrator.csproj"
COPY . .
WORKDIR "/src/src/Acme.Blog.DbMigrator"
RUN dotnet build "./Acme.Blog.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Acme.Blog.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN /bin/bash -c "sed -i $'s/\r$//' entrypoint.sh"
RUN chmod +x ./entrypoint.sh
ENTRYPOINT ["./entrypoint.sh"]