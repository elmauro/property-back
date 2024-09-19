# Property-Service API Documentation

## Introduction

This API helps manage properties information for businesses. It makes it easier to add, update, and get data about properties.

In this document, we will talk about:

- **Design Patterns Used**
- **Basic Requirements**
- **How to Install**

## Design Patterns Used

- Repository Pattern: Abstraction of the logic Access Data
- Cqrs Pattern: Separates read and write operations in an application
- Mediator Pattern: Reduce direct dependencies between objects

## Basic Requirements

Before you begin, ensure that your system meets the following prerequisites:

**Running locally with Visual Studio 2022**
- Windows 10 installed
- At least 4 GB of RAM (8 GB or more recommended)
- Sufficient disk space to install the .NET SDK, development tools, and project dependencies (at least 10 GB of free space recommended)
- NET 8 SDK installed. Download and install the .NET 8 SDK from the official .NET website: https://dotnet.microsoft.com/download/dotnet/8.0
- Docker or Docker Desktop installed https://www.docker.com/
- docker-compose
- Git (version control): [Download from Git](https://git-scm.com/)
- Visual Studio 2022 Community Edition installed

**Running locally with Docker Desktop**
- Docker or Docker Desktop installed https://www.docker.com/
- docker-compose
- Git (version control): [Download from Git](https://git-scm.com/)

## How to Install

**Clone the repository**
```sh
https://github.com/elmauro/property-service
```

Use Docker Compose to launch the database

```sh
docker-compose -f compose.yml up --build -d property-postgres
```

```sh
winpty docker exec -it property-postgres psql -U property -d property -f scripts/idempotent-migration.sql
```

![image](https://github.com/user-attachments/assets/e9110029-5971-4774-b813-81ec260f1270)

## Starting the Service

You can start the service using Visual Studio 2022 or Docker Compose

**With Docker Compose**

```sh
docker-compose -f compose.yml up --build -d property
```

![image](https://github.com/user-attachments/assets/b93b0c2c-8477-4eee-8d70-ae81cab5b199)

## Using the Property Service API

**API Access**

The API can be accessed locally at:

```sh
http://localhost:56510/property/index.html
```

The API is fully documented using Swagger, which provides a detailed overview of available endpoints, request models, and response statuses. Please review the Swagger documentation to validate response HTTP statuses and understand the expected behavior of the API

![image](https://github.com/user-attachments/assets/7ab0239d-a9f8-4693-a325-f5bd39cbc307)

## Authentication

username: test

password: password

![image](https://github.com/user-attachments/assets/7c2548b9-0fb1-4763-aedd-16e00820b3fd)


**Important!**
The response for POST methods includes the automatic ID created in the location section of the response headers. You need the ID to update and access information.

Sample with the Property method POST

![image](https://github.com/user-attachments/assets/c164e60a-e35e-4701-a733-d0b1ee0ee3ee)


## Logs

You can view the logs from within the Docker container using the following command

```sh
docker logs [container_id]
```

![image](https://github.com/user-attachments/assets/7a5e8b0e-487f-4ac8-8ac0-bbec00fbd500)

![image](https://github.com/user-attachments/assets/4769ba7a-7ce3-4506-a50d-bf2b6ba0b920)



