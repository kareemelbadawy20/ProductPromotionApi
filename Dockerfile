# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY ProductPromotionApi.Api/ProductPromotionApi.Api.csproj ProductPromotionApi.Api/
COPY ProductPromotionApi.Application/ProductPromotionApi.Application.csproj ProductPromotionApi.Application/
COPY ProductPromotionApi.Core/ProductPromotionApi.Core.csproj ProductPromotionApi.Core/
COPY ProductPromotionApi.Infrastructure/ProductPromotionApi.Infrastructure.csproj ProductPromotionApi.Infrastructure/
COPY ProductPromotionAPI.Tests/ProductPromotionAPI.Tests.csproj ProductPromotionAPI.Tests/
RUN dotnet restore

# Copy everything else and build the application
COPY . .
WORKDIR /app/ProductPromotionApi.Api
RUN dotnet publish -c Release -o out

# Use the official ASP.NET Core runtime as the base image for the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/ProductPromotionApi.Api/out .

# Expose port 80 for the application
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ProductPromotionApi.Api.dll"]
