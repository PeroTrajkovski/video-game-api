A small ASP.NET Core Web API that provides CRUD operations for a `VideoGame` entity using Entity Framework Core and SQL Server. 
The API is minimal and suitable as a learning/example project.

---

## Table of contents
- [Project overview](#project-overview)
- [Prerequisites](#prerequisites)
- [Configuration](#configuration)
- [Database and migrations](#database-and-migrations)
- [Run the API](#run-the-api)
- [API endpoints](#api-endpoints)
- [Model schema](#model-schema)
- [Seed data](#seed-data)
- [Development notes & recommendations](#development-notes--recommendations)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)
- [License](#license)

---

## Project overview

This repository implements an ASP.NET Core Web API (targeting .NET 9) that manages `VideoGame` records persisted with Entity Framework Core and SQL Server. It includes:

- `Program.cs` — application bootstrap and service registration (DbContext, controllers, OpenAPI).
- `Data/ApplicationDbContext.cs` — EF Core `DbContext` with a `DbSet<VideoGame>` and seed data.
- `Models/VideoGame.cs` — POCO model for the `VideoGame` entity.
- `Controllers/VideoGameController.cs` — controller exposing CRUD endpoints at `/api/VideoGame`.

This project uses C# 13.0 language features where present in the codebase.

## Prerequisites

- .NET 9 SDK installed
- SQL Server instance (local or remote) accessible from this machine
- Optional: `dotnet-ef` global tool for running migrations: `dotnet tool install --global dotnet-ef` if not already installed

## Configuration

1. Open `appsettings.json` and set a connection string named `DefaultConnection` that points to your SQL Server. Example:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=VideoGameDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}

2. If using SQL Server Authentication, replace `Trusted_Connection=True` with `User Id=...;Password=...;`.

## Database and migrations

This project seeds initial data in `ApplicationDbContext.OnModelCreating` using `HasData(...)`. To create the database and apply the seed data, run EF Core migrations:

# From repository root
dotnet ef migrations add InitialCreate
dotnet ef database update

Notes:
- The seed data uses explicit `Id` values; ensure migrations are generated and applied so the seed rows are inserted.
- If you change the model, add a new migration and update the database again.

## Run the API

From the repository root:

dotnet run

By default, the app will run on the configured Kestrel ports and will expose OpenAPI/Swagger UI when the environment is `Development` (see `Program.cs`).

## API endpoints

Base path: `/api/VideoGame`

- `GET /api/VideoGame`
  - Returns: 200 OK — list of all video games
- `GET /api/VideoGame/{id}`
  - Returns: 200 OK — single video game
  - Returns: 404 Not Found — if not found
- `POST /api/VideoGame`
  - Body: JSON representation of a `VideoGame`
  - Returns: 201 Created — created resource location header points to `GET /api/VideoGame/{id}`
- `PUT /api/VideoGame/{id}`
  - Body: JSON representation of `VideoGame` fields to update
  - Returns: 200 OK — updated resource
  - Returns: 404 Not Found — if the id does not exist
- `DELETE /api/VideoGame/{id}`
  - Returns: 200 OK — deletion success message
  - Returns: 404 Not Found — if the id does not exist

Example curl to list all games:

curl -s http://localhost:5000/api/VideoGame | jq

Create example (POST):

curl -X POST http://localhost:5000/api/VideoGame \
  -H "Content-Type: application/json" \
  -d '{"title":"Hollow Knight","platform":"PC","developer":"Team Cherry","publisher":"Team Cherry"}'

## Model schema

`VideoGame` model (located at `Models/VideoGame.cs`):

- `int Id` — primary key
- `string? Title`
- `string? Platform`
- `string? Developer`
- `string? Publisher`

All string properties are nullable in the current model. Add data annotations such as `[Required]` and make properties non-nullable if the API should enforce required fields.

## Seed data

`ApplicationDbContext` seeds four initial games via `HasData` using explicit `Id` values (1-4). This runs when migrations are applied to the database.

## Development notes & recommendations

- **Controller constructor**: The controller uses the primary-constructor-like syntax in `VideoGameController` (`public class VideoGameController(ApplicationDbContext context) : ControllerBase`) which is a newer C# feature. If you encounter compatibility issues, convert to a standard constructor:

public VideoGameController(ApplicationDbContext context)
{
    _context = context;
}

- **Async consistency**: `DeleteVideoGame` currently uses synchronous `Find` instead of `FindAsync`. Prefer `await _context.VideoGames.FindAsync(id)` to avoid blocking.

- **API contracts**: Consider using `ActionResult<T>` return types and model validation (`[ApiController]` enables automatic model validation) and check `ModelState.IsValid` or rely on model binding.

- **Concurrency & error handling**: Add try/catch for `DbUpdateException` and handle `DbUpdateConcurrencyException` if you add concurrency tokens in the model.

- **Nullability & validation**: Mark required fields non-nullable and add `[Required]`, `[MaxLength]`, or custom validation attributes as needed.

## Troubleshooting

- **Migration errors**: If EF Core reports duplicate seed keys, remove the conflicting seed rows or update the seed data and add a new migration.
- **Connection issues**: Verify `DefaultConnection` and that SQL Server accepts connections from your app. Use `TrustServerCertificate=True` for local dev when using self-signed certs.
- **Missing dotnet-ef**: Install the tool with `dotnet tool install --global dotnet-ef`.

## Contributing

Contributions are welcome. Fork and create a branch, run the app and tests (if added), and open a PR with a clear description of changes.

If your team has coding standards, put them in `.editorconfig` and `CONTRIBUTING.md` — this README references those files if present.