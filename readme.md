# B&B Web App

Full-stack bed and breakfast booking application with a React frontend and an ASP.NET Core backend. The project lets guests browse rooms, view room details, search availability by date range, and submit a booking request.

## Project Overview

This repository contains two main applications:

```text
b&bWebApp/
├── backend/BookingProject/   # ASP.NET Core Web API
├── frontend/                 # React + Vite client
└── readme.md                 # Project-level documentation
```

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
- CSS modules/files for page and component styling

### Backend

- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQLite
- Swagger / OpenAPI
- Controller and service-based architecture

## Current Features

### Frontend

- Home page with project introduction and room browsing call-to-action.
- Rooms page that fetches room data from the backend.
- Availability search using check-in and check-out dates.
- Room cards with room type, description, guest capacity, size, and price.
- Room details page using route parameter `/rooms/:id`.
- Booking form page at `/rooms/:id/bookingForm`.
- Login page layout.
- Registration form component with React Hook Form and Yup validation.
- Shared layout through `App.jsx`, `Header`, `Footer`, and React Router outlet.

### Backend

- SQLite database configured through Entity Framework Core.
- Automatic database migration on startup.
- Seed data loaded from `Database/SeedingData.json`.
- Hotel, room, customer, and booking models.
- Room availability filtering based on date overlap with confirmed bookings.
- JSON reference cycle handling.
- Swagger UI in development.
- CORS enabled for frontend/backend communication.

## Available Routes

### Frontend Routes

```text
/                         Home page
/Login                    Login page
/rooms                    Room listing page
/rooms/:id                Room details page
/rooms/:id/bookingForm    Booking form page
```

### Backend Endpoints

```http
GET /
```

Returns the current hotel branch with related room data.

```http
GET /allRooms
```

Returns all rooms for the current hotel branch.

```http
GET /rooms?checkInDate=2026-06-20&checkOutDate=2026-06-22
```

Returns available rooms for a date range.

```http
GET /rooms/{id}
```

Returns one room by ID.

```http
POST /rooms/{roomId}/bookingForm
```

Creates a booking request for a room.

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

Dates should use this format:

```text
yyyy-MM-dd
```

## Environment Variables

The frontend reads the backend URL from `frontend/.env`:

```env
VITE_API_BASE_URL=https://localhost:7171
```

The API helper reads this value with:

```js
import.meta.env.VITE_API_BASE_URL
```

The backend database connection is configured in `backend/BookingProject/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Database/BookingAppDbContext.db"
  }
}
```

## Getting Started

### Prerequisites

Install these before running the project:

- Node.js
- npm
- .NET SDK 10

### Run The Backend

From the repository root:

```powershell
dotnet build .\backend\BookingProject\BookingProject.csproj
```

Then run:

```powershell
dotnet run --project .\backend\BookingProject\BookingProject.csproj
```

In development, Swagger should be available at:

```text
https://localhost:7171/swagger
```

If the port is different, update `frontend/.env`.

### Run The Frontend

From the frontend folder:

```powershell
cd .\frontend
npm install
npm run dev
```

The Vite dev server will print the local frontend URL in the terminal.

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

## API Integration Notes

Frontend API helpers live in:

```text
frontend/src/API/
```

Current files include:

```text
apiBase.js      Reads VITE_API_BASE_URL
GetAPIs.jsx    Fetches room data
PostAPIs.jsx   Posts booking requests
```

For booking creation, the frontend should call:

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

The object shape must match the backend `BookingRequestDTO`.

## Known Issues

- Some frontend lint errors currently exist in unrelated files, including unused imports and hook dependency warnings.
- Some API helpers return fallback values instead of throwing errors, which can hide failed requests.
- The booking form currently uses hard-coded demo data instead of real form state.
- The booking summary is hard-coded and should eventually use selected room data.
- Login and registration are UI-only and are not connected to backend authentication yet.
- The backend currently uses hard-coded hotel branch logic in some services/controllers.
- Backend error handling should be improved with clearer `404`, `400`, and validation responses.

## Troubleshooting

### POST returns 404

If this request fails:

```text
POST https://localhost:7171/rooms/1/bookingForm
```

Check these first:

- The backend is running.
- The backend is running on port `7171`.
- `frontend/.env` has the correct `VITE_API_BASE_URL`.
- Swagger shows `POST /rooms/{roomId}/bookingForm`.
- The backend was restarted after adding or changing controllers.

### SQLite migration lock

If the backend repeatedly logs migration lock messages, another backend process may already be running or SQLite may have a stale migration lock.

Stop the running backend process and restart the app. If needed, inspect the SQLite database and clear the stale `__EFMigrationsLock` row.

### Build file locked

If `.NET` build fails because `BookingProject.exe` is used by another process, stop the running backend and build again.

## Next Steps

1. Connect the booking form inputs to React state or React Hook Form.
2. Return a useful booking response from the backend, such as booking ID and confirmation message.
3. Improve frontend loading, success, and error states for API calls.
4. Replace hard-coded booking and room summary values with real route/API data.
5. Add backend validation responses for invalid booking requests.
6. Clean up current frontend lint errors.
7. Add tests for room availability and booking creation.

## Learning Focus

This project demonstrates:

- React routing and page composition.
- Frontend-to-backend communication with Axios.
- Environment-based API configuration with Vite.
- ASP.NET Core controllers and dependency injection.
- EF Core models, migrations, relationships, and seeding.
- SQLite as a local development database.
- Date handling between JavaScript and .NET APIs.
- Basic full-stack booking flow design.

## AI Assistance Statement

This project was developed as a learning and development project by the author. AI tools were used as an assistant during the process, mainly to explain code, clarify errors, suggest improvements, and help structure documentation. The project decisions, implementation direction, testing, debugging, and final responsibility for the code belong to the developer.
The goal of using AI in this project was not to replace the development process, but to support it in the same way a technical mentor, documentation reference, or debugging partner would. The codebase reflects an active learning process, including manual implementation, review, iteration, and problem solving.
