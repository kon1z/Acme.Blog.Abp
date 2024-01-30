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
COPY ["src/Acme.Blog.AuthServer/Acme.Blog.AuthServer.csproj", "src/Acme.Blog.AuthServer/"]
COPY ["src/Acme.Blog.EntityFrameworkCore/Acme.Blog.EntityFrameworkCore.csproj", "src/Acme.Blog.EntityFrameworkCore/"]
COPY ["src/Acme.Blog.Domain/Acme.Blog.Domain.csproj", "src/Acme.Blog.Domain/"]
COPY ["src/Acme.Blog.Domain.Shared/Acme.Blog.Domain.Shared.csproj", "src/Acme.Blog.Domain.Shared/"]
RUN dotnet restore "./src/Acme.Blog.AuthServer/./Acme.Blog.AuthServer.csproj"
COPY . .
WORKDIR "/src/src/Acme.Blog.AuthServer"
RUN dotnet build "./Acme.Blog.AuthServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Acme.Blog.AuthServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acme.Blog.AuthServer.dll"]