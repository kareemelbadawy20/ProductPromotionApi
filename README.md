
# Product Promotion API

## Introduction
The Product Promotion API is a RESTful service built using .NET and ASP.NET Core. It provides endpoints for managing products and promotions, catering to both administrators and mobile clients. The API adheres to clean architecture principles and SOLID design for scalability, maintainability, and testability.

## Technology Stack
- **.NET and ASP.NET Core (C#)**
- **Entity Framework Core**
- **SQL Server** or **PostgreSQL**
- **JWT-based authentication**

## Prerequisites
- **.NET 9.0 SDK**
- **Docker** and **Docker Compose**
- **SQL Server** 

---

## Setup

### 1. Clone the Repository
```bash
git clone https://github.com/kareemelbadawy20/ProductPromotionApi.git
cd ProductPromotionApi
```

### 2. Configure the Database
Update the `appsettings.json` file with your database connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=ProductPromotionDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "your_secret_key",
    "Issuer": "your_issuer"
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

### 3. Apply Migrations
Run the following commands to set up the database schema:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run the Application
#### Using Docker Compose
Build and run the service:
```bash
docker-compose up --build
```

#### Without Docker
Run the application directly:
```bash
dotnet run --project ProductPromotionAPI.WebAPI
```

---

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

---

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

---

## Authentication
Admin API endpoints require **JWT-based authentication**. Include the token in the `Authorization` header:
```
Authorization: Bearer <your_token>
```

---

## Running Tests

### Unit Tests
Run unit tests:
```bash
dotnet test ProductPromotionAPI.Tests
```

### Integration Tests
Run integration tests:
```bash
dotnet test ProductPromotionAPI.IntegrationTests
```

---

## Docker Instructions

### Dockerfile
```dockerfile
# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY ProductPromotionAPI/* ProductPromotionAPI/
COPY ProductPromotionAPI.Application/* ProductPromotionAPI.Application/
COPY ProductPromotionAPI.Domain/* ProductPromotionAPI.Domain/
COPY ProductPromotionAPI.Infrastructure/* ProductPromotionAPI.Infrastructure/
COPY ProductPromotionAPI.WebAPI/* ProductPromotionAPI.WebAPI/
RUN dotnet restore

WORKDIR /app/ProductPromotionAPI.WebAPI
RUN dotnet publish -c Release -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/ProductPromotionAPI.WebAPI/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "ProductPromotionAPI.WebAPI.dll"]
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
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=ProductPromotionDB;User Id=sa;Password=Your_password123;
```

---

By following these instructions, you should be able to set up, run, and test the Product Promotion API. For assistance, feel free to reach out!
