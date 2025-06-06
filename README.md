# Vet4Pet

Vet4Pet is a full-stack web application designed for veterinary clinics to manage animals, appointments, and facilitate real-time communication between veterinarians and pet owners. The system consists of a .NET-based REST API (backend) and an Angular-based web UI (frontend), with real-time chat powered by SignalR.

## Features
- Animal and owner management
- Appointment scheduling and history
- Real-time chat between veterinarians and owners (SignalR)
- User authentication and authorization
- State management in the frontend using NgRx
- Responsive and modern UI with Angular Material
- Unit tests for both backend and frontend

## Technologies Used
- **Backend:** .NET 8, Entity Framework Core, SignalR
- **Frontend:** Angular, Angular Material, RxJS, NgRx, @microsoft/signalr
- **Database:** SQL Server (default, can be changed)
- **Testing:** xUnit, Moq (backend); Jasmine, Karma (frontend)
- **Containerization:** Docker, Docker Compose

---

## Prerequisites
- [Docker](https://www.docker.com/) (recommended for easiest setup)
- Or, for manual setup:
  - [.NET 8 SDK](https://dotnet.microsoft.com/download)
  - [Node.js (v18+)](https://nodejs.org/)
  - [Angular CLI](https://angular.io/cli)
  - SQL Server (or change connection string for another DB)

---

## Getting Started

### 1. Run with Docker (Recommended)

The easiest way to run the entire stack (API, UI, and database) is using Docker Compose. This will automatically build and start all services, including the database with the correct schema.

```bash
git clone https://github.com/your-org/vet4pet-curotec.git
cd vet4pet-curotec
docker-compose up --build
```

- The API will be available at `http://localhost:8080`.
- The UI will be available at `http://localhost:4200`.
- The SQL Server database will be created and migrations applied automatically.

To stop the services:
```bash
docker-compose down
```

---

### 2. Manual Setup (Alternative)

If you prefer to run each application manually, follow these steps:

#### a. Clone the repository
```bash
git clone https://github.com/your-org/vet4pet-curotec.git
cd vet4pet-curotec
```

#### b. Setup the Database
- Create a SQL Server database instance (locally or in the cloud).
- Update the connection string in `Vet4PetAPI/appsettings.json` if needed.

#### c. Setup the API (Backend)
```bash
cd Vet4PetAPI
# Restore dependencies
dotnet restore
# Apply database migrations
dotnet ef database update
# Run the API
dotnet run
```
- The API will start on `http://localhost:8080` by default.
- SignalR hub is available at `/api/chat`.

#### d. Setup the UI (Frontend)
```bash
cd Vet4PetUI
# Install dependencies
npm install
# Run the Angular app
ng serve
```
- The UI will be available at `http://localhost:4200`.
- Make sure the API is running for full functionality.

#### e. Running Tests
- **API:**
  ```bash
  cd Vet4PetAPI
  dotnet test
  ```
- **UI:**
  ```bash
  cd Vet4PetUI
  ng test
  ```

---

## Real-Time Chat
- The chat feature uses SignalR for instant messaging between veterinarians and pet owners.
- Ensure both API and UI are running for chat to work.

---

## Project Structure
- `Vet4PetAPI/` - .NET backend (API, SignalR, business logic)
- `Vet4PetUI/` - Angular frontend (UI, services, components, NgRx state management)

---

## Environment Variables
- API connection strings and secrets can be set in `Vet4PetAPI/appsettings.json`.
- UI API endpoints can be configured in Angular environment files if needed.

---

## License
This project is for demonstration and educational purposes. 