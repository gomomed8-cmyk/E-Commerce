# E-Commerce API

A production-style E-Commerce RESTful API built with ASP.NET Core 8, featuring JWT authentication, role-based authorization, Redis caching, Stripe payments, and a layered architecture.

## Technologies

- C#
- ASP.NET Core 8 Web API
- Entity Framework Core 8
- SQL Server
- ASP.NET Core Identity
- JWT Authentication & Role-Based Authorization
- Redis
- Stripe Payments
- AutoMapper
- Swagger / OpenAPI

## Architecture

The solution follows a layered architecture with clear separation of responsibilities:

- **E-Commerce.API** – API endpoints, configuration, and HTTP pipeline.
- **E-Commerce.Application** – Business logic, DTOs, services, and contracts.
- **E-Commerce.Domain** – Domain entities and core business rules.
- **E-Commerce.Infrastructure** – Database access, repositories, Identity, Redis, and external service integrations.

## Key Features

### Authentication & Authorization

- User registration and authentication
- JWT-based authentication
- Role-based authorization
- ASP.NET Core Identity

### Catalog Management

- Product and catalog management
- Product types and brands
- Static file and image handling

### Basket & Orders

- Shopping basket management
- Order creation and management
- Order processing

### Payments

- Stripe payment integration

### Performance & Caching

- Redis caching
- Reduced database access for frequently requested data

### Data Access

- Entity Framework Core
- SQL Server
- Repository Pattern
- Unit of Work Pattern
- Database migrations
- Data seeding
- AutoMapper for object mapping

### API

- RESTful API
- Swagger / OpenAPI documentation

## Design Patterns & Principles

- Repository Pattern
- Unit of Work Pattern
- Dependency Injection
- Service Layer
- Separation of Concerns
- SOLID Principles

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- Redis

### Setup

1. Clone the repository.

2. Configure the SQL Server connection string.

3. Configure JWT settings.

4. Configure the Redis connection.

5. Configure Stripe using User Secrets or Environment Variables.

6. Apply the Entity Framework Core migrations.

7. Run the application.

## Security

This repository does not contain sensitive credentials.

The following values should be configured securely and should not be committed to source control:

- Database connection strings
- JWT secrets
- Redis credentials
- Stripe API keys

For local development, use **User Secrets** or **Environment Variables**.

## API Documentation

The API is documented using Swagger / OpenAPI.

When running the application in the Development environment, Swagger UI can be used to explore and test the available endpoints.

## Project Structure

E-Commerce
│
├── E-Commerce.API
├── E-Commerce.Application
├── E-Commerce.Domain
└── E-Commerce.Infrastructure
