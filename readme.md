# B&B Web App

A full-stack bed and breakfast booking application built with a **React + Vite frontend** and an **ASP.NET Core Web API backend**.

The project lets guests browse rooms, view room details, search availability by date range, and submit booking requests. It is also a learning-focused project for practicing full-stack application structure, API integration, routing, state management, Entity Framework Core, and booking-domain logic.

---

## Project Status

**Status:** In development  
**Main focus:** Booking flow, room availability, API integration, and clean full-stack architecture.

This project is not a finished production system yet. Some features are complete, while others are intentionally kept in the roadmap as part of the learning and improvement process.

---

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture Overview](#architecture-overview)
- [Repository Structure](#repository-structure)
- [Frontend Routes](#frontend-routes)
- [Backend API Endpoints](#backend-api-endpoints)
- [Environment Configuration](#environment-configuration)
- [Getting Started](#getting-started)
- [Useful Commands](#useful-commands)
- [API Integration Notes](#api-integration-notes)
- [Known Limitations](#known-limitations)
- [Roadmap](#roadmap)
- [Learning Focus](#learning-focus)
- [AI Assistance Statement](#ai-assistance-statement)

---

## Features

### Current Frontend Features

- Home page with a clear introduction and call-to-action for browsing rooms.
- Room listing page that fetches room data from the backend.
- Availability search using check-in and check-out dates.
- Room cards showing room type, description, guest capacity, room size, and price.
- Room details page using the dynamic route `/rooms/:id`.
- Booking form page using the route `/rooms/:id/bookingForm`.
- Login page layout.
- Registration form component using React Hook Form and Yup validation.
- Shared application layout with `Header`, `Footer`, and React Router outlet.
- API helper layer for frontend/backend communication.

### Current Backend Features

- ASP.NET Core Web API backend.
- SQLite database configured through Entity Framework Core.
- Automatic database migration on application startup.
- Seed data loaded from `Database/SeedingData.json`.
- Hotel, room, customer, and booking domain models.
- Room availability filtering based on date overlap with confirmed bookings.
- Controller and service-based backend structure.
- Swagger/OpenAPI support in development.
- CORS configuration for frontend/backend communication.
- JSON reference cycle handling.

---

## Tech Stack

### Frontend

- React 19
- Vite
- React Router
- Axios
- Zustand
- React Hook Form
- Yup
- Tailwind CSS
- CSS files/modules for page and component styling

### Backend

- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- Controller and service-based architecture

### Development Tools

- Git and GitHub
- npm
- .NET CLI
- Swagger UI
- Browser developer tools

---

## Architecture Overview

```text
React Frontend
     |
     | Axios HTTP requests
     v
ASP.NET Core Web API
     |
     | Services / Controllers
     v
Entity Framework Core
     |
     v
SQLite Database
```

The frontend is responsible for the user interface, route navigation, form handling, and API calls.  
The backend is responsible for business rules, room availability checks, booking creation, database access, and API responses.

---

## Repository Structure

```text
b&bWebApp/
├── backend/BookingProject/   # ASP.NET Core Web API
├── frontend/                 # React + Vite client
└── readme.md                 # Project-level documentation
```

### Important Areas

```text
frontend/src/API/             # Frontend API helper functions
frontend/src/pages/           # Frontend pages and route views
frontend/src/components/      # Reusable UI components
backend/BookingProject/       # Backend API project
backend/BookingProject/Database/SeedingData.json
```

---

## Frontend Routes

| Route | Purpose |
| --- | --- |
| `/` | Home page |
| `/Login` | Login page layout |
| `/rooms` | Room listing page |
| `/rooms/:id` | Room details page |
| `/rooms/:id/bookingForm` | Booking form page |

---

## Backend API Endpoints

### Get Current Hotel Branch

```http
GET /
```

Returns the current hotel branch with related room data.

---

### Get All Rooms

```http
GET /allRooms
```

Returns all rooms for the current hotel branch.

---

### Search Available Rooms

```http
GET /rooms?checkInDate=2026-06-20&checkOutDate=2026-06-22
```

Returns rooms that are available within the selected date range.

---

### Get Room By ID

```http
GET /rooms/{id}
```

Returns one room by ID.

---

### Create Booking Request

```http
POST /rooms/{roomId}/bookingForm
```

Creates a booking request for a specific room.

Example request body:

```json
{
  "roomId": 1,
  "checkInDate": "2026-10-01",
  "checkOutDate": "2026-10-02",
  "numberOfGuests": 2,
  "customer": {
    "firstName": "John",
    "lastName": "Doe",
    "email": "john@example.com",
    "phoneNumber": "1234567890"
  }
}
```

Date values should use this format:

```text
yyyy-MM-dd
```

---

## Environment Configuration

### Frontend

The frontend reads the backend base URL from `frontend/.env`:

```env
VITE_API_BASE_URL=https://localhost:7171
```

The API helper reads the value with:

```js
import.meta.env.VITE_API_BASE_URL
```

### Backend

The backend database connection is configured in `backend/BookingProject/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Database/BookingAppDbContext.db"
  }
}
```

---

## Getting Started

### Prerequisites

Install the following tools before running the project:

- Node.js
- npm
- .NET SDK 10

---

### 1. Clone The Repository

```powershell
git clone https://github.com/T-King-00/Bnb-WebApplication.git
cd Bnb-WebApplication
```

---

### 2. Run The Backend

From the repository root:

```powershell
dotnet build .\backend\BookingProject\BookingProject.csproj
```

Then run the backend:

```powershell
dotnet run --project .\backend\BookingProject\BookingProject.csproj
```

In development, Swagger should be available at:

```text
https://localhost:7171/swagger
```

If the backend starts on another port, update `frontend/.env`.

---

### 3. Run The Frontend

From the frontend folder:

```powershell
cd .\frontend
npm install
npm run dev
```

The Vite dev server will print the local frontend URL in the terminal.

---

## Useful Commands

### Frontend

```powershell
npm run dev
npm run build
npm run lint
npm run preview
```

### Backend

```powershell
dotnet build .\backend\BookingProject\BookingProject.csproj
dotnet run --project .\backend\BookingProject\BookingProject.csproj
```

---

## API Integration Notes

Frontend API helper files live in:

```text
frontend/src/API/
```

Current files include:

```text
apiBase.js      # Reads VITE_API_BASE_URL
GetAPIs.jsx    # Fetches room data
PostAPIs.jsx   # Posts booking requests
```

For booking creation, the frontend should call the backend with an object matching the backend `BookingRequestDTO` shape:

```js
await PostBookingReq({
  roomId: 1,
  checkInDate: "2026-10-01",
  checkOutDate: "2026-10-02",
  numberOfGuests: 2,
  customer: {
    firstName: "John",
    lastName: "Doe",
    email: "john@example.com",
    phoneNumber: "1234567890",
  },
});
```

---

## Booking Flow

```text
1. Guest opens the room listing page.
2. Frontend fetches available rooms from the backend.
3. Guest selects a room.
4. Guest opens the room details page.
5. Guest fills in booking information.
6. Frontend sends a POST request to the backend.
7. Backend validates the request and creates the booking.
8. Frontend can display a success or error state.
```

---

## Known Limitations

- Some frontend lint errors currently exist, including unused imports and hook dependency warnings.
- Some API helpers return fallback values instead of throwing errors, which can hide failed requests.
- The booking form currently uses hard-coded demo data instead of fully connected form state.
- The booking summary is hard-coded and should eventually use selected room data.
- Login and registration are UI-only and are not connected to backend authentication yet.
- The backend currently uses hard-coded hotel branch logic in some services/controllers.
- Backend error handling should be improved with clearer `404`, `400`, and validation responses.

---

## Troubleshooting

### POST Request Returns 404

If this request fails:

```text
POST https://localhost:7171/rooms/1/bookingForm
```

Check these points:

- The backend is running.
- The backend is running on the same port used in `frontend/.env`.
- `frontend/.env` contains the correct `VITE_API_BASE_URL`.
- Swagger shows `POST /rooms/{roomId}/bookingForm`.
- The backend was restarted after adding or changing controllers.

---

### SQLite Migration Lock

If the backend repeatedly logs migration lock messages, another backend process may already be running or SQLite may have a stale migration lock.

Stop the running backend process and restart the app. If needed, inspect the SQLite database and clear the stale `__EFMigrationsLock` row.

---

### Build File Locked

If `.NET` build fails because `BookingProject.exe` is used by another process, stop the running backend and build again.

---

## Roadmap

Planned improvements:

- Connect booking form inputs to React state or React Hook Form.
- Return a useful booking response from the backend, such as booking ID and confirmation message.
- Improve frontend loading, success, and error states for API requests.
- Replace hard-coded booking and room summary values with real route/API data.
- Add backend validation responses for invalid booking requests.
- Clean up current frontend lint errors.
- Add tests for room availability logic.
- Add authentication and connect login/registration to the backend.
- Improve API route naming and response DTO consistency.
- Add better separation between domain models, DTOs, and API responses.

---

## Learning Focus

This project demonstrates practice with:

- React routing and page composition.
- Dynamic routes with route parameters.
- Frontend-to-backend communication with Axios.
- Environment-based API configuration with Vite.
- Form validation with React Hook Form and Yup.
- Lightweight frontend state management with Zustand.
- ASP.NET Core controllers and dependency injection.
- Service-layer business logic.
- EF Core models, relationships, migrations, and seeding.
- SQLite as a local development database.
- Date handling between JavaScript and .NET APIs.
- Basic full-stack booking flow design.

---

## Development Notes

This project is being developed as a practical full-stack learning project. The goal is not only to build features, but also to improve code structure, understand API design, practice debugging, and make the project easier to explain in a portfolio or job interview.

---

## AI Assistance Statement

This project was developed as a learning and development project by the author. AI tools were used as an assistant during the process, mainly to explain code, clarify errors, suggest improvements, and help structure documentation.

The project decisions, implementation direction, testing, debugging, and final responsibility for the code belong to the developer. The goal of using AI in this project was not to replace the development process, but to support it in the same way a technical mentor, documentation reference, or debugging partner would. The codebase reflects an active learning process, including manual implementation, review, iteration, and problem solving.
