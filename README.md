
# Product Promotion API

## Introduction
The Product Promotion API is a RESTful service built using .NET and ASP.NET Core. It provides endpoints for managing products and promotions, with separate endpoints for administrators and mobile clients. The API follows clean architecture principles and SOLID design principles to ensure scalability, maintainability, and testability.

## Technology Stack
- .NET and ASP.NET Core (C#)
- Entity Framework Core
- SQL Server
- JWT-based authentication

## Prerequisites
- .NET 9.0 SDK
- Docker
- Docker Compose
- SQL Server

## Setup

### Clone the Repository
```bash
git clone https://github.com/kareemelbadawy20/ProductPromotionApi.git
cd ProductPromotionApi
```

### Configure Database
Ensure your database connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\SQLEXPRESS;Database=ProductPromotionDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "your_secret_key",
    "Issuer": "your_issuer",
    "Audience": "your_audience"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### Apply Migrations
Run the following commands to apply migrations and create the database:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Running the Application

#### Using Docker Compose
Build and run the Docker Compose setup:
```bash
docker-compose up --build
```

#### Without Docker
Run the application directly:
```bash
dotnet run --project ProductPromotionApi.Api
```

## API Endpoints

### Admin API
| **Method** | **Endpoint**                     | **Description**            |
|------------|-----------------------------------|----------------------------|
| POST       | `/api/admin/products`            | Create a new product       |
| PUT        | `/api/admin/products/{id}`       | Update an existing product |
| DELETE     | `/api/admin/products/{id}`       | Delete a product           |
| GET        | `/api/admin/products`            | Get a list of products     |
| GET        | `/api/admin/products/{id}`       | Get a product by ID        |
| POST       | `/api/admin/promotions`          | Create a new promotion     |
| PUT        | `/api/admin/promotions/{id}`     | Update an existing promotion |
| DELETE     | `/api/admin/promotions/{id}`     | Delete a promotion         |
| GET        | `/api/admin/promotions`          | Get a list of promotions   |
| GET        | `/api/admin/promotions/{id}`     | Get a promotion by ID      |

**Example: Create a Product**
```json
POST /api/admin/products
{
  "name": "Product1",
  "description": "Description for Product1",
  "price": 100.0
}
```

### Mobile API
| **Method** | **Endpoint**             | **Description**      |
|------------|---------------------------|----------------------|
| GET        | `/api/mobile/products`    | List products        |
| GET        | `/api/mobile/featured`    | Get featured products |
| GET        | `/api/mobile/new`         | Get new products     |
| GET        | `/api/mobile/promotions`  | Get promotions       |

**Example: List Products**
```json
GET /api/mobile/products
```

## Authentication
Admin API endpoints use **JWT-based authentication**. Include the token in the `Authorization` header:
```
Authorization: Bearer <your_token>
```

## Running Tests

### Unit Tests
Run unit tests:
```bash
dotnet test ProductPromotionApi.Tests
```

### Integration Tests
Run integration tests:
```bash
dotnet test ProductPromotionApi.IntegrationTests
```

## Docker Instructions

### Dockerfile
```dockerfile
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
```

### Docker Compose
```yaml
version: '3.4'

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      SA_PASSWORD: "Your_password123"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"

  productpromotionapi:
    image: productpromotionapi
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8080:80"
    depends_on:
      - sqlserver
    environment:
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=ProductPromotionDB;Trusted_Connection=True;TrustServerCertificate=True;
      - Jwt__Key=your_secret_key
      - Jwt__Issuer=your_issuer
      - Jwt__Audience=your_audience
```

### Build and Run using Docker Compose
Build and run the Docker Compose setup:
```bash
docker-compose up --build
```

### Verify the Setup
Open a web browser or Postman and navigate to `http://localhost:8080` to verify that the API is running.

Use the API endpoints as described in your project documentation to ensure everything is working correctly.
