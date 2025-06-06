# Vet4Pet Architecture Overview

## System Architecture

```
┌─────────────────┐     ┌─────────────────┐     ┌─────────────────┐
│                 │     │                 │     │                 │
│  Angular UI     │◄────┤  .NET API       │◄────┤  SQL Server    │
│  (Frontend)     │     │  (Backend)      │     │  (Database)    │
│                 │     │                 │     │                 │
└─────────────────┘     └─────────────────┘     └─────────────────┘
        ▲                        ▲
        │                        │
        │                        │
        │                        │
        │                        │
        │                        │
┌─────────────────┐     ┌─────────────────┐
│                 │     │                 │
│  SignalR        │     │  Entity         │
│  (Real-time)    │     │  Framework      │
│                 │     │                 │
└─────────────────┘     └─────────────────┘
```

## Frontend Architecture (Angular)

```
┌─────────────────────────────────────────────────┐
│                  Angular UI                     │
│                                                 │
│  ┌─────────────┐    ┌─────────────┐            │
│  │ Components  │    │   Services  │            │
│  └─────────────┘    └─────────────┘            │
│         ▲                  ▲                    │
│         │                  │                    │
│         │                  │                    │
│  ┌─────────────┐    ┌─────────────┐            │
│  │    NgRx     │    │  Angular    │            │
│  │   Store     │    │  Material   │            │
│  └─────────────┘    └─────────────┘            │
│                                                 │
└─────────────────────────────────────────────────┘
```

## Backend Architecture (.NET)

```
┌─────────────────────────────────────────────────────────────────────┐
│                         .NET Backend                                │
│                                                                     │
│  ┌─────────────────────────────────────────────────────────────┐   │
│  │                    Presentation Layer                        │   │
│  │  ┌─────────────┐    ┌─────────────┐    ┌─────────────┐      │   │
│  │  │Controllers  │    │  SignalR    │    │  DTOs       │      │   │
│  │  │             │    │    Hub      │    │             │      │   │
│  │  └─────────────┘    └─────────────┘    └─────────────┘      │   │
│  └─────────────────────────────────────────────────────────────┘   │
│                              ▲                                      │
│                              │                                      │
│  ┌─────────────────────────────────────────────────────────────┐   │
│  │                    Application Layer                         │   │
│  │  ┌─────────────┐    ┌─────────────┐    ┌─────────────┐      │   │
│  │  │  Services   │    │  Interfaces │    │  Mappings   │      │   │
│  │  │             │    │             │    │             │      │   │
│  │  └─────────────┘    └─────────────┘    └─────────────┘      │   │
│  └─────────────────────────────────────────────────────────────┘   │
│                              ▲                                      │
│                              │                                      │
│  ┌─────────────────────────────────────────────────────────────┐   │
│  │                    Domain Layer                             │   │
│  │  ┌─────────────┐    ┌─────────────┐    ┌─────────────┐      │   │
│  │  │  Entities   │    │  Interfaces │    │  Enums      │      │   │
│  │  │             │    │             │    │             │      │   │
│  │  └─────────────┘    └─────────────┘    └─────────────┘      │   │
│  └─────────────────────────────────────────────────────────────┘   │
│                              ▲                                      │
│                              │                                      │
│  ┌─────────────────────────────────────────────────────────────┐   │
│  │                    Infrastructure Layer                      │   │
│  │  ┌─────────────┐    ┌─────────────┐    ┌─────────────┐      │   │
│  │  │Repositories │    │  DbContext  │    │ Migrations  │      │   │
│  │  │             │    │             │    │             │      │   │
│  │  └─────────────┘    └─────────────┘    └─────────────┘      │   │
│  └─────────────────────────────────────────────────────────────┘   │
│                                                                     │
└─────────────────────────────────────────────────────────────────────┘
```

## Data Flow

```
┌──────────┐     HTTP/REST     ┌──────────┐
│          │◄─────────────────►│          │
│  UI      │                   │  API     │
│          │◄─────────────────►│          │
└──────────┘    SignalR/WS     └──────────┘
```

## Key Components

1. **Frontend (Angular)**
   - Components: UI building blocks
   - Services: API communication
   - NgRx Store: State management
   - SignalR Client: Real-time communication
   - Angular Material: UI components

2. **Backend (.NET)**
   - **Presentation Layer**
     - Controllers: REST endpoints
     - SignalR Hub: Real-time communication
     - DTOs: Data transfer objects
   
   - **Application Layer**
     - Services: Business logic implementation
     - Interfaces: Service contracts
     - Mappings: Object mapping logic
   
   - **Domain Layer**
     - Entities: Core business objects
     - Interfaces: Repository contracts
     - Enums: Domain enumerations
   
   - **Infrastructure Layer**
     - Repositories: Data access implementation
     - DbContext: Database context
     - Migrations: Database schema versioning

3. **Database (SQL Server)**
   - Tables: Users, Animals, Appointments, Messages
   - Migrations: Schema versioning
   - Relationships: Foreign keys and constraints

## Communication Patterns

1. **REST API**
   - CRUD operations
   - JSON data format
   - HTTP methods (GET, POST, PUT, DELETE)

2. **SignalR**
   - Real-time chat
   - WebSocket fallback
   - Bi-directional communication

3. **State Management**
   - NgRx store
   - Actions/Reducers
   - Effects for side effects
   - Selectors for data access

## Security

- JWT Authentication
- Role-based Authorization
- HTTPS/TLS
- CORS Configuration
- Input Validation
- SQL Injection Prevention 