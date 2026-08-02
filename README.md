# E-Commerce 

A backend E-Commerce RESTful API built with ASP.NET Core 8, featuring authentication, authorization, caching, payment integration, and a layered architecture.

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

The solution is organized into separate layers:

- E-Commerce.API – API endpoints, configuration, and HTTP pipeline
- E-Commerce.Application – Business logic, services, DTOs, and contracts
- E-Commerce.Domain – Domain entities and core business rules
- E-Commerce.Infrastructure – Database access, repositories, Identity, Redis, and payment integrations

## Key Features

- User registration and authentication
- JWT-based authentication
- Role-based authorization
- Product and catalog management
- Shopping basket
- Order management
- Redis caching
- Stripe payment integration
- Entity Framework Core with SQL Server
- Database migrations and data seeding
- AutoMapper for object mapping
- Swagger API documentation
- Static file and image handling

## Design Patterns & Principles

- Repository Pattern
- Unit of Work Pattern
- Dependency Injection
- Service Layer
- Separation of Concerns
- SOLID Principles

## Getting Started

1. Clone the repository.
2. Configure the SQL Server connection strings.
3. Configure JWT settings.
4. Configure the Redis connection.
5. Add your Stripe API keys using User Secrets or Environment Variables.
6. Apply the Entity Framework Core migrations.
7. Run the application.

## Security

Sensitive configuration such as database credentials, JWT secrets, Redis credentials, and Stripe secret keys should not be committed to source control.

## API Documentation

Swagger/OpenAPI is available when running the application in the Development environment.

## Author

Yousef Mohamed Said
